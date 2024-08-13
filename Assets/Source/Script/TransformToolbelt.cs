using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor.ProBuilder;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class TransformToolbelt : MonoBehaviour
{
    // Transofrm Tool Buttons
    private Button graspButton;
    private Button rotateButton;
    private Button scaleButton;
    private Button brushButton;
    private Button deleteButton;

    private Canvas toolbarLayout;


    //function profiles - Transformation ToolBar
    public UserGrasp userGrasp;
    public UserRotation userRotation;
    public UserScale userScale;
    public UserDeletion userDeletion;
    // Start is called before the first frame update
    void Start()
    {
        toolbarLayout = GameObject.Find("ToolbarLayout").GetComponent<Canvas>();
        toolbarLayout.enabled = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        /*if (GameManager.Instance.currentMainTool != MainTool.none)
        {
            GameManager.Instance.currentMainTool = MainTool.none;
            currentTransformTool = TransformTool.none;
            lastTransformTool = TransformTool.none;

        }*/

        // one message only for each tool
        if (GameManager.Instance.activeGameObject == null)
        {
            /*Debug.Log("No Game Object Selected");*/
            return;
        }
        else
        {
            toolbarLayout.enabled = true;
            HandleTransformationToolSwitch();
            HandleTransformationToolBar();    
        }

    }

    // ============================================================================
    // Getter and Setter
    //getter and setter for tool Type
    public void setCurrentTool(Tool tool)
    {
        GameManager.Instance.currentTool = tool;
    }
    public Tool getCurrentTool()
    {
        return GameManager.Instance.currentTool;
    }

    public void setLastTool(Tool tool)
    {
        GameManager.Instance.lastTool = tool;
    }
    public Tool getLastTool()
    {
        return GameManager.Instance.lastTool;
    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        graspButton = GameObject.Find("GraspButton").GetComponent<Button>();
        rotateButton = GameObject.Find("RotateButton").GetComponent<Button>();
        scaleButton = GameObject.Find("ScaleButton").GetComponent<Button>();
        deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();

        graspButton.onClick.AddListener(() => setCurrentTool(Tool.grasp));
        rotateButton.onClick.AddListener(() => setCurrentTool(Tool.rotate));
        scaleButton.onClick.AddListener(() => setCurrentTool(Tool.scale));
        deleteButton.onClick.AddListener(() => setCurrentTool(Tool.delete));
    }

    public void HandleTransformationToolSwitch()
    {
        Tool currentTool = getCurrentTool();
        Tool lastTool = getLastTool();

        if (currentTool != lastTool)
        {
            Debug.Log("Current Tool: " + currentTool);

            if (currentTool == Tool.grasp)
            {
                //Operating A Game Object Grasp
                Debug.Log("Translation Tranformation");
                userGrasp = new UserGrasp();
                userGrasp.unlockPosition();
            }
            else if (currentTool == Tool.rotate)
            {
                //Operating A Game Object Rotation
                Debug.Log("Rotate Tranformation");
                userRotation = new UserRotation();
                userRotation.UnlockRotation();

            }
            else if (currentTool == Tool.scale)
            {
                //Operating A Game Object Scaling
                Debug.Log("Scale Tranformation");
                userScale = new UserScale();
                userScale.UnlockScale();


            }
            else if (currentTool == Tool.delete)
            {
                //Operating A Game Object Scaling
                Debug.Log("Delete Game Object");
                userDeletion = new UserDeletion();
            }

            // find the nearby object clicked from the mouse position aka input
            setLastTool(currentTool);
        }
    }

    public void HandleTransformationToolBar()
    {
        Tool currentTool = getCurrentTool();
        if (currentTool == Tool.grasp)
        {
            //Operating A Game Object Grasp
            if(userGrasp.meshPositionLock == false)
            {
                userGrasp.HandleUserGrasp();
            }

        }
        else if (currentTool == Tool.rotate)
        {
            //Operating A Game Object Rotation
            if (userRotation.meshRotationLock == false)
            {
                userRotation.HandleUserRotation();
            }
        }
        else if (currentTool == Tool.scale)
        {
            //Operating A Game Object Scaling
            if (userScale.meshScaleLock == false)
            {
                userScale.HandleUserScale();
            }
        }
        else if (currentTool == Tool.delete)
        {
            //Deleting The Game Object
            userDeletion.HandleDeletion();
        }

    }        
}


