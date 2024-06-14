using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ProBuilder;
using System.Linq;
using UnityEngine.ProBuilder.MeshOperations;
using System.Drawing;
using System.Security.Cryptography;
public enum BrushTool { draw, extrude, cut, none };
public enum DrawObject {point, Quad, Rectangle, none }
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
    List<Vector3> points = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        pbMesh = ProBuilderMesh.Create();
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
                //Draw a new object
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

            if (points.Count == 2)
            {
                if (currentDrawObject == DrawObject.Quad)
                {
                    CreateQuad();

                }
                else if (currentDrawObject == DrawObject.Rectangle)
                {
                    CreateRectangle();

                }
                else
                {
                }



            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Clicked");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 offset = new Vector3(0, 0.11f, 0);
                    Vector3 point_pos = hit.point - offset;
                    // print the point position and it's index in points array
                    Debug.Log("Point Index: " + points.Count + "Point Position: " + point_pos);
                    points.Add(point_pos);
                    pbMesh.ToMesh();
                    pbMesh.Refresh();
                    pbMesh.positions = points;

                    Instantiate(pointPrefab, hit.point, Quaternion.identity);
                    /*if (currentDrawObject == DrawObject.point)
                    {
                        Instantiate(pointPrefab, hit.point, Quaternion.identity);
                    }*/
                }
            }

        }

        if (currentBrushTool == BrushTool.extrude)
        {

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
    public void DrawConvex()
    {
        //create a loop that inserts vertices to vertices array. the inserted vertices are obtained by one click mouse position and stops when the player click spacebar
        
        Debug.Log("Draw Convex");
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
    void CreateShape()
    {
        pbMesh.Clear(); // Clear previous shape
        pbMesh.positions = points;
        List<int> indices = new List<int>();

        // Create indices for the shape
        for (int i = 0; i < points.Count; i++)
        {
            indices.Add(i);
            indices.Add((i + 1) % points.Count); // Connect the last point to the first point
        }

        // Create a face from the indices
        pbMesh.faces = new List<Face> { new Face(indices.ToArray()) };
        pbMesh.ToMesh();
        pbMesh.Refresh();
    }

    void CreateQuad()
    {
        Debug.Log("Create Quad");
        Vector3 point1 = points[0];
        Vector3 point2 = points[1];

        // Define the other two points to form a quad
        Vector3 direction = point2 - point1;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * direction.magnitude;

        Vector3 point3 = point1 + perpendicular;
        Vector3 point4 = point2 + perpendicular;

        // Create a list of quad points
        List<Vector3> quadPoints = new List<Vector3> { point1, point2, point4, point3 };

        pbMesh.Clear(); // Clear previous shape
        pbMesh.positions = quadPoints;
        List<int> indices = new List<int>
        {
            2, 1, 0, // First triangle
            0, 3, 2  // Second triangle
        };

        // Create a face from the indices
        pbMesh.faces = new List<Face> { new Face(indices.ToArray()) };
        pbMesh.SetMaterial(pbMesh.faces, QuadMaterial);
        pbMesh.ToMesh();
        pbMesh.Refresh();

        points.Clear();
    }

    void CreateRectangle()
    {
        Debug.Log("Create Rectangle");
        Vector3 point1 = points[0];
        Vector3 point2 = new Vector3(point1.x, point1.y, points[1].z);
        Vector3 point3 = points[1];
        Vector3 point4 = new Vector3(points[1].x, points[1].y, point1.z);

        // Create a list of rectangle points
        List<Vector3> quadPoints = new List<Vector3> { point1, point2, point3, point4 };

        pbMesh.Clear(); // Clear previous shape
        pbMesh.positions = quadPoints;
        List<int> indices = new List<int>
        {
            2, 1, 0, // First triangle
            0, 3, 2  // Second triangle
        };

        // Create a face from the indices
        pbMesh.faces = new List<Face> { new Face(indices.ToArray()) };
        pbMesh.ToMesh();
        pbMesh.Refresh();

        points.Clear();

    }
}
