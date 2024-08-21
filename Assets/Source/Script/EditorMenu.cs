using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ProBuilder;
using System.Linq;
using UnityEngine.ProBuilder.MeshOperations;
using System;
using UnityEditor.ProBuilder;



public class EditorMenu : MonoBehaviour
{


    public static bool isSelected = false;
    
    // UI Elements
    private Button SelectElementButton;
    private Button InsertElementButton;
    private Button EditElementButton;
    private Button DeleteElementButton;
    private Button PullButton;
    private Button PushButton;

    private Canvas editorMenu;

    //Prefabs
    public GameObject Vertex;
    public GameObject Edge;
    public GameObject Face;

    /*// profile users interaction
    public UserInsertEditor userInsertEditor;
    public UserEditEditor userEditEditor;
    public UserDeleteEditor userDeleteEditor;*/
    public UserSelectEditor userSelectEditor;
    public UserExtrusion userExtrusion;
    public UserInclusion userInclusion;

    // Start is called before the first frame update
    void Start()
    {
        editorMenu = GameObject.Find("EditorMenu").GetComponent<Canvas>();
        editorMenu.enabled = false;

        // profile users interaction
        userExtrusion = new UserExtrusion();
        userInclusion = new UserInclusion();
        userSelectEditor = new UserSelectEditor();
        userSelectEditor.setPrefabs(Vertex);


    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveMesh();
        HandleEditorToolSwitch();
        HandleEditorTools();

        /*if (currentBrushTool == EditorTool.extrude)
        {
            userExtrusion.HandleExtrusion();
        }*/




    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        SelectElementButton = GameObject.Find("SelectElementButton").GetComponent<Button>();
        InsertElementButton = GameObject.Find("InsertElementButton").GetComponent<Button>();
        EditElementButton = GameObject.Find("EditElementButton").GetComponent<Button>();
        DeleteElementButton = GameObject.Find("DeleteElementButton").GetComponent<Button>();
        PullButton = GameObject.Find("PullButton").GetComponent<Button>();
        PushButton = GameObject.Find("PushButton").GetComponent<Button>();

        SelectElementButton.onClick.AddListener(() => setCurrentEditorTool(EditorTool.select));
        InsertElementButton.onClick.AddListener(() => setCurrentEditorTool(EditorTool.insert));
        EditElementButton.onClick.AddListener(() => setCurrentEditorTool(EditorTool.edit));
        DeleteElementButton.onClick.AddListener(() => setCurrentEditorTool(EditorTool.delete));
        PullButton.onClick.AddListener(() => setCurrentEditorTool(EditorTool.pull));
        PushButton.onClick.AddListener(() => setCurrentEditorTool(EditorTool.push));

    }

    // ===================================================================================================================
    // getter and setter for currentBrushTool
    public EditorTool getCurrentEditorTool()
    {
        return GameManager.Instance.currentEditorTool;
    }
    public void setCurrentEditorTool(EditorTool editorTool)
    {
        GameManager.Instance.currentEditorTool = editorTool;
    }
    public EditorTool getLastEditorTool()
    {
        return GameManager.Instance.lastEditorTool;
    }
    public void setLastEditorTool(EditorTool editorTool)
    {
        GameManager.Instance.lastEditorTool = editorTool;
    }


    // ======================================== 
    public void HandleEditorToolSwitch()
    {
        EditorTool currentEditorTool = getCurrentEditorTool();
        EditorTool lastEditorTool = getLastEditorTool();

        if (currentEditorTool != lastEditorTool)
        {

            Debug.Log("Current Brush Tool: " + currentEditorTool);
            if(currentEditorTool == EditorTool.select && !isSelected)
            {
                Debug.Log("<color=green>select an element from screen.</color>");

            }
            else if (currentEditorTool == EditorTool.insert && !isSelected)
            {
                Debug.Log("<color=green>Insert an element</color>");
            }
            else if(currentEditorTool == EditorTool.edit && !isSelected)
            {
                Debug.Log("<color=green>Edit an element</color>");
            }
            else if (currentEditorTool == EditorTool.delete && !isSelected)
            {
                Debug.Log("<color=green>Delete an element</color>");
            }
            else if (currentEditorTool == EditorTool.pull && !isSelected)
            {
                Debug.Log("<color=green>Pull an element</color>");
                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Face;
                Debug.Log("<color=purple>Face Mode FORCE | ENABLED</color>");
            }
            else if (currentEditorTool == EditorTool.push && !isSelected)
            {
                Debug.Log("<color=green>Push an element</color>");
                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Face;
                Debug.Log("<color=purple>Face Mode FORCE | ENABLED</color>");
            }
            else
            {
                Debug.Log("<color=red>Invalid Brush Tool</color>");
            }
            setLastEditorTool(currentEditorTool);
            setCurrentEditorTool(currentEditorTool);

        }
    }

    public void CheckActiveMesh()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject == null)
        {
            /*Debug.LogError("No Game Object Selected");*/
            return;
        }
        else
        {
            if (GameManager.Instance.IsItAMesh(gameObject))
            {
                editorMenu.enabled = true;
            }
            else
            {
                editorMenu.enabled = false;
            }
        }
    }

    public void HandleEditorTools()
    {
        EditorTool currentEditorTool = getCurrentEditorTool();
        EditorTool lastEditorTool = getLastEditorTool();

        if (currentEditorTool == EditorTool.select)
        {
            // nothing but update selectModeToEdit which already done
            userSelectEditor.setProBuilderToUserSelectEditor();
            userSelectEditor.HandleEditorSelection();
        }
        else if (currentEditorTool == EditorTool.insert)
        {
            // userInsertEditor.HandleInsert();
        }
        else if (currentEditorTool == EditorTool.edit)
        {
            // userEditEditor.HandleEdit();
        }
        else if (currentEditorTool == EditorTool.delete)
        {
            // userDeleteEditor.HandleDelete();
        }
        else if (currentEditorTool == EditorTool.pull)
        {
            userExtrusion.HandleExtrusion();
        }
        else if (currentEditorTool == EditorTool.push)
        {
            userInclusion.HandleInclusion();
        }
    }




}
