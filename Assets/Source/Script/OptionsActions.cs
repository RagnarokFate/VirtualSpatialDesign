using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class OptionsActions : MonoBehaviour
{
    // Options List - Toggle
    private bool vertexToggle = false;
    private bool gridToggle = false;

    public GameObject DotPrefab;

    private GameObject vertixRootParent;
    private GameObject gridRootParent;

    public GameObject OptionsPanelGameObject;

    // Start is called before the first frame update
    void Start()
    {
        this.vertixRootParent = new GameObject("Vertices");
        this.gridRootParent = new GameObject("Grid");
    }

   public void ToggleSubdiviSion()
   {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
            if (mesh != null)
            {
                //mesh.Subdivide();
            }
        }
   }


    public void ToggleFlipEdges()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
            if (mesh != null)
            {
                //mesh.FlipEdges();
            }
        }
    }
    public void ToggleFlipFaces()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
            if (mesh != null)
            {
                //mesh.FlipFaces();
            }
        }
    }

    public void Toggle_Triangualte_ToQuad()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
            if (mesh != null)
            {
                //mesh.ToMesh();
            }
        }
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
}
