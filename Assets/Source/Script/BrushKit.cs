using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ProBuilder;
using System.Linq;
using UnityEngine.ProBuilder.MeshOperations;

public enum BrushTool { draw, extrude, cut, none };
public enum DrawObject {Point, Quad, Rectangle, Polygon ,none }
public class BrushKit : MonoBehaviour
{
    [SerializeField]
    private BrushTool currentBrushTool = BrushTool.none;
    private BrushTool lastBrushTool = BrushTool.none;

    [SerializeField]
    private DrawObject currentDrawObject = DrawObject.none;

    public Button DrawButton;
    public Button ExtrudeButton;
    public Button CutButton;

    public Camera mainCamera;

    //Mesh Manipulation variables

    public GameObject pointPrefab;
    public Material QuadMaterial;

    ProBuilderMesh pbMesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> indices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        pbMesh = ProBuilderMesh.Create();
        QuadMaterial = new Material(Shader.Find("Standard"));
        lastBrushTool = BrushTool.none;
        currentBrushTool = BrushTool.none;
    }

    // Update is called once per frame
    void Update()
    {
        BrushTool currentBrushTool = getCurrentBrushTool();
        if (currentBrushTool != lastBrushTool)
        {
            Debug.Log("Current Brush Tool: " + currentBrushTool);
            
            if(currentBrushTool == BrushTool.draw)
            {
                
            }
            else if(currentBrushTool == BrushTool.extrude)
            {
                //Extrude a selected object
            }
            else if(currentBrushTool == BrushTool.cut)
            {
                //Cut a selected object
            }
            else
            {
                
                //Do nothing
                //EXPORT

            }
            lastBrushTool = currentBrushTool;
        }



        if (currentBrushTool == BrushTool.draw)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //Draw a new object
                if (currentDrawObject == DrawObject.Point)
                {
                    Point point = new Point(vertices[vertices.Count - 1],pointPrefab);
                    point.CreatePoint();
                }
                if (currentDrawObject == DrawObject.Quad)
                {
                    Quad quad = new Quad(vertices);
                    quad.CreateQuad();
                }
                else if (currentDrawObject == DrawObject.Rectangle)
                {
                    Rectangle rectangle = new Rectangle(vertices);
                    rectangle.CreateRectangle();

                }
                else if (currentDrawObject == DrawObject.Polygon)
                {
                    Polygon polygon = new Polygon(vertices);
                    polygon.CreatePolygon();
                }
                else
                {
                    //nothing
                }
                vertices.Clear();
            }

            //selecting vertices!
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Clicked");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 offset = new Vector3(0, 0.11f, 0);
                    Vector3 point_pos = hit.point - offset;
                    // print the point position and it's index in vertices array
                    Debug.Log("Point Index: " + vertices.Count + "Point Position: " + point_pos);
                    vertices.Add(point_pos);
                    
                }
                if (vertices.Count > 3 && currentDrawObject == DrawObject.Polygon)
                {
                    Polygon polygon = new Polygon(vertices);
                    polygon.CreatePolygon();
                }

            }



        }

        if (currentBrushTool == BrushTool.extrude)
        {
            // select a face
            if (Input.GetMouseButtonDown(0))
            {
                //Extrude a selected object
            }
            // apply extrusion via right click
            if (Input.GetMouseButtonDown(1))
            {
                //Extrude a selected object
            }
        }
        if (currentBrushTool == BrushTool.cut)
        {
            // select a face
            if (Input.GetMouseButtonDown(0))
            {
                //Cut a selected object
            }
            // apply extrusion via right click
            if (Input.GetMouseButtonDown(1))
            {
                //Cut a selected object
            }
        }

    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        DrawButton = GameObject.Find("DrawButton").GetComponent<Button>();
        ExtrudeButton = GameObject.Find("ExtrudeButton").GetComponent<Button>();
        CutButton = GameObject.Find("CuttingButton").GetComponent<Button>();

        DrawButton.onClick.AddListener(() => setCurrentBrushTool(BrushTool.draw));
        ExtrudeButton.onClick.AddListener(() => setCurrentBrushTool(BrushTool.extrude));
        CutButton.onClick.AddListener(() => setCurrentBrushTool(BrushTool.cut));

    }


    // getter and setter for currentBrushTool
    public BrushTool getCurrentBrushTool()
    {
        return currentBrushTool;
    }
    public void setCurrentBrushTool(BrushTool brushTool)
    {
        currentBrushTool = brushTool;
    }



    // ======================================== 
   

    

    
}
