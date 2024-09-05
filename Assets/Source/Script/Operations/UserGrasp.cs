using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class UserGrasp
{
    private bool locked;
    private AxisLock axisLock;

    public bool meshPositionLock;
    private Vector3 previousPos;
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
        
        if(previousPos == null)
        {
            previousPos = gameObject.transform.position;
        }
        if (gameObject != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPos;
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject != null)
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
                previousPos = worldPos;
                string text = "Tranlated game object " + gameObject.name + " to position : " + worldPos.ToString() ;
                FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("MainMenuLayout").GetComponent<Canvas>().transform);
                return;
            }
            HandleLock();
            if (locked)
            {
                if (axisLock == AxisLock.X_Axis)
                {
                    gameObject.transform.position = new Vector3(worldPos.x, previousPos.y, previousPos.z);
                }
                else if (axisLock == AxisLock.Y_Axis)
                {
                    gameObject.transform.position = new Vector3(previousPos.x, worldPos.y, previousPos.z);
                }
                else if (axisLock == AxisLock.Z_Axis)
                {
                    gameObject.transform.position = new Vector3(previousPos.x, previousPos.y, worldPos.z);
                }
                previousPos = gameObject.transform.position;

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
