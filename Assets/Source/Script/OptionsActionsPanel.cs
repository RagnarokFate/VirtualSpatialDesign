using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class OptionsActionsPanel : MonoBehaviour
{
    public static bool OptionsPanel = false;

    public GameObject OptionsPanelGameObject;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        HandleOptionsListPanel();
    }

    public void OpenOptionsPanel()
    {
        OptionsPanelGameObject.SetActive(true);
        OptionsPanel = true;
    }

    public void CloseOptionsPanel()
    {
        OptionsPanelGameObject.SetActive(false);
        OptionsPanel = false;
    }



    public void HandleOptionsListPanel()
    {
        if (GameManager.Instance.currentEditorTool == EditorTool.none && Input.GetMouseButtonDown(1))
        {
            
            if(OptionsPanel == true)
                CloseOptionsPanel();
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Vector2 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
            OptionsPanelGameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
            OpenOptionsPanel();
        }
    }


}
