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

    private (List<Vector3>, List<Face>) GetVerticesAndIndices()
    {
        Vector3 point1 = vertices[0];
        Vector3 point2 = new Vector3(point1.x, point1.y, vertices[1].z);
        Vector3 point3 = vertices[1];
        Vector3 point4 = new Vector3(vertices[1].x, vertices[1].y, point1.z);

        // Create a list of rectangle points
        List<Vector3> rectanglePoints = new List<Vector3> { point1, point2, point3, point4 };
        List<int> indices = new List<int>
        {
            2, 1, 0, // First triangle
            0, 3, 2,  // Second triangle
            0, 1, 2,  // Third triangle
            2, 3, 0   // Fourth triangle
        };

        List <Face> faces = new List<Face> { new Face(indices.ToArray()) };
        return (rectanglePoints, faces);
    }

    public ProBuilderMesh CreateRectangleProBuilder()
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

        proBuilderMesh.gameObject.transform.parent = RectangleParent.transform;

        //setting default values for the mesh
        proBuilderMesh.selectable = true;
        proBuilderMesh.userCollisions = true;
        proBuilderMesh.name = "Rectangle";


        return proBuilderMesh;

    }
}
