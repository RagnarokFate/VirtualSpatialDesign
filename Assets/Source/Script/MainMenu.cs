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
    public static DrawObject drawObject = DrawObject.none;
    [SerializeField]
    public static Shape3DType shapeType = Shape3DType.None;
    static List<Vector3> vertices = new List<Vector3>();

    public Material highlightMaterial;
    public Material selectionMaterial;

    private GameObject TextDecayGameObject;
    private Canvas MainMenuLayout;

    private PocketPanelView pocketPanelView;

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

        MainMenuLayout = GameObject.Find("MainMenuLayout").GetComponent<Canvas>();

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

        selectButton.onClick.AddListener(() => CheckMainToolSwitch(Tool.select));
        deselectButton.onClick.AddListener(() => CheckMainToolSwitch(Tool.deselect));
        insertButton.onClick.AddListener(() => CheckMainToolSwitch(Tool.insert));
        drawButton.onClick.AddListener(() => CheckMainToolSwitch(Tool.draw));
        measureButton.onClick.AddListener(() => CheckMainToolSwitch(Tool.measure));

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

    public void CheckMainToolSwitch(Tool tool)
    {
        if (getCurrentTool() != tool)
        {
            setCurrentTool(tool);
        }
        else
        {
            Debug.Log("Main tool has been set to `none`");
            setCurrentTool(Tool.none);
        }
    }

    // ========================================================================================


    public void HandleToolSwitch()
    {
        Tool currentTool = getCurrentTool();
        Tool lastTool = getLastTool();

        if (currentTool != lastTool)
        {
            TextDecayGameObject = new GameObject("TextDecayGameObject");
            FadeOutText fadeOutText = TextDecayGameObject.AddComponent<FadeOutText>();
            Debug.Log("Current Main Tool : " + currentTool);
            if (currentTool == Tool.select)
            {
                FadeOutText.Show(2f, Color.green, "Select Tool is enabled", new Vector2(0, 400), MainMenuLayout.transform);
            }
            else if (currentTool == Tool.deselect)
            {
                FadeOutText.Show(2f, Color.green, "Deselect Tool is enabled", new Vector2(0, 400), MainMenuLayout.transform);
            }
            else if (currentTool == Tool.insert)
            {
                // Insert 3d objects presents of probuilder to scene
                FadeOutText.Show(2f, Color.green, "3D Insert Tool is enabled", new Vector2(0, 400), MainMenuLayout.transform);
            }
            else if (currentTool == Tool.draw)
            {
                // draw 2d objects presents of probuilder to scene
                FadeOutText.Show(2f, Color.green, "2D Draw Tool is enabled", new Vector2(0, 400), MainMenuLayout.transform);

            }
            else if (currentTool == Tool.measure)
            {
                FadeOutText.Show(2f, Color.green, "Measure Tool is enabled", new Vector2(0, 400), MainMenuLayout.transform);

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