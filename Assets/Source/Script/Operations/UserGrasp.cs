using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class UserGrasp
{
    private bool locked;
    private AxisLock axisLock;

    public bool meshPositionLock;

    public UserGrasp()
    {
        meshPositionLock = false;
        locked = false;
        axisLock = AxisLock.none;
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleUserGrasp()
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
                worldPos = hit.point;
            }
            else
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                lockPosition();
                gameObject.transform.position = worldPos;
                GameManager.Instance.currentMainTool = MainTool.none;
                GameManager.Instance.currentBrushTool = BrushTool.none;
                GameManager.Instance.currentTransformTool = TransformTool.none;
                return;
            }
            HandleLock();
            if (locked)
            {
                if (axisLock == AxisLock.X_Axis)
                {
                    gameObject.transform.position = new Vector3(worldPos.x, gameObject.transform.position.y, gameObject.transform.position.z);
                }
                else if (axisLock == AxisLock.Y_Axis)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, worldPos.y, gameObject.transform.position.z);
                }
                else if (axisLock == AxisLock.Z_Axis)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, worldPos.z);
                }
            }
            else
            {
                gameObject.transform.position = worldPos;
            }
        }
        /*else
        {
            Debug.Log("No Active GameObject");
        }*/


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
        
    public void lockPosition()
    {
        meshPositionLock = true;
    }
    public void unlockPosition()
    {
        meshPositionLock = false;
    }
}
