using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Polygon
{
    public Material QuadMaterial;
    List<Vector3> vertices = new List<Vector3>();
    ProBuilderMesh pbMesh;

    GameObject PolygonParent;
    public float lineWidth = 0.1f;
    public Color lineColor = Color.red;

    // private LineRenderer lineRenderer;
    public Polygon(List<Vector3> input)
    {
        vertices = input;
        pbMesh = ProBuilderMesh.Create();
        QuadMaterial = new Material(Shader.Find("Standard"));
        try
        {
            PolygonParent = GameObject.Find("Polygons");
        }
        catch (System.Exception)
        {
            Debug.LogError("Polygons parent GameObject not found in the scene. Please create one and name it 'Polygons'.");
            throw;
        }
    }


    public GameObject CreatePolygon()
    {
        Debug.Log("Create Polygon");
        GameObject gameObject = new GameObject("Polygon");
        List<Vector3> polygonPoints = new List<Vector3>();
        for (int i = 0; i < vertices.Count; i++)
        {
            polygonPoints.Add(vertices[i]);
        }
        pbMesh.Clear(); // Clear previous shape
        pbMesh.positions = polygonPoints;
        List<int> indices = new List<int>();
        for (int i = 0; i < vertices.Count; i++)
        {
            indices.Add(0);
            indices.Add(i % vertices.Count);
            indices.Add((i + 1) % vertices.Count);

        }
        pbMesh.faces = new List<Face> { new Face(indices.ToArray()) };
        pbMesh.ToMesh();
        pbMesh.Refresh();


        // Assign the ProBuilder mesh to the GameObject's MeshFilter and MeshRenderer
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.sharedMesh = pbMesh.GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.sharedMaterial = QuadMaterial;

        gameObject.gameObject.transform.parent = PolygonParent.transform;
        return gameObject;

    }

    public void setvertices(List<Vector3> input)
    {
        vertices = input;
    }

}
