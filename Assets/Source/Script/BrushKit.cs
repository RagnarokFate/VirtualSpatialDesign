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
    private DrawLine drawLine;
    ProBuilderMesh pbMesh;
    List<Vector3> vertices = new List<Vector3>();

    List<Vector3> centers = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject drawLineObject = new GameObject("DrawLineObject");
        drawLine = drawLineObject.AddComponent<DrawLine>();

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
                GameObject gameObject = new GameObject();
                Vector3 centerVertex = new Vector3(0, 0, 0);
                //Draw a new object
                if (currentDrawObject == DrawObject.Point)
                {
                    Point point = new Point(vertices[vertices.Count - 1],pointPrefab);
                    gameObject = point.CreatePoint();
                }
                if (currentDrawObject == DrawObject.Quad)
                {
                    Quad quad = new Quad(vertices);
                    gameObject = quad.CreateQuad();
                }
                else if (currentDrawObject == DrawObject.Rectangle)
                {
                    Rectangle rectangle = new Rectangle(vertices);
                    gameObject = rectangle.CreateRectangle();

                }
                else if (currentDrawObject == DrawObject.Polygon)
                {
                    Polygon polygon = new Polygon(vertices);
                    gameObject = polygon.CreatePolygon();
                }
                else
                {
                    //nothing
                }
                // COMPONENTS TO ADD TO THE OBJECT WITH DEFAULT VALUES
                // AddRigidBody(gameObject);
                // AddCollider(gameObject);
                MeshUtils meshUtils = new MeshUtils(gameObject);
                centerVertex = meshUtils.GetCenter();
                centers.Add(centerVertex);
                GameObjectsSingleton.Instance.AddGameObject(gameObject);
                drawLine.DestroyLine();
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

                    drawLine.UpdateLine(point_pos);
                }
                               
            }



        }

        if (currentBrushTool == BrushTool.extrude)
        {
            Debug.Log("Extrude Tool, Choose Vertix(V)/Edge(E)/Face(F)");
            SelectingObject currentSelectingObject = SelectingObject.none;
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("Vertix Selected");
                currentSelectingObject = SelectingObject.Vertex;

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Edge Selected");
                currentSelectingObject = SelectingObject.Edge;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Face Selected");
                currentSelectingObject = SelectingObject.Face;
            }

            // select a SelectingObject from the object
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Clicked - Extrude");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 offset = new Vector3(0, 0.11f, 0);
                    Vector3 point_pos = hit.point - offset;
                    // print the point position and it's index in vertices array
                    Debug.Log("Point Position: " + point_pos + "in Extrude Operation");
                    

                }
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

    //default add rigid body to the object
    public void AddRigidBody(GameObject gameObject)
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 1.0f;
        rb.drag = 0.1f;
        rb.angularDrag = 0.05f;
        rb.useGravity = false;
        rb.isKinematic = false;

        

        //print the center of mass of the object
        Debug.Log("Center of Mass: " + rb.centerOfMass);
        // NOTE : The center of mass is the point at which the object is ZERO!.
    }

    //default add collider to the object
    public void AddCollider(GameObject gameObject)
    {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
    }



}
