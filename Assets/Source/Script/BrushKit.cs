using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ProBuilder;
using System.Linq;
using UnityEngine.ProBuilder.MeshOperations;
using System.Drawing;
public enum BrushTool { draw, extrude, cut, none };

public class BrushKit : MonoBehaviour
{
    [SerializeField]
    private BrushTool currentBrushTool = BrushTool.none;
    private BrushTool lastBrushTool = BrushTool.none;

    public Button DrawButton;
    public Button ExtrudeButton;
    public Button CutButton;

    public Camera mainCamera;

    //Mesh Manipulation variables

    public GameObject pointPrefab;

    private Mesh mesh;
    ProBuilderMesh pbMesh;
    List<Vector3> points = new List<Vector3>();
    List<Face> faces = new List<Face>();

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
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Clicked");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("Hit Point: " + hit.point);
                    points.Add(hit.point);
                    pbMesh.ToMesh();
                    pbMesh.Refresh();
                    pbMesh.positions = points;

                    Instantiate(pointPrefab, hit.point, Quaternion.identity);

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
}
