using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    public Button VertexModeButton;
    public Button EdgeModeButton;
    public Button FaceModeButton;

    public GameObject SelectingModePanel;

    public void CloseSelectingModePanel()
    {
        SelectingModePanel.SetActive(false);
    }

    public void SetVertexMode()
    {
        GameManager.Instance.selectModeToEdit = SelectModeToEdit.Vertex;
        Debug.Log("<color=purple>Vertex Mode</color>");
        CloseSelectingModePanel();
    }
    public void SetEdgeMode()
    {
        GameManager.Instance.selectModeToEdit = SelectModeToEdit.Edge;
        Debug.Log("<color=purple>Edge Mode</color>");
        CloseSelectingModePanel();
    }
    public void SetFaceMode()
    {
        GameManager.Instance.selectModeToEdit = SelectModeToEdit.Face;
        Debug.Log("<color=purple>Face Mode</color>");
        CloseSelectingModePanel();
    }
    // EXTRA
    public void ClearSelection()
    {
        GameManager.Instance.selectModeToEdit = SelectModeToEdit.none;
    }


}
