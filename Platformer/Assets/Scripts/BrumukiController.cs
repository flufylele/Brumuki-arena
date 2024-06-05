using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class BrumukiController : MonoBehaviour
{
    //Stats
    private float health;
    private float armor;
    private float maxHealth;
    private float maxArmor;

    //Jumping
    private int remainingNumberOfJumps;
    private float remainingJumpHeight;

    //Moving
    private float moveHorizontal;

    //Checks
    private bool jumping;
    public bool onGround;
    private bool onFire;
    private bool gamePaused;

    //Time check
    private float lastTakenFireDamage;
    private float lastTakenContactDamage;
    public float onFireDuration;

    [Header("Components")]
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private Rigidbody2D mainRigidBody;
    [SerializeField] private SpriteRenderer mainSprite;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private TextMeshProUGUI armorUI;
    [SerializeField] private InGameUiController inGameUI;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image healthFill;
    [SerializeField] private Image armorFill;
    private GameStats gameStats;
    private Transform shootingPointRotationTransform;
    private Transform shootingPointTransform;
    
    //Game objects
    [SerializeField] private GameObject projectile;
    

    [Header("Particles")]
    [SerializeField] private ParticleSystem runningParticleSystem;
    [SerializeField] private ParticleSystem fireParticle;

    [Header("Sounds")]
    [SerializeField] private AudioSource footstepsSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource hitSound;

    //Down velocity
    private float timePassedDownVelocity;

    //Shooting
    private float lastShot;


    void Start()
    {
        //Getting all needed components
        shootingPointRotationTransform = GameObject.Find("ShootingPointRotation").GetComponent<Transform>();
        shootingPointTransform = GameObject.Find("ShootingPoint").GetComponent<Transform>();
        gameSettings = GameObject.Find("Game settings").GetComponent<GameSettings>();
        gameStats = GameObject.Find("Stats").GetComponent<GameStats>();

        //Setting variables
        gameSettings.music = GameObject.Find("Music").GetComponent<AudioSource>();
        gameSettings.music.volume = gameSettings.settings.volume;
        footstepsSound.enabled = false;
        footstepsSound.volume = gameSettings.settings.volume / 5;
        jumpSound.volume = gameSettings.settings.volume / 1.5f;
        hitSound.volume = gameSettings.settings.volume;

        if (!gameSettings.settings.playerHealthBar)
        {
            healthBar.SetActive(false);
        }

        fireParticle.Stop();    
        
        remainingNumberOfJumps = gameSettings.settings.maxNumberOfJumps;
        remainingJumpHeight = gameSettings.settings.jumpHeight;

        maxHealth = gameSettings.settings.maxHealth;
        health = maxHealth;
        healthFill.fillAmount = 1;
        healthUI.text = health.ToString();

        maxArmor = gameSettings.settings.maxArmor;
        armor = maxArmor;
        if (maxArmor > 0) armorFill.fillAmount = 1;
        else armorFill.fillAmount = 0;

        
        armorUI.text = armor.ToString();

    }
    

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        lastShot += Time.deltaTime;
        lastTakenFireDamage += Time.deltaTime;
        onFireDuration += Time.deltaTime;

        if ((mainSprite.flipX && moveHorizontal > 0) || (!mainSprite.flipX && moveHorizontal < 0))
        {
            Flip();
        };

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && remainingNumberOfJumps > 0)
        {
            jumpSound.Play();
            Jump();
        };
       
        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && jumping == true && remainingJumpHeight > 0) 
        {
            JumpHigher();     
        };

        
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && onGround == false)
        {
            Fall();
        };

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) && lastShot > gameSettings.settings.fireRate)
        {
            Shoot();
        }

        if((Input.GetKeyDown(KeyCode.Escape)))
        {
            inGameUI.PauseMenu();
            footstepsSound.enabled = false;
        }


        if(Time.timeScale > 0 && onFire && onFireDuration < 5)
        {
            if(lastTakenFireDamage > 1)
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

        if(onGround && mainRigidBody.velocity.x != 0)
        {
            footstepsSound.enabled = true;
        }
        else
        { 
            footstepsSound.enabled = false; 
        }

    }

    private void FixedUpdate()
    {
            mainRigidBody.velocity = new Vector2(moveHorizontal * gameSettings.settings.speed, mainRigidBody.velocity.y);
    }


    //Entering collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            remainingNumberOfJumps = gameSettings.settings.maxNumberOfJumps;
            onGround = true;
            jumping = false;
            remainingJumpHeight = gameSettings.settings.jumpHeight;
            timePassedDownVelocity = 0f;
        }

        if (collision.gameObject.layer == 6)
        {
            onFire = true;
            onFireDuration = 0;
            fireParticle.Play();
        }

        if(collision.gameObject.CompareTag("Next level"))
        {
            SceneManager.LoadScene("Shop");
        }
    }


    //Exiting collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }

    }

    //Continuous collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Time.timeScale > 0)
        {
            lastTakenContactDamage += Time.deltaTime;
            if (collision.gameObject.tag == "Enemy" && lastTakenContactDamage > 1f)
            {
                TakeDamage(collision.gameObject.GetComponent<EnemyController>().enemyStats.contactDamage);
                lastTakenContactDamage = 0f;
            }

            if (collision.gameObject.layer == 6) // standing on fire activated
            {

                onFire = true;
                fireParticle.Play();
                onFireDuration = 0;
            }
        }
        
    }

    private void Flip()
    {
        mainSprite.flipX = !mainSprite.flipX;
    }

    private void TakeDamage(float damage)
    {
        if(armor > 0)
        {
            armor -= damage;
            if (armor < 0)
            {
                armor = 0;
            }
            armorUI.text = armor.ToString();
            armorFill.fillAmount = armor / maxArmor;
        }
        else
        {
            health -= damage;
            healthUI.text = health.ToString();
            healthFill.fillAmount = health / maxHealth;
            if (health <= 0f)
            {
                Die();
            }
        }
        hitSound.Play();



    }

    private void Die()
    {
        mainRigidBody.velocity = new Vector2(0, 0);
        footstepsSound.enabled = false;
        Time.timeScale = 0;
        inGameUI.DiedMenu();
        
    }

    private void Shoot()
    {
        Instantiate(projectile, shootingPointTransform.position, Quaternion.identity);
        lastShot = 0;
        gameStats.shotsFired += 1;
    }

    private void Jump()
    {
        remainingJumpHeight = gameSettings.settings.jumpHeight;
        mainRigidBody.velocity = new Vector2(mainRigidBody.velocity.x, gameSettings.settings.jumpForce);
        remainingNumberOfJumps -= 1;
        jumping = true;
        onGround = false;
    }

    private void JumpHigher()
    {
        mainRigidBody.velocity = new Vector2(mainRigidBody.velocity.x, gameSettings.settings.jumpForce);
        remainingJumpHeight -= Time.deltaTime;
        
    }

    private void Fall()
    {
        timePassedDownVelocity += Time.deltaTime;
        mainRigidBody.velocity = new Vector2(mainRigidBody.velocity.x, mainRigidBody.velocity.y - (gameSettings.settings.fallForce * timePassedDownVelocity));
    }
}

