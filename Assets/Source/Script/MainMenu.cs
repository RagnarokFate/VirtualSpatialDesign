using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
	private MainTool currentMainTool = MainTool.none;
    private MainTool lastMainTool = MainTool.none;

    private Button selectButton;
    private Button deselectButton;
    private Button insertButton;
    private Button drawButton;


    private DrawLine drawLine;


    //function profiles
    public UserSelection userSelection;
    public UserDeselection userDeselection;
    public UserDrawment userDrawment;
    public UserInsertion userInsertion;

    [SerializeField]
    private DrawObject drawObject = DrawObject.Polygon;
    [SerializeField]
    private Shape3DType shapeType = Shape3DType.None;
    static List<Vector3> vertices = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
	{
        GameObject drawLineObject = new GameObject("DrawLineObject");
        drawLine = drawLineObject.AddComponent<DrawLine>();

        userSelection = new UserSelection();
        userDeselection = new UserDeselection();
        userDrawment = new UserDrawment();
        userInsertion = new UserInsertion();

}

	// Update is called once per frame
	void Update()
	{
        if (currentMainTool != lastMainTool)
        {
            Debug.Log("Current Main Tool : " + currentMainTool);
            if(currentMainTool == MainTool.select)
            {
                Debug.Log("Select Tool is enabled");
            }
            else if(currentMainTool == MainTool.deselect)
            {
                Debug.Log("Deselect Tool is enabled");
            }
            else if(currentMainTool == MainTool.insert)
            {
                Debug.Log("Insert Tool is enabled");
                // insert 3d objects presents of probuilder to scene

            }
            else if(currentMainTool == MainTool.draw)
            {
                Debug.Log("Draw Tool is enabled");
                // draw 2d objects presents of probuilder to scene
            }
            else
            {
                Debug.Log("No Tool is enabled");
            }
            lastMainTool = currentMainTool;
        }

        HandleMainMenu();

    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        selectButton = GameObject.Find("SelectButton").GetComponent<Button>();
        deselectButton = GameObject.Find("DeselectButton").GetComponent<Button>();
        insertButton = GameObject.Find("InsertButton").GetComponent<Button>();
        drawButton = GameObject.Find("DrawButton").GetComponent<Button>();

        selectButton.onClick.AddListener(() => setCurrentMainTool(MainTool.select));
        deselectButton.onClick.AddListener(() => setCurrentMainTool(MainTool.deselect));
        insertButton.onClick.AddListener(() => setCurrentMainTool(MainTool.insert));
        drawButton.onClick.AddListener(() => setCurrentMainTool(MainTool.draw));

    }

    public void HandleMainMenu()
    {
        if (currentMainTool == MainTool.select)
        {
            // select tool is enabled

        }
        else if (currentMainTool == MainTool.deselect)
        {
            // deselect tool is enabled
        }
        else if (currentMainTool == MainTool.insert)
        {
            // insert tool is enabled
            userInsertion.SetShapeType(shapeType);
            userInsertion.HandleInsertion();
        }
        else if (currentMainTool == MainTool.draw)
        {
            // draw tool is enabled
            userDrawment.SetDrawObject(drawObject);
            userDrawment.HandleDrawment();

        }
        else
        {
            // no tool is enabled
            // INITAL CASE!
        }

    }  



    // ========================================================================================
    // Getters and Setters
    public void setCurrentMainTool(MainTool tool)
    {
        currentMainTool = tool;
    }
}