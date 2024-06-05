using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [SerializeField] private Transform brumukiTransform;
    [SerializeField] private float effectAmmount;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float startingPosition, length;

    void Start()
    {
        startingPosition = transform.position.x;
        length = spriteRenderer.bounds.size.x;
    }


    void Update()
    {
        float distanceRelativeToCamera = brumukiTransform.position.x * (1 - effectAmmount);
        float moveAmount = brumukiTransform.position.x * effectAmmount;
        gameObject.transform.position = new Vector3(startingPosition + moveAmount, transform.position.y, transform.position.z);
        if(distanceRelativeToCamera > startingPosition +  length)
        {
            startingPosition += length;
        }
        else if(distanceRelativeToCamera < startingPosition - length)
        {
            startingPosition -= length;
        }
    }
}
