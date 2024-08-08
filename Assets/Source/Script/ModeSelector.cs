using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectModeToEdit { Vertex, Edge, Face }

public class ModeSelector : MonoBehaviour
{

    public void HandleDropdownList(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Vertex Mode Selected");
                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Vertex;
                break;
            case 1:
                Debug.Log("Edge Mode Selected");
                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Edge;
                break;
            case 2:
                Debug.Log("Face Mode Selected");
                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Face;
                break;
            default:
                Debug.Log("Invalid Mode Selected");
                break;
        }
    }
}
