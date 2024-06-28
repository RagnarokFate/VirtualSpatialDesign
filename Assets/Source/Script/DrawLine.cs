using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Color lineColor = Color.black;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> indices = new List<int>();
    private Mesh lineMesh;
    private GameObject lineObject;

    void Start()
    {
        lineObject = new GameObject("LineObject");
        lineObject.AddComponent<MeshFilter>();
        lineObject.AddComponent<MeshRenderer>();
        lineObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        lineObject.GetComponent<MeshRenderer>().material.color = lineColor;

        lineMesh = new Mesh();
        lineObject.GetComponent<MeshFilter>().mesh = lineMesh;
    }

    public void DestroyLine()
    {
        vertices.Clear();
        indices.Clear();
        if (lineObject != null)
        {
            Destroy(lineObject);
        }
        lineObject = new GameObject("LineObject");
        lineObject.AddComponent<MeshFilter>();
        lineObject.AddComponent<MeshRenderer>();
        lineObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        lineObject.GetComponent<MeshRenderer>().material.color = lineColor;

        lineMesh = new Mesh();
        lineObject.GetComponent<MeshFilter>().mesh = lineMesh;
    }

    public void UpdateLine(Vector3 input)
    {
        vertices.Add(input);
        int vertexCount = vertices.Count;

        if (vertexCount > 1)
        {
            indices.Add(vertexCount - 2);
            indices.Add(vertexCount - 1);
        }

        UpdateLineMesh();
    }

    private void UpdateLineMesh()
    {
        lineMesh.Clear();
        lineMesh.vertices = vertices.ToArray();
        lineMesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
    }
}
