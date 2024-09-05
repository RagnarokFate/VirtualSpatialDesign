using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserRotation
{
    private bool locked;
    private AxisLock axisLock;
    private Vector2 initialMousePos;
    private Quaternion initialRotation;
    private bool isRotating;

    public bool meshRotationLock;
    public float rotationSensitivity = 0.1f;


    public UserRotation()
    {
        meshRotationLock = false;
        locked = false;
        axisLock = AxisLock.none;
        isRotating = false;
    }

    // Reset back object color upon deselecting/unclicking Active GameObject
    public void HandleUserRotation()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;

        if (gameObject != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Input.GetMouseButtonDown(0)) // Start rotation on mouse down
                {
                    initialMousePos = mousePos;
                    initialRotation = gameObject.transform.rotation;
                    isRotating = true;
                }

                if (Input.GetMouseButton(0) && isRotating) // Continue rotation while holding mouse button
                {
                    Vector2 mouseDelta = mousePos - initialMousePos;
                    float rotationAngle = mouseDelta.magnitude * rotationSensitivity; // Adjust rotation sensitivity

                    Vector3 rotationAxis = Vector3.zero;
                    HandleLock();

                    if (axisLock == AxisLock.none)
                    {
                        rotationAxis = new Vector3(mouseDelta.y, -mouseDelta.x, 0);
                    }
                    else if (axisLock == AxisLock.X_Axis)
                    {
                        rotationAxis = Vector3.right;
                    }
                    else if (axisLock == AxisLock.Y_Axis)
                    {
                        rotationAxis = Vector3.up;
                    }
                    else if (axisLock == AxisLock.Z_Axis)
                    {
                        rotationAxis = Vector3.forward;
                    }

                    gameObject.transform.rotation = initialRotation * Quaternion.AngleAxis(rotationAngle, rotationAxis);
                }

                if (Input.GetMouseButtonUp(0)) // Stop rotation on mouse up
                {
                    isRotating = false;

                    string text = "Selected object :" + gameObject.name + " with rotation vector of :" + gameObject.transform.localRotation.ToString();
                    FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("MainMenuLayout").GetComponent<Canvas>().transform);
                }
            }
        }
    }

    public void HandleLock()
    {
        if (Input.GetKey(KeyCode.X))
        {
            locked = true;
            if (locked)
            {
                axisLock = AxisLock.X_Axis;
            }

        }
        else if (Input.GetKey(KeyCode.Y))
        {
            locked = true;
            if (locked)
            {
                axisLock = AxisLock.Y_Axis;
            }
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            locked = true;
            if (locked)
            {
                axisLock = AxisLock.Z_Axis;
            }
        }
        else
        {
            locked = false;
            axisLock = AxisLock.none;
        }

    }

    public void LockRotation()
    {
        meshRotationLock = true;
    }

    public void UnlockRotation()
    {
        meshRotationLock = false;
    }
}