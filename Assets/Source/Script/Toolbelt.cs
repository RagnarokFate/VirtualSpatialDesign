using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor.ProBuilder;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;


public enum Tool { select, deselect, grasp, rotate, scale, brush, delete, none };


public class ToolBelt : MonoBehaviour
{
    [SerializeField]
    private Tool currentTool = Tool.none;
    private Tool lastTool = Tool.none;


    public Button selectButton;
    public Button deselectButton;
    public Button graspButton;
    public Button rotateButton;
    public Button scaleButton;
    public Button brushButton;
    public Button deleteButton;

    [SerializeField]
    private Canvas brushKitLayout;

    // Start is called before the first frame update
    void Start()
    {
        lastTool = Tool.none;
        currentTool = Tool.none;
        brushKitLayout = GameObject.Find("BrushKitLayout").GetComponent<Canvas>();
        brushKitLayout.enabled = false;
        //set Brush tools visible to false at the start
    }

    // Update is called once per frame
    void Update()
    {
        Tool currentTool = getCurrentTool();
        if (currentTool != lastTool)
        {
            Debug.Log("Current Tool: " + currentTool);
            // find the nearby object clicked from the mouse position aka input


            // NEW TOOL
            /*Vector3 mousePosition = Input.mousePosition;
            Debug.Log("Mouse Position: " + mousePosition);*/
            // if the current tool is brush
            if (currentTool == Tool.select)
            {
                brushKitLayout.enabled = false;
                //Operating A Game Object Selection

            }
            else if(currentTool == Tool.deselect)
            {
                brushKitLayout.enabled = false;
                //Operating A Game Object Deselection
            }
            else if (currentTool == Tool.grasp)
            {
                brushKitLayout.enabled = false;
                //Operating A Game Object Grasp
            }
            else if (currentTool == Tool.rotate)
            {
                brushKitLayout.enabled = false;
                //Operating A Game Object Rotation
            }
            else if (currentTool == Tool.scale)
            {
                brushKitLayout.enabled = false;
                //Operating A Game Object Scaling
            }
            else
            if (currentTool == Tool.brush)
            {
                //Operating A Game Object Manipulation
                //set Brush tools visible to true
                //there is a canvas of name "BrushKitLayout" which contains all the brush tools like draw, extrude, bevel, cut. set it visible to true
                brushKitLayout.enabled = true;

            }                

            lastTool = currentTool;
            
        }

    }


   

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        selectButton = GameObject.Find("SelectButton").GetComponent<Button>();
        deselectButton = GameObject.Find("DeselectButton").GetComponent<Button>();
        graspButton = GameObject.Find("GraspButton").GetComponent<Button>();
        rotateButton = GameObject.Find("RotateButton").GetComponent<Button>();
        scaleButton = GameObject.Find("ScaleButton").GetComponent<Button>();
        brushButton = GameObject.Find("BrushTool").GetComponent<Button>();
        deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();

        selectButton.onClick.AddListener(() => setCurrentTool(Tool.select));
        deselectButton.onClick.AddListener(() => setCurrentTool(Tool.deselect));
        graspButton.onClick.AddListener(() => setCurrentTool(Tool.grasp));
        rotateButton.onClick.AddListener(() => setCurrentTool(Tool.rotate));
        scaleButton.onClick.AddListener(() => setCurrentTool(Tool.scale));
        brushButton.onClick.AddListener(() => setCurrentTool(Tool.brush));
        deleteButton.onClick.AddListener(() => setCurrentTool(Tool.delete));
    }




    //getter and setter for tool Type
    public Tool getCurrentTool()
    {
        return currentTool;
    }
    public void setCurrentTool(Tool tool)
    {
        currentTool = tool;
    }
    
}


