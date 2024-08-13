using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    private Button selectButton;
    private Button deselectButton;
    private Button insertButton;
    private Button drawButton;
    private Button measureButton;

    private DrawLine drawLine;


    //function profiles - MAIN MENU
    public UserSelection userSelection;
    public UserDeselection userDeselection;
    public UserDrawment userDrawment;
    public UserInsertion userInsertion;
    public UserMeasure userMeasure;


    [SerializeField]
    private DrawObject drawObject = DrawObject.none;
    [SerializeField]
    private Shape3DType shapeType = Shape3DType.None;
    static List<Vector3> vertices = new List<Vector3>();

    public Material highlightMaterial;
    public Material selectionMaterial;

    // Start is called before the first frame update
    void Start()
	{
        GameObject drawLineObject = new GameObject("DrawLineObject");
        // drawLine = drawLineObject.AddComponent<DrawLine>();
        //function profiles - MAIN MENU

        userSelection = new UserSelection(highlightMaterial, selectionMaterial);
        userDeselection = new UserDeselection();
        userDrawment = new UserDrawment();
        userInsertion = new UserInsertion();
        userMeasure = new UserMeasure();



    }

	// Update is called once per frame
	void Update()
	{

        HandleToolSwitch();
        HandleMainMenu();

    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        selectButton = GameObject.Find("SelectButton").GetComponent<Button>();
        deselectButton = GameObject.Find("DeselectButton").GetComponent<Button>();
        insertButton = GameObject.Find("InsertButton").GetComponent<Button>();
        drawButton = GameObject.Find("DrawButton").GetComponent<Button>();
        measureButton = GameObject.Find("MeasureButton").GetComponent<Button>();

        selectButton.onClick.AddListener(() => setCurrentTool(Tool.select));
        deselectButton.onClick.AddListener(() => setCurrentTool(Tool.deselect));
        insertButton.onClick.AddListener(() => setCurrentTool(Tool.insert));
        drawButton.onClick.AddListener(() => setCurrentTool(Tool.draw));
        measureButton.onClick.AddListener(() => setCurrentTool(Tool.measure));
    }


    // ========================================================================================
    // Getters and Setters
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

    public void HandleToolSwitch()
    {
        Tool currentTool = getCurrentTool();
        Tool lastTool = getLastTool();

        if (currentTool != lastTool)
        {

            Debug.Log("Current Main Tool : " + currentTool);
            if (currentTool == Tool.select)
            {
                Debug.Log("<color=blue> Select Tool is enabled </color>");
            }
            else if (currentTool == Tool.deselect)
            {
                Debug.Log("<color=blue> Deselect Tool is enabled </color>");
            }
            else if (currentTool == Tool.insert)
            {
                Debug.Log("<color=blue> Insert Tool is enabled </color>");
                // insert 3d objects presents of probuilder to scene

            }
            else if (currentTool == Tool.draw)
            {
                Debug.Log("<color=blue> 2D Draw Tool is enabled </color>");

                // draw 2d objects presents of probuilder to scene
            }
            else if (currentTool == Tool.measure)
            {
                Debug.Log("<color=blue> Measure Tool is enabled </color>");
            }
            else
            {
                Debug.Log("No Tool is enabled");
            }
            setLastTool(currentTool);
        }
    }

    public void HandleMainMenu()
    {
        Tool currentTool = getCurrentTool();
        Tool lastTool = getLastTool();

        if (currentTool == Tool.select)
        {
            // select tool is enabled
            userSelection.HandleHighlight();
            userSelection.HandleSelection();

        }
        else if (currentTool == Tool.deselect)
        {
            // deselect tool is enabled
            userDeselection.HandleDeselection();
        }
        else if (currentTool == Tool.insert)
        {
            // insert tool is enabled
            userInsertion.SetShapeType(shapeType);
            userInsertion.HandleInsertion();
        }
        else if (currentTool == Tool.draw)
        {
            // draw tool is enabled
            userDrawment.SetDrawObject(drawObject);
            userDrawment.HandleDrawment();

        }
        else if (currentTool == Tool.measure)
        {
            // measure tool is enabled
            userMeasure.HandleMeasure();
        }
        else
        {
            // no tool is enabled
            // INITAL CASE!
        }

    }  




}