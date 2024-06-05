using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingPointRotation : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    private Vector3 cursorPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = cursorPosition - gameObject.transform.position;
        float rotationZAxis = (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationZAxis);

    }
}
