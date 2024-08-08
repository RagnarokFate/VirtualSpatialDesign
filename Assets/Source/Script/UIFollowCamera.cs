using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    
    public Camera uiCamera; // Reference to the UI camera (for screen space - camera mode)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Match the position of the canvas to the camera
        transform.position = uiCamera.transform.position;

        // Match the rotation of the canvas to the camera
        transform.rotation = uiCamera.transform.rotation;
    }

}
