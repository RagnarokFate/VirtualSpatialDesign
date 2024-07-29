using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum status { Selection, Hightlight, None }
public class UserSelection
{
   
    public UserSelection()
    {
    }

    public void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Mouse Clicked");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit Object : " + hit.collider.gameObject.name);
                //GameManager.Instance.SetActiveGameObject(hit.collider.gameObject);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse Clicked");
        }
    }


}
