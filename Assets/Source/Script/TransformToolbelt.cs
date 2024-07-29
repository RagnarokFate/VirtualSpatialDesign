using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor.ProBuilder;
using UnityEngine;
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
        if (currentTransformTool != lastTransformTool)
        {
            Debug.Log("Current Tool: " + currentTransformTool);
            
            if (currentTransformTool == TransformTool.grasp)
            {
                //Operating A Game Object Grasp
                Debug.Log("Translation Tranformation");

            }
            else if (currentTransformTool == TransformTool.rotate)
            {
                //Operating A Game Object Rotation
                Debug.Log("Rotate Tranformation");

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
        processToolSwitch();

    }

    // ============================================================================
    // Getter and Setter
    //getter and setter for tool Type
    public TransformTool getCurrentTool()
    {
        return currentTransformTool;
    }
    public void setCurrentTool(TransformTool tool)
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

        graspButton.onClick.AddListener(() => setCurrentTool(TransformTool.grasp));
        rotateButton.onClick.AddListener(() => setCurrentTool(TransformTool.rotate));
        scaleButton.onClick.AddListener(() => setCurrentTool(TransformTool.scale));
        deleteButton.onClick.AddListener(() => setCurrentTool(TransformTool.delete));
    }


    public void processToolSwitch()
    {
        
        if (currentTransformTool == TransformTool.grasp)
        {
            //Operating A Game Object Grasp
        }
        else if (currentTransformTool == TransformTool.rotate)
        {
            //Operating A Game Object Rotation
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


