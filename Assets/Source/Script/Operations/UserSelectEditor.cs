using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UserSelectEditor
{
    private List<Vector3> vertices;
    private List<Vector2> edges;
    private List<Face> faces;

    public static bool isSelected = false;

    private Vector3 selectedVertex;
    private Vector2 selectedEdge;
    private Face selectedFace;

    //prefab
    private GameObject selectedVertexPrefab;

    private GameObject vertexGameObject;
    private GameObject edgeGameObject;
    private GameObject faceGameObject;

    public UserSelectEditor()
    {
        vertices = new List<Vector3>();
        edges = new List<Vector2>();
        faces = new List<Face>();
    }
    public UserSelectEditor(List<Vector3> vertices, List<Vector2> edges, List<Face> faces)
    {
        this.vertices = vertices;
        this.edges = edges;
        this.faces = faces;
    }
    public UserSelectEditor(ProBuilderMesh mesh)
    {
        vertices = mesh.positions.ToList();
        faces = mesh.faces.ToList();

        // edges = mesh.edgeCount > 0 ? mesh.edges.ToList() : new List<Vector2>();
        //edges = GenerateEdges(vertices, mesh.faces);
    }

    public void setProBuilderToUserSelectEditor()
    {
        ProBuilderMesh mesh = GameManager.Instance.activeGameObject.GetComponent<ProBuilderMesh>();
        if (mesh == null)
        {
            Debug.LogError("No ProBuilderMesh component found on this GameObject.");
            return;
        }
        vertices = mesh.positions.ToList();
        faces = mesh.faces.ToList();
    }

    public void setPrefabs(GameObject selectedVertexPrefab)
    {
        this.selectedVertexPrefab = selectedVertexPrefab;
    }

    public void HandleEditorSelection()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;
        if (GameManager.Instance.activeGameObject == null)
        {
            return;
        }
        ProBuilderMesh proBuilderMesh = gameObject.GetComponent<ProBuilderMesh>();
        if (proBuilderMesh == null)
        {
            Debug.LogError("No ProBuilderMesh component found on this GameObject.");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {

                HandleEditorSelectionOnPoint(hit.point);
            }
        }
    }

    public void HandleEditorSelectionOnPoint(Vector3 point)
    {
        selectedVertex = getCloestVertex(point);
        //selectedEdge = getCloestEdge(point);
        selectedFace = getCloestFace(point);

        ClearSelection();

        if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Vertex)
        {
            vertexGameObject = GameObject.Instantiate(selectedVertexPrefab, selectedVertex, Quaternion.identity);
            vertexGameObject.name = "Vertex";

            Debug.Log("Selected Vertex: " + selectedVertex);
            isSelected = true;
        }
        else if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Edge)
        {
            edgeGameObject = new GameObject("Edge");
            LineRenderer lineRenderer = edgeGameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, vertices[(int)selectedEdge.x]);
            lineRenderer.SetPosition(1, vertices[(int)selectedEdge.y]);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startColor = new Color32(70, 184, 163, 255);
            lineRenderer.endColor = new Color32(70, 184, 163, 255);
        }
        else if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Face)
        {
            faceGameObject = new GameObject("Face");
            LineRenderer lineRenderer = faceGameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = selectedFace.indexes.Count + 1;
            for (int i = 0; i < selectedFace.indexes.Count; i++)
            {
                lineRenderer.SetPosition(i, vertices[selectedFace.indexes[i]]);
            }
            lineRenderer.SetPosition(selectedFace.indexes.Count, vertices[selectedFace.indexes[0]]);
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.startColor = new Color32(163, 184, 70, 255);
            lineRenderer.endColor = new Color32(163, 184, 70 , 255);

            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

            Debug.Log("Selected Face: " + selectedFace);
            isSelected = true;
        }



    }
    public void ClearSelection()
    {
        if (vertexGameObject != null)
        {
            GameObject.Destroy(vertexGameObject);
        }
        if (edgeGameObject != null)
        {
            GameObject.Destroy(edgeGameObject);
        }
        if (faceGameObject != null)
        {
            GameObject.Destroy(faceGameObject);
        }
    }

    private void HoldSelection()
    {
        if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Vertex)
        {
            vertexGameObject.transform.position = selectedVertex;
        }
        else if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Edge)
        {
            LineRenderer lineRenderer = edgeGameObject.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, vertices[(int)selectedEdge.x]);
            lineRenderer.SetPosition(1, vertices[(int)selectedEdge.y]);
        }
        else if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Face)
        {
            LineRenderer lineRenderer = faceGameObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = selectedFace.indexes.Count + 1;
            for (int i = 0; i < selectedFace.indexes.Count; i++)
            {
                lineRenderer.SetPosition(i, vertices[selectedFace.indexes[i]]);
            }
            lineRenderer.SetPosition(selectedFace.indexes.Count, vertices[selectedFace.indexes[0]]);
        }
    }

    public Vector3 getCloestVertex(Vector3 point)
    {
        return vertices.OrderBy(v => Vector3.Distance(v, point)).First();
    }

    public Vector2 getCloestEdge(Vector3 point)
    {
        return edges.OrderBy(e => Vector3.Distance(vertices[(int)e.x], point) + Vector3.Distance(vertices[(int)e.y], point)).First();
    }

    public Face getCloestFace(Vector3 point)
    {
        return faces.OrderBy(f => Vector3.Distance(getFaceCenter(f), point)).First();
    }

    public Vector3 getFaceCenter(Face face)
    {
        Vector3 center = Vector3.zero;
        foreach (var index in face.indexes)
        {
            center += vertices[index];
        }
        return center / face.indexes.Count;
    }

    public static List<Vector2Int> GenerateEdges(List<Vector3> vertices, List<Face> faces)
    {
        HashSet<Vector2Int> edges = new HashSet<Vector2Int>();

        // Iterate over each face
        foreach (var face in faces)
        {
            int faceVertexCount = face.indexes.Count;

            // Iterate over each vertex in the face
            for (int i = 0; i < faceVertexCount; i++)
            {
                int firstIndex = face[i];
                int secondIndex = face[(i + 1) % faceVertexCount];

                // Create an edge (ensuring the lower index comes first)
                Vector2Int edge = new Vector2Int(Mathf.Min(firstIndex, secondIndex), Mathf.Max(firstIndex, secondIndex));

                // Add the edge to the set (this automatically handles duplicates)
                edges.Add(edge);
            }
        }

        // Convert the set to a list and return
        return new List<Vector2Int>(edges);
    }
}