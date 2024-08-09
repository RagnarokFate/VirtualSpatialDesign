using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserDeletion
{
    public UserDeletion()
    {
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleDeletion()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {

            if (gameObject == null)
            {
                return;
            }

            List<GameObject> gameObjects = GameManager.Instance.gameObjectList;
            if (gameObjects == null)
            {
                return;
            }

            if (gameObjects.Contains(gameObject))
            {
                gameObjects.Remove(gameObject);
                Object.Destroy(gameObject);
                GameManager.Instance.activeGameObject = null;
                Debug.Log("Game Object Deleted ");
                GameManager.Instance.gameObjectList = gameObjects;
            }
            else
            {
                Debug.Log("Game Object Not Found ");
            }
        }

    }

}
