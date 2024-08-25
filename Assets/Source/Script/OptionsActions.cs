using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using System;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class OptionsActions : MonoBehaviour
{
    // Options List - Toggle
    private bool vertexToggle = false;
    private bool gridToggle = false;

    public GameObject DotPrefab;

    private GameObject vertixRootParent;
    private GameObject gridRootParent;

    public GameObject OptionsPanelGameObject;

    private static bool isTriangle = false;

    // Start is called before the first frame update
    void Start()
    {
        this.vertixRootParent = new GameObject("Vertices");
        this.gridRootParent = new GameObject("Grid");
    }

    public void ToggleSubdivision()
    {
        // 2. Check for active game object and its ProBuilderMesh component
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();
            if (pbMesh != null)
            {
                // Get original vertices and faces
                List<Vector3> originalVertices = new List<Vector3>(pbMesh.positions);
                List<int> originalTriangles = new List<int>();

                // Convert ProBuilder faces to triangle indices
                foreach (var face in pbMesh.faces)
                {
                    originalTriangles.AddRange(face.indexes);
                }

                // Subdivide the mesh
                List<Vector3> newVertices;
                List<int> newTriangles;
                SubdivideFaces(originalVertices, originalTriangles, out newVertices, out newTriangles);

                // Convert the triangle indices back into ProBuilder faces
                List<Face> newFaces = new List<Face>();
                for (int i = 0; i < newTriangles.Count; i += 3)
                {
                    newFaces.Add(new Face(new int[] { newTriangles[i], newTriangles[i + 1], newTriangles[i + 2] }));
                }

                // Rebuild the mesh with the new vertices and faces
                pbMesh.RebuildWithPositionsAndFaces(newVertices, newFaces);
                pbMesh.ToMesh();
                pbMesh.Refresh();
            }
        }

        // Close options panel (presumed part of UI logic)
        CloseOptionsPanel();
    }   


    public void ToggleFlipFaces()
    {
        Debug.Log("Flipping Faces");
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();
            if (pbMesh != null)
            {
                List<Face> newFaces = new List<Face>();
                foreach (Face face in pbMesh.faces)
                {
                    Face tempface = face;
                    tempface.Reverse();
                    newFaces.Add(tempface);
                }
                pbMesh.faces = newFaces;
                pbMesh.ToMesh();
                pbMesh.Refresh();
            }

        }
        CloseOptionsPanel();

    }

    public void ToggleFlipEdges()
    {
        Debug.Log("Flipping Edges");
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();
            if (pbMesh != null)
            {
                SurfaceTopology.ConformNormals(pbMesh, pbMesh.faces);
                pbMesh.ToMesh();
                pbMesh.Refresh();
                /*pbMesh.ConformNormals(pbMesh.faces);
                pbMesh.ToMesh();
                pbMesh.Refresh();*/
            }
        }
        CloseOptionsPanel();

    }
    public void Toggle_Triangualte_ToQuad()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;

        if (gameObject != null)
        {
            ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();
            if (pbMesh != null && !isTriangle)
            {
                Debug.Log("Triangualte");
                pbMesh.faces = SurfaceTopology.ToTriangles(pbMesh, pbMesh.faces);
                pbMesh.ToMesh();
                pbMesh.Refresh();
                isTriangle = true;
            }
            else
            {
                Debug.Log("To Quad");
                pbMesh.faces = QuadUtility.ToQuads(pbMesh, pbMesh.faces);
                pbMesh.ToMesh();
                pbMesh.Refresh();
                isTriangle = false;
            }
        }
        CloseOptionsPanel();

    }

    public void ToggleVertices()
    {
        vertexToggle = !vertexToggle;
        if (vertexToggle is true)
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (gameObject != null)
            {
                ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
                if (mesh != null)
                {
                    int counter = 0;
                    foreach (Vector3 vertex in mesh.positions)
                    {
                        GameObject vertexObject = GameObject.Instantiate(DotPrefab, vertex, Quaternion.identity);
                        vertexObject.name = "Vertex " + counter;
                        vertexObject.transform.SetParent(vertixRootParent.transform);
                    }
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (vertixRootParent != null && vertixRootParent.transform.childCount > 0)
            {
                GameObject.Destroy(vertixRootParent);
                this.vertixRootParent = new GameObject("Vertices");
            }
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
        }
        CloseOptionsPanel();
    }

    public void ToggleGrid()
    {
        gridToggle = !gridToggle;
        if (gridToggle is true)
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (gameObject != null)
            {
                ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
                if (mesh != null)
                {
                    // Parent GameObject to hold all face grids
                    GameObject gridParent = new GameObject("Grid GameObject Parent");
                    gridParent.transform.SetParent(gridRootParent.transform);

                    foreach (Face face in mesh.faces)
                    {
                        // Create a GameObject for each face
                        GameObject gridGameObject = new GameObject("Grid GameObject Face");
                        gridGameObject.transform.SetParent(gridParent.transform);

                        LineRenderer lineRenderer = gridGameObject.AddComponent<LineRenderer>();
                        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                        lineRenderer.startColor = Color.green;
                        lineRenderer.endColor = Color.green;
                        lineRenderer.startWidth = 0.01f;
                        lineRenderer.endWidth = 0.01f;
                        lineRenderer.loop = true;

                        // Set the positions for the vertices of the face
                        int vertexCount = face.indexes.Count;
                        lineRenderer.positionCount = vertexCount;
                        for (int j = 0; j < vertexCount; j++)
                        {
                            Vector3 vertex = mesh.positions[face.indexes[j]];
                            lineRenderer.SetPosition(j, vertex);
                        }
                    }

                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (gridRootParent != null && gridRootParent.transform.childCount > 0)
            {
                GameObject.Destroy(gridRootParent);
                this.gridRootParent = new GameObject("Grid");
            }
            gameObject.SetActive(true);
        }
        CloseOptionsPanel();
    }

    public void CloseOptionsPanel()
    {
        OptionsPanelGameObject.SetActive(false);
        OptionsActionsPanel.OptionsPanel = false;
    }




    // assistant functions
    // Function to subdivide a triangle into 4 smaller triangles
    // Updated function to subdivide a triangle into 4 smaller triangles
    public static void SubdivideTriangle(Vector3 v1, Vector3 v2, Vector3 v3, out List<Vector3> newVertices, out List<int> newTriangles)
    {
        newVertices = new List<Vector3>();
        newTriangles = new List<int>();

        // Calculate the midpoints of the triangle's edges
        Vector3 m1 = (v1 + v2) / 2f;
        Vector3 m2 = (v2 + v3) / 2f;
        Vector3 m3 = (v3 + v1) / 2f;

        // Add original vertices
        newVertices.Add(v1);
        newVertices.Add(v2);
        newVertices.Add(v3);

        // Add new vertices (midpoints)
        newVertices.Add(m1);
        newVertices.Add(m2);
        newVertices.Add(m3);

        // Create the 4 new triangles
        newTriangles.AddRange(new int[]
        {
            0, 3, 5, // Triangle 1
            3, 1, 5, // Triangle 2
            1, 4, 5, // Triangle 3
            4, 2, 5, // Triangle 4
        });
    }

    // Updated function to subdivide a quad into 4 smaller triangles
    public static void SubdivideQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, out List<Vector3> newVertices, out List<int> newTriangles)
    {
        newVertices = new List<Vector3>();
        newTriangles = new List<int>();

        // Divide the quad into two triangles and subdivide each
        SubdivideTriangle(v1, v2, v3, out List<Vector3> tri1Vertices, out List<int> tri1Triangles);
        SubdivideTriangle(v3, v4, v1, out List<Vector3> tri2Vertices, out List<int> tri2Triangles);

        // Merge the two sets of vertices and triangles
        newVertices.AddRange(tri1Vertices);
        int offset = newVertices.Count;
        newTriangles.AddRange(tri1Triangles);

        foreach (var vert in tri2Vertices)
        {
            if (!newVertices.Contains(vert))
            {
                newVertices.Add(vert);
            }
        }

        foreach (var tri in tri2Triangles)
        {
            newTriangles.Add(tri + offset);
        }
    }

    // Updated function to obtain overall new vertices and triangles for a list of faces
    public static void SubdivideFaces(List<Vector3> originalVertices, List<int> originalTriangles, out List<Vector3> newVertices, out List<int> newTriangles)
    {
        newVertices = new List<Vector3>();
        newTriangles = new List<int>();

        for (int i = 0; i < originalTriangles.Count; i += 3)
        {
            Vector3 v1 = originalVertices[originalTriangles[i]];
            Vector3 v2 = originalVertices[originalTriangles[i + 1]];
            Vector3 v3 = originalVertices[originalTriangles[i + 2]];

            SubdivideTriangle(v1, v2, v3, out List<Vector3> triVertices, out List<int> triTriangles);

            int offset = newVertices.Count;
            newVertices.AddRange(triVertices);

            foreach (var tri in triTriangles)
            {
                newTriangles.Add(tri + offset);
            }
        }
    }

}
