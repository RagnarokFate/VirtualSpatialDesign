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

    public GameObject CreatePolygon()
    {
        Debug.Log("Create Polygon");
        GameObject gameObject = new GameObject("Polygon");

        Debug.Log("Vertices: " + vertices.Count);

        List<int> indices = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        List<Face> faces = new List<Face>();
        for (int i = 0; i < vertices.Count - 2; i++)
        {
            // back face
            int index_1 = (i + 1) % vertices.Count;
            int index_2 = i % vertices.Count;
            int index_3 = 0;
            indices.Add(index_1);
            indices.Add(index_2);
            indices.Add(index_3);

            Face back_face = new Face(new int[] { index_1, index_2, index_3 });
            faces.Add(back_face);

            // front face
            index_1 = 0;
            index_2 = i % vertices.Count;
            index_3 = (i + 1) % vertices.Count;

            indices.Add(index_1);
            indices.Add(index_2);
            indices.Add(index_3);

            Face front_face = new Face(new int[] { index_1, index_2, index_3 });
            faces.Add(front_face);

            // Calculate the normal for the face
            Vector3 v0 = vertices[i];
            Vector3 v1 = vertices[(i + 1) % vertices.Count];
            Vector3 v2 = vertices[(i + 2) % vertices.Count];
            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v1).normalized;

            normals.Add(-1 * normal);
            normals.Add(normal);

        }

        Debug.Log("Faces: " + indices.Count / 3);
        Debug.Log("Normals: " + normals.Count);
        Debug.Log("Vertices: " + vertices.Count);

        // Assign the ProBuilder mesh to the GameObject's MeshFilter and MeshRenderer
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        ProBuilderMesh proBuilderMesh = gameObject.AddComponent<ProBuilderMesh>();

        //proBuilderMesh = ProBuilderMesh.Create(vertices,faces);
        // proBuilderMesh.Clear();
        proBuilderMesh = ProBuilderMesh.Create(vertices, faces);

        proBuilderMesh.positions = vertices;
        //proBuilderMesh.faces = new List<Face> { new Face(indices.ToArray()) };
        proBuilderMesh.faces = faces;

        Debug.Log("faces: " + proBuilderMesh.faces.Count);

        // update the proBuilderMesh component
    

        proBuilderMesh.ToMesh();
        proBuilderMesh.Refresh();

        meshFilter.sharedMesh = proBuilderMesh.GetComponent<MeshFilter>().sharedMesh;
        gameObject.gameObject.transform.parent = PolygonParent.transform;
        return proBuilderMesh.gameObject;

    }


    public ProBuilderMesh CreatePolygonProBuilder()
    {
        List<int> indices = new List<int>();
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
