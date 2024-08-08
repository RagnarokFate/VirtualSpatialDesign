using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float movementSpeed = 10.0f;
    public float lookSpeed = 2.0f;

    void Update()
    {
        // Always look at the origin (0,0,0)
        transform.LookAt(Vector3.zero);

        // Get input for movement
        float moveForwardBackward = Input.GetAxis("Vertical");  // W/S or Up/Down Arrow
        float moveLeftRight = Input.GetAxis("Horizontal");      // A/D or Left/Right Arrow
        float moveUp = 0.0f;
        float moveDown = 0.0f;

        // Get input for vertical movement
        if (Input.GetKey(KeyCode.E))  // Move up
        {
            moveUp = 1.0f;
        }
        if (Input.GetKey(KeyCode.Q))  // Move down
        {
            moveDown = -1.0f;
        }

        // Calculate movement direction
        Vector3 move = new Vector3(moveLeftRight, moveUp + moveDown, moveForwardBackward);

        // Apply movement
        transform.Translate(move * movementSpeed * Time.deltaTime, Space.Self);
    }
}
