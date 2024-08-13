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



public class EditorMenu : MonoBehaviour
{
    [SerializeField]
    private EditorTool currentBrushTool = EditorTool.none;
    private EditorTool lastBrushTool = EditorTool.none;


    private Button ExtrudeButton;
    private Button CutButton;
    // ADD MORE LATER TODO : BEVEL ETC...


    private Canvas editorMenu;

    // profile users interaction
    public UserExtrusion userExtrusion;

    // Start is called before the first frame update
    void Start()
    {
        editorMenu = GameObject.Find("EditorMenu").GetComponent<Canvas>();
        editorMenu.enabled = false;

        // profile users interaction
        userExtrusion = new UserExtrusion();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject == null)
        {
            /*Debug.LogError("No Game Object Selected");*/
            return;
        }
        else
        {
            if(GameManager.Instance.IsItAMesh(gameObject))
            {
                editorMenu.enabled = true;
            }
            else
            {
                editorMenu.enabled = false;
            }
        }

        EditorTool currentBrushTool = getCurrentBrushTool();
        HandleBrushKitSwitch();

        if (currentBrushTool == EditorTool.extrude)
        {
            userExtrusion.HandleExtrusion();
        }
        else if (currentBrushTool == EditorTool.cut)
        {
            //HandleCutMode();
        }
        else
        {
            //HandleBevelMode();
        }



    }

    // create a function that check if which button is clicked lastly
    public void OnEnable()
    {
        ExtrudeButton = GameObject.Find("ExtrudeButton").GetComponent<Button>();
        CutButton = GameObject.Find("CuttingButton").GetComponent<Button>();

        ExtrudeButton.onClick.AddListener(() => setCurrentBrushTool(EditorTool.extrude));
        CutButton.onClick.AddListener(() => setCurrentBrushTool(EditorTool.cut));

    }

    // ===================================================================================================================
    // getter and setter for currentBrushTool
    public EditorTool getCurrentBrushTool()
    {
        return currentBrushTool;
    }
    public void setCurrentBrushTool(EditorTool brushTool)
    {
        currentBrushTool = brushTool;
    }



    // ======================================== 
    public void HandleBrushKitSwitch()
    {
        EditorTool currentBrushTool = getCurrentBrushTool();

        if (currentBrushTool != lastBrushTool)
        {

            Debug.Log("Current Brush Tool: " + currentBrushTool);

            // 1 time message for the user!
            if (currentBrushTool == EditorTool.extrude)
            {
                Debug.Log("Extrude Tool, Choose Vertix(V)/Edge(E)/Face(F)");
                userExtrusion.UnlockExtrusion();
            }
            else if (currentBrushTool == EditorTool.cut)
            {
                //Cut a selected object
                Debug.Log("Cut Tool, Choose Vertix(V)/Edge(E)/Face(F)");
            }
            else
            {
            }
            lastBrushTool = currentBrushTool;
            GameManager.Instance.SetCurrentBrushTool(currentBrushTool);
        }
    }

    // handle the object extrude mode
/*    public SelectingMode HandleExtrudeMode()
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
    }*/



    /*public void ExtrudeFaces()
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
    }*/


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
