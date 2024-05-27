using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Regular movement speed
    [SerializeField] private float boostedSpeed = 10f; // Speed when Shift is pressed for UP and DOWN

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Check if the Shift key is held down
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Increase speed only for UP and DOWN movements
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                movement.z *= boostedSpeed;
            }
        }
        else
        {
            movement *= speed;
        }

        // Apply movement to the character
        transform.Translate(movement * Time.deltaTime, Space.World);
    }
}
