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
                string text = "Deleting game object : " + gameObject.name;
                FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("MainMenuLayout").GetComponent<Canvas>().transform);

                gameObjects.Remove(gameObject);
                Object.Destroy(gameObject);
                if(GameManager.Instance.IsItAMesh(gameObject))
                {
                    GameManager.Instance.threeD_Counter--;
                }
                else
                {
                    GameManager.Instance.twoD_Counter--;
                }
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
