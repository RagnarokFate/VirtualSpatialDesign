using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserScale
{
    private bool locked;
    private AxisLock axisLock;
    private Vector2 initialMousePos;
    private Vector3 initialScale;
    private bool isScaling;

    public bool meshScaleLock;

    public float scaleSensitivity = 0.01f;
    public UserScale()
    {
        meshScaleLock = false;
        locked = false;
        axisLock = AxisLock.none;
        isScaling = false;
    }

    // Reset back object color upon deselecting/unclicking Active GameObject
    public void HandleUserScale()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        GameManager.Instance.currentBrushTool = BrushTool.none;
        GameManager.Instance.currentMainTool = MainTool.none;

        if (gameObject != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Input.GetMouseButtonDown(0)) // Start scaling on mouse down
                {
                    initialMousePos = mousePos;
                    initialScale = gameObject.transform.localScale;
                    isScaling = true;
                }

                if (Input.GetMouseButton(0) && isScaling) // Continue scaling while holding mouse button
                {
                    Vector2 mouseDelta = mousePos - initialMousePos;
                    float mouseMagnitude = mouseDelta.magnitude;
                    // float mouseMagnitude = (mouseDelta.magnitude > 0) ? -mouseDelta.magnitude : mouseDelta.magnitude;
                    float scaleFactor = 1 + mouseMagnitude * scaleSensitivity; // Adjust scaling sensitivity

                    Vector3 scale = initialScale;
                    HandleLock();

                    if (axisLock == AxisLock.none)
                    {
                        scale = initialScale * scaleFactor;
                    }
                    else if (axisLock == AxisLock.X_Axis)
                    {
                        scale.x = initialScale.x * scaleFactor;
                    }
                    else if (axisLock == AxisLock.Y_Axis)
                    {
                        scale.y = initialScale.y * scaleFactor;
                    }
                    else if (axisLock == AxisLock.Z_Axis)
                    {
                        scale.z = initialScale.z * scaleFactor;
                    }

                    gameObject.transform.localScale = scale;
                }

                if (Input.GetMouseButtonUp(0)) // Stop scaling on mouse up
                {
                    isScaling = false;
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

    public void LockScale()
    {
        meshScaleLock = true;
    }

    public void UnlockScale()
    {
        meshScaleLock = false;
    }
}



