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


    private (List<Vector3>, List<Face>) GetVerticesAndIndices()
    {
        Vector3 point1 = vertices[0];
        Vector3 point2 = vertices[1];

        // Define the other two vertices to form a quad
        Vector3 direction = point2 - point1;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * direction.magnitude;

        Vector3 point3 = point1 + perpendicular;
        Vector3 point4 = point2 + perpendicular;

        // Create a list of quad points
        List<Vector3> quadPoints = new List<Vector3> { point1, point2, point4, point3 };

        // Create a list of rectangle points
        List<int> indices = new List<int>
        {
            2, 1, 0, // First triangle
            0, 3, 2,  // Second triangle
            0, 1, 2,  // Third triangle
            2, 3, 0   // Fourth triangle

        };

        List<Face> faces = new List<Face> { new Face(indices.ToArray()) };
        return (quadPoints, faces);
    }


    public ProBuilderMesh CreateQuadProBuilder()
    {
        List<Face> faces = new List<Face>();

        (vertices, faces) = GetVerticesAndIndices();

        ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create();
        proBuilderMesh.Clear();

        proBuilderMesh.positions = vertices;
        proBuilderMesh.faces = faces;

        proBuilderMesh.GetComponent<MeshRenderer>().material = QuadMaterial;
        //proBuilderMesh.GetComponent<MeshRenderer>().material.color = Color.red;

        proBuilderMesh.ToMesh();
        proBuilderMesh.Refresh();

        proBuilderMesh.gameObject.transform.parent = QuadParent.transform;

        //setting default values for the mesh
        proBuilderMesh.selectable = true;
        proBuilderMesh.userCollisions = true;
        proBuilderMesh.name = "Quad";


        return proBuilderMesh;
    }
}
