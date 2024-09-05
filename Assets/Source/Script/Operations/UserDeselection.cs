using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class UserDeselection
{
    public UserDeselection()
    {
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleDeselection()
    {
        // TODO
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if(gameObject != null)
        {
            string text = "Deselecting the current active object ";
            FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("MainMenuLayout").GetComponent<Canvas>().transform);

            GameManager.Instance.SetActiveGameObject(null);
        }
        else
        {
            return;
        }

    }

}
