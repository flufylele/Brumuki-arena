using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] Rigidbody2D burukiRigidBody;
    [SerializeField] ParticleSystem runningParticleSystem;
    [SerializeField] BrumukiController brumukiController;
    [SerializeField] float timeBeforeParticles;
    private float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (brumukiController.onGround && burukiRigidBody.velocity.x != 0 && timeCounter > timeBeforeParticles)
        {
            runningParticleSystem.Play();
            timeCounter = 0;
        }
        else
        {
            runningParticleSystem.Stop();
        }
        
    }
}
