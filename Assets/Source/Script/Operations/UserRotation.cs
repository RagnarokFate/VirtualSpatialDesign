using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class UserRotation
{
    private bool locked;
    private AxisLock axisLock;

    public bool meshRotationLock;

    public UserRotation()
    {
        meshRotationLock = false;
        locked = false;
        axisLock = AxisLock.none;
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleUserRotation()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        GameManager.Instance.currentBrushTool = BrushTool.none;
        GameManager.Instance.currentMainTool = MainTool.none;


        if (gameObject != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPos;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                worldPos = -1 * (hit.point - Camera.main.transform.position).normalized;
            }
            else
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                lockRotation();
                gameObject.transform.rotation = Quaternion.Euler(worldPos);
                GameManager.Instance.currentMainTool = MainTool.none;
                GameManager.Instance.currentBrushTool = BrushTool.none;
                GameManager.Instance.currentTransformTool = TransformTool.none;
                return;
            }

            if (locked)
            {
                if (axisLock == AxisLock.X_Axis)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(worldPos.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z));
                }
                else if (axisLock == AxisLock.Y_Axis)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, worldPos.y, gameObject.transform.rotation.z));
                }
                else if (axisLock == AxisLock.Z_Axis)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, worldPos.z));
                }
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(worldPos);
            }
        }
        
    }
    public void HandleLock()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            locked = !locked;
            if (locked)
            {
                axisLock = AxisLock.X_Axis;
            }
            else
            {
                axisLock = AxisLock.none;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            locked = !locked;
            if (locked)
            {
                axisLock = AxisLock.Y_Axis;
            }
            else
            {
                axisLock = AxisLock.none;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            locked = !locked;
            if (locked)
            {
                axisLock = AxisLock.Z_Axis;
            }
            else
            {
                axisLock = AxisLock.none;
            }
        }

    }

    public void lockRotation()
    {
        meshRotationLock = true;
    }
    public void unlockRotation()
    {
        meshRotationLock = false;
    }
}   
