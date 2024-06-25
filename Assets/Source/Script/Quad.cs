using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class Quad
{
    public Material QuadMaterial;
    List<Vector3> vertices = new List<Vector3>();
    ProBuilderMesh pbMesh;
    GameObject QuadParent;

    public Quad(List<Vector3> input)
    {
        vertices = input;
        pbMesh = ProBuilderMesh.Create();
        QuadMaterial = new Material(Shader.Find("Standard"));
        try
        {
            QuadParent = GameObject.Find("Quads");
        }
        catch (System.Exception)
        {
            Debug.LogError("Quads parent GameObject not found in the scene. Please create one and name it 'Quads'.");
            throw;
        }
    }

    public GameObject CreateQuad()
    {
        Debug.Log("Create Quad");
        GameObject gameObject = new GameObject("Quad");
        Vector3 point1 = vertices[0];
        Vector3 point2 = vertices[1];

        // Define the other two vertices to form a quad
        Vector3 direction = point2 - point1;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * direction.magnitude;

        Vector3 point3 = point1 + perpendicular;
        Vector3 point4 = point2 + perpendicular;

        // Create a list of quad points
        List<Vector3> quadPoints = new List<Vector3> { point1, point2, point4, point3 };

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

        gameObject.gameObject.transform.parent = QuadParent.transform;
        return gameObject;
    }
}
