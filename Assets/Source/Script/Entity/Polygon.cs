using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UIElements;

public class Polygon
{
    List<Vector3> vertices;

    GameObject PolygonParent;
    public Material QuadMaterial;



    // private LineRenderer lineRenderer;
    public Polygon(List<Vector3> input)
    {
        vertices = input;
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

    public (List<Vector3>,List<Face>) GetVerticesAndIndices()
    {
        List<int> indices = new List<int>();
        List<Face> faces = new List<Face>();
        int index_1;
        int index_2;
        int index_3;

        for (int i = 0; i < vertices.Count - 1; i++)
        {
            // back face
            index_1 = (i + 1) % vertices.Count;
            index_2 = i % vertices.Count;
            index_3 = 0;

            /*index_1 = (i + 2);
            index_2 = (i + 1);
            index_3 = 0;*/

            indices.Add(index_1);
            indices.Add(index_2);
            indices.Add(index_3);


            Face back_face = new Face(new int[] { index_1, index_2, index_3 });
            faces.Add(back_face);


            // front face
            index_1 = 0;
            index_2 = i % vertices.Count;
            index_3 = (i + 1) % vertices.Count;


            /* index_1 = (i + 2);
             index_2 = i + 1;
             index_3 = 0;*/

            indices.Add(index_1);
            indices.Add(index_2);
            indices.Add(index_3);

            Face front_face = new Face(new int[] { index_1, index_2, index_3 });
            faces.Add(front_face);

        }
        // faces = new List<Face> { new Face(indices.ToArray()) };
        Debug.Log("Faces: " + faces.Count);
        Debug.Log("Vertices: " + vertices.Count);


        return (vertices,faces);
    }


    public ProBuilderMesh CreatePolygonProBuilder()
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

        proBuilderMesh.gameObject.transform.parent = PolygonParent.transform;

        //setting default values for the mesh
        proBuilderMesh.selectable = true;
        proBuilderMesh.userCollisions = true;
        proBuilderMesh.name = "Polygon";


        return proBuilderMesh;
    }


    
}
