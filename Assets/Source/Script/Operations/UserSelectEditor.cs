using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class UserSelectEditor
{
    private List<Vector3> vertices;
    private List<Vector2> edges;
    private List<Face> faces;

    private Vector3 selectedVertex;
    private Vector2 selectedEdge;
    private Face selectedFace;

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