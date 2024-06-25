using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Rectangle
{
    public Material QuadMaterial;
    List<Vector3> vertices = new List<Vector3>();
    ProBuilderMesh pbMesh;
    private GameObject RectangleParent;

    public Rectangle(List<Vector3> input)
    {
        vertices = input;
        pbMesh = ProBuilderMesh.Create();
        QuadMaterial = new Material(Shader.Find("Standard"));
        try
        {
            RectangleParent = GameObject.Find("Rectangles");
        }
        catch (System.Exception)
        {
            Debug.LogError("Rectangles parent GameObject not found in the scene. Please create one and name it 'Rectangles'.");
            throw;
        }
    }

    public GameObject CreateRectangle()
    {
        Debug.Log("Create Rectangle");
        GameObject gameObject = new GameObject("Rectangle");
        Vector3 point1 = vertices[0];
        Vector3 point2 = new Vector3(point1.x, point1.y, vertices[1].z);
        Vector3 point3 = vertices[1];
        Vector3 point4 = new Vector3(vertices[1].x, vertices[1].y, point1.z);

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
        vertices.Clear();

        // Assign the ProBuilder mesh to the GameObject's MeshFilter and MeshRenderer
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.sharedMesh = pbMesh.GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.sharedMaterial = QuadMaterial;
        gameObject.gameObject.transform.parent = RectangleParent.transform;

        return gameObject;


    }
}
