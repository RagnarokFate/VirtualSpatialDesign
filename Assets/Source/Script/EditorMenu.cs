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
    private GameObject TextDecayGameObject;
    //Prefabs
    public GameObject Vertex;

    /*// profile users interaction
    public UserInsertEditor userInsertEditor;
    public UserEditEditor userEditEditor;
    public UserDeleteEditor userDeleteEditor;*/
    private UserSelectEditor userSelectEditor;
    private UserInsertEditor userInsertEditor;
    private userDeleteEditor userDeleteEditor;
    private UserExtrusion userExtrusion;
    private UserInclusion userInclusion;

    // Start is called before the first frame update
    void Start()
    {
        editorMenu = GameObject.Find("EditorMenu").GetComponent<Canvas>();
        //editorMenu.enabled = false;

        // profile users interaction
        userExtrusion = new UserExtrusion();
        userInclusion = new UserInclusion();
        userSelectEditor = new UserSelectEditor();
        userSelectEditor.setPrefabs(Vertex);

        userInsertEditor = new UserInsertEditor(Vertex);
        userDeleteEditor = new userDeleteEditor(Vertex);


    }

    // Update is called once per frame
    void Update()
    {
        // CheckActiveMesh();
        HandleEditorToolSwitch();
        HandleEditorTools();



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

        SelectElementButton.onClick.AddListener(() => CheckEditorToolSwitch(EditorTool.select));
        InsertElementButton.onClick.AddListener(() => CheckEditorToolSwitch(EditorTool.insert));
        EditElementButton.onClick.AddListener(() => CheckEditorToolSwitch(EditorTool.edit));
        DeleteElementButton.onClick.AddListener(() => CheckEditorToolSwitch(EditorTool.delete));
        PullButton.onClick.AddListener(() => CheckEditorToolSwitch(EditorTool.pull));
        PushButton.onClick.AddListener(() => CheckEditorToolSwitch(EditorTool.push));

    }

    // ===================================================================================================================
    // getter and setter for currentBrushTool
    public EditorTool getCurrentEditorTool()
    {
        return GameManager.Instance.currentEditorTool;
    }
    public void CheckEditorToolSwitch(EditorTool editorTool)
    {
        if(getCurrentEditorTool() != editorTool)
        {
            setCurrentEditorTool(editorTool);
        }
        else
        {
            Debug.Log("Editor tool has been set to `none`");
            setCurrentEditorTool(EditorTool.none);
        }
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
            TextDecayGameObject = new GameObject("TextDecayGameObject");
            FadeOutText fadeOutText = TextDecayGameObject.AddComponent<FadeOutText>();

            Debug.Log("Current Brush Tool: " + currentEditorTool);
            if(currentEditorTool == EditorTool.select && !isSelected)
            {
                FadeOutText.Show(2f, Color.green, "Selection an element is enabled", new Vector2(0, 400), editorMenu.transform);
            }
            else if (currentEditorTool == EditorTool.insert && !isSelected)
            {
                FadeOutText.Show(2f, Color.green, "Insertion an element is enabled", new Vector2(0, 400), editorMenu.transform);

            }
            else if(currentEditorTool == EditorTool.edit && !isSelected)
            {
                FadeOutText.Show(2f, Color.green, "Editing an element is enabled", new Vector2(0, 400), editorMenu.transform);

            }
            else if (currentEditorTool == EditorTool.delete && !isSelected)
            {
                FadeOutText.Show(2f, Color.green, "Deletion an element is enabled", new Vector2(0, 400), editorMenu.transform);
            }
            else if (currentEditorTool == EditorTool.pull && !isSelected)
            {
                FadeOutText.Show(2f, Color.green, "Extrusion an element is enabled", new Vector2(0, 400), editorMenu.transform);
                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Face;
            }
            else if (currentEditorTool == EditorTool.push && !isSelected)
            {
                FadeOutText.Show(2f, Color.green, "Inclusion an element is enabled", new Vector2(0, 400), editorMenu.transform);

                GameManager.Instance.selectModeToEdit = SelectModeToEdit.Face;
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
            userInsertEditor.HandleInsertEditor();
        }
        else if (currentEditorTool == EditorTool.edit)
        {
            // userEditEditor.HandleEdit();
        }
        else if (currentEditorTool == EditorTool.delete)
        {
            userDeleteEditor.HandleDeleteEditor();

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
