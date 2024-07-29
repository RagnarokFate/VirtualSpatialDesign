using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserDeselection
{
    public UserDeselection()
    {
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleDeselection()
    {
        // TODO
        GameManager.Instance.SetActiveGameObject(null);
    }

}
