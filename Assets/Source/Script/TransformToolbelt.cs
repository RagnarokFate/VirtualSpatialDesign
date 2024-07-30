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

public class ToolBelt : MonoBehaviour
{
    //[SerializeField]
    private TransformTool currentTransformTool = TransformTool.none;
    private TransformTool lastTransformTool = TransformTool.none;
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
    // Start is called before the first frame update
    void Start()
    {
        toolbarLayout = GameObject.Find("ToolbarLayout").GetComponent<Canvas>();
        toolbarLayout.enabled = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        // one message only for each tool
        if(GameManager.Instance.activeGameObject == null)
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
    public TransformTool getCurrentTransformationTool()
    {
        return currentTransformTool;
    }
    public void setCurrentTransformationTool(TransformTool tool)
    {
        currentTransformTool = tool;
    }


    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        graspButton = GameObject.Find("GraspButton").GetComponent<Button>();
        rotateButton = GameObject.Find("RotateButton").GetComponent<Button>();
        scaleButton = GameObject.Find("ScaleButton").GetComponent<Button>();
        deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();

        graspButton.onClick.AddListener(() => setCurrentTransformationTool(TransformTool.grasp));
        rotateButton.onClick.AddListener(() => setCurrentTransformationTool(TransformTool.rotate));
        scaleButton.onClick.AddListener(() => setCurrentTransformationTool(TransformTool.scale));
        deleteButton.onClick.AddListener(() => setCurrentTransformationTool(TransformTool.delete));
    }

    public void HandleTransformationToolSwitch()
    {
        if (currentTransformTool != lastTransformTool)
        {
            GameManager.Instance.currentMainTool = MainTool.none;
            GameManager.Instance.currentBrushTool = BrushTool.none;

            Debug.Log("Current Tool: " + currentTransformTool);

            if (currentTransformTool == TransformTool.grasp)
            {
                //Operating A Game Object Grasp
                Debug.Log("Translation Tranformation");
                userGrasp = new UserGrasp();
                userGrasp.unlockPosition();
            }
            else if (currentTransformTool == TransformTool.rotate)
            {
                //Operating A Game Object Rotation
                Debug.Log("Rotate Tranformation");
                userRotation = new UserRotation();
                userRotation.unlockRotation();

            }
            else if (currentTransformTool == TransformTool.scale)
            {
                //Operating A Game Object Scaling
                Debug.Log("Scale Tranformation");

            }
            else if (currentTransformTool == TransformTool.delete)
            {
                //Operating A Game Object Scaling
                Debug.Log("Delete Game Object");

            }

            // find the nearby object clicked from the mouse position aka input
            lastTransformTool = currentTransformTool;
            GameManager.Instance.SetCurrentTransformTool(currentTransformTool);
        }
    }

    public void HandleTransformationToolBar()
    {
        
        if (currentTransformTool == TransformTool.grasp)
        {
            //Operating A Game Object Grasp
            if(userGrasp.meshPositionLock == false)
            {
                userGrasp.HandleUserGrasp();
            }

        }
        else if (currentTransformTool == TransformTool.rotate)
        {
            //Operating A Game Object Rotation
            if (userRotation.meshRotationLock == false)
            {
                userRotation.HandleUserRotation();
            }
        }
        else if (currentTransformTool == TransformTool.scale)
        {
            //Operating A Game Object Scaling
        }
        else if (currentTransformTool == TransformTool.delete)
        {
            //Deleting The Game Object
        }

    }        
}


