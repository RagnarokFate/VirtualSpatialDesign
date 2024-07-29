using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ProBuilder;
using System.Linq;
using UnityEngine.ProBuilder.MeshOperations;
using System;
using UnityEditor.ProBuilder;



public class BrushKit : MonoBehaviour
{
    [SerializeField]
    private BrushTool currentBrushTool = BrushTool.none;
    private BrushTool lastBrushTool = BrushTool.none;


    private Button ExtrudeButton;
    private Button CutButton;
    // ADD MORE LATER TODO : BEVEL ETC...


    private Canvas brushKitLayout;


    // ProBuilderMesh pbMesh;


    // Start is called before the first frame update
    void Start()
    {
        brushKitLayout = GameObject.Find("BrushKitLayout").GetComponent<Canvas>();
        brushKitLayout.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        BrushTool currentBrushTool = getCurrentBrushTool();
        if (currentBrushTool != lastBrushTool)
        {
            Debug.Log("Current Brush Tool: " + currentBrushTool);

            // 1 time message for the user!
            if (currentBrushTool == BrushTool.extrude)
            {
                //Extrude a selected object
                /*GameObject ActiveGameObject = GameManager.Instance.activeGameObject;
                Debug.Log("Selected Game Object is " + ActiveGameObject.name);*/

                Debug.Log("Extrude Tool, Choose Vertix(V)/Edge(E)/Face(F)");
            }
            else if (currentBrushTool == BrushTool.cut)
            {
                //Cut a selected object
            }
            else
            {

                //Do nothing
                //EXPORT

            }
            lastBrushTool = currentBrushTool;
            GameManager.Instance.SetCurrentBrushTool(currentBrushTool);
        }
       
        
        

        

    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        ExtrudeButton = GameObject.Find("ExtrudeButton").GetComponent<Button>();
        CutButton = GameObject.Find("CuttingButton").GetComponent<Button>();

        ExtrudeButton.onClick.AddListener(() => setCurrentBrushTool(BrushTool.extrude));
        CutButton.onClick.AddListener(() => setCurrentBrushTool(BrushTool.cut));

    }

    //
    

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
        // meshCollider.convex = true;
    }


    // handle the object extrude mode
    public SelectingMode HandleExtrudeMode()
    {
        // select the object
        SelectingMode currentSelectingMode = SelectingMode.none;

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Vertix Selected");
            currentSelectingMode = SelectingMode.Vertex;

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Edge Selected");
            currentSelectingMode = SelectingMode.Edge;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Face Selected");
            currentSelectingMode = SelectingMode.Face;
            ExtrudeFaces();

        }
        else
        {
            currentSelectingMode = SelectingMode.none;
        }

        return currentSelectingMode;
    }



    public void ExtrudeFaces()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject == null)
        {
            Debug.LogError("No Game Object Selected");
            return;
        }

        ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();

        // Ensure we have a valid ProBuilder mesh
        if (pbMesh == null)
        {
            Debug.LogError("No ProBuilderMesh component found on this GameObject.");
            return;
        }

        // Ensure the mesh is valid
        if (!pbMesh)
        {
            Debug.LogError("ProBuilderMesh is not valid.");
            return;
        }

        // Ensure there are faces to extrude
        if (pbMesh.faces == null || pbMesh.faces.Count == 0)
        {
            Debug.LogError("No faces found in the ProBuilderMesh.");
            return;
        }

        // Debug: Log the face indices
        foreach (var face in pbMesh.faces)
        {
            Debug.Log($"Face Index: {face.indexes.ToString()}");
        }

        // Define the extrude distance
        float extrudeDistance = 10.0f;

        try
        {
            // Perform the extrusion
            pbMesh.faces = ExtrudeElements.Extrude(pbMesh, pbMesh.faces, ExtrudeMethod.FaceNormal, extrudeDistance);
            pbMesh.ToMesh();
            pbMesh.Refresh();
        }
        catch (KeyNotFoundException knfe)
        {
            Debug.LogError($"Extrusion failed due to a missing key in the dictionary: {knfe.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Extrusion failed: {e.Message}");
        }
    }


    /*public void ExtrudeFaces()
    {
        // Create a new cube primitive
        var mesh = ShapeGenerator.CreateShape(ShapeType.Cube);

        // Extrude the first available face along it's normal direction by 1 meter.
        mesh.Extrude(new Face[] { mesh.faces.First() }, ExtrudeMethod.FaceNormal, 1f);

        // Apply the changes back to the `MeshFilter.sharedMesh`.
        // 1. ToMesh cleans the UnityEngine.Mesh and assigns vertices and sub-meshes.
        // 2. Refresh rebuilds generated mesh data, ie UVs, Tangents, Normals, etc.
        // 3. (Optional, Editor only) Optimize merges coincident vertices, and rebuilds lightmap UVs.
        mesh.ToMesh();
        mesh.Refresh();
        mesh.Optimize();
    }*/





}
