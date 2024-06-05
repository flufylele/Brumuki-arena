using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleController : MonoBehaviour
{
    [SerializeField] private float fireDuration;
    private float currentFireDuration;
    private BrumukiController brumukiController;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        brumukiController = GameObject.Find("Brumuki").GetComponent<BrumukiController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFireDuration += Time.deltaTime;
        if(gameObject.layer == 6 && currentFireDuration > fireDuration)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            currentFireDuration = 0;
            gameObject.layer = 7;
        }
        else if(gameObject.layer == 7)
        {
            currentFireDuration = 0;
        }

    }
}
