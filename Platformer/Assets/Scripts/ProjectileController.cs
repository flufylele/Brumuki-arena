using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D projectileRigidBody;
    
    [SerializeField] private float projectileRotationSpeed;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] public float damage;
    private float projectileDuration;
    private Camera mainCamera;
    private Vector3 cursorPosition;
    
     

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = GameObject.Find("Game settings").GetComponent<GameSettings>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 projectileDirection = cursorPosition - gameObject.transform.position;
        projectileRigidBody.velocity = projectileDirection * gameSettings.settings.projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        projectileDuration += Time.deltaTime;
        transform.Rotate(0, 0, projectileRotationSpeed);
        if(projectileDuration > gameSettings.settings.maxProjectileDuration)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            collision.gameObject.layer = 6;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }

        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        
    }

}
