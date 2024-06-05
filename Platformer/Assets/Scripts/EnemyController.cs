using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private float health;
    private float startingPoint;
    private int direction;
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private RawImage exclamationPoint;
    [SerializeField] private Image healthFill;
    [SerializeField] private GameObject healthBar;
    

    public EnemyStats enemyStats;
    private GameStats gameStats;
    private GameSettings gameSettings;
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI scoreText;

    //Time check
    private float lastTakenFireDamage;
    public float onFireDuration;
    private float lastFlip;

    //Checks
    private bool playerFound;
    private bool onFire;

    [Header("Particles")]
    [SerializeField] private ParticleSystem fireParticle;

    private float offSetRaycast;

    void Start()
    {
        enemyStats = GameObject.Find("EnemyStats").GetComponent<EnemyStats>();
        gameSettings = GameObject.Find("Game settings").GetComponent<GameSettings>();
        gameStats = GameObject.Find("Stats").GetComponent<GameStats>();
        
        if (!gameSettings.settings .enemiesHealthbar)
        {
            healthBar.SetActive(false);
        }

        health = enemyStats.maxHealth;
        healthFill.fillAmount = 1;
        fireParticle.Stop();
        
        startingPoint = gameObject.transform.position.x;
        direction = 1;
        offSetRaycast = 2.1f;
        exclamationPoint.enabled = false;
    }

    

    void Update()
    {
        lastTakenFireDamage += Time.deltaTime;
        onFireDuration += Time.deltaTime;
        lastFlip += Time.deltaTime;

        if (onFire && onFireDuration < 5)
        {
            if (lastTakenFireDamage > 1)
            {
                TakeDamage(5);
                lastTakenFireDamage = 0;
            }
        }
        else
        {
            onFire = false;
            onFireDuration = 0;
            fireParticle.Stop();
        }

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + offSetRaycast, gameObject.transform.position.y),
            new Vector2(direction, 0), enemyStats.viewDistance);

        //Debug.DrawRay(new Vector2(gameObject.transform.position.x + offSetRaycast, gameObject.transform.position.y),new Vector2(direction * 50f , 0), Color.red, 50f);
        if(hit && hit.collider.gameObject.CompareTag("Brumuki"))
        {
            playerFound = true;
            enemySprite.color = Color.red;
            exclamationPoint.enabled = true;
        }
        else if(!hit || !hit.collider.CompareTag("Projectile"))
        {
            playerFound = false;
            enemySprite.color = Color.grey;
            exclamationPoint.enabled = false;
        }

        if (playerFound)
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
            if (Vector2.Distance(gameObject.transform.position, new Vector2(startingPoint, gameObject.transform.position.y)) > enemyStats.maxPatrolDistance)
            {
                Flip();
            };
        }
        

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.name == "Enemy 1 (2)")
        {
        }
        if(collision.collider.gameObject.tag == "Projectile")
        {
            TakeDamage(collision.gameObject.GetComponent<ProjectileController>().damage);
        }
        if (collision.gameObject.layer == 6)
        {
            onFire = true;
            onFireDuration = 0;
            fireParticle.Play();
        }
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy")) && lastFlip > 1f)
        {
            Flip();
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 6) // standing on fire activated
        {
            onFire = true;
            fireParticle.Play();
            onFireDuration = 0;
        }
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy")) && lastFlip > 1f)
        {
            Flip();
        }

    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        healthFill.fillAmount = health / enemyStats.maxHealth;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Patrol()
    {
        enemyRigidBody.velocity = new Vector2(direction * enemyStats.movementSeed, enemyRigidBody.velocity.y);
    }

    private void FollowPlayer()
    {

        enemyRigidBody.velocity = new Vector2(direction * enemyStats.movementSeedAggro, enemyRigidBody.velocity.y);
        startingPoint = gameObject.transform.position.x;
    }

    private void Flip()
    {
        enemySprite.flipX = !enemySprite.flipX;
        startingPoint = gameObject.transform.position.x;
        direction = direction * (-1);
        offSetRaycast = offSetRaycast * (-1);
        lastFlip = 0;
    }

    private void Death()
    {
        goldText = GameObject.Find("Gold text").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score text").GetComponent<TextMeshProUGUI>();
        gameStats.enemiesKilled += 1;
        gameStats.gold += enemyStats.goldGained;
        gameStats.score += enemyStats.scoreGained;
        goldText.text = gameStats.gold.ToString();
        scoreText.text = "Score: " + gameStats.score.ToString();
        Destroy(gameObject);
    }
}
