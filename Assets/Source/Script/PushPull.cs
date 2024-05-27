/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PushPullMode { None, Vertex, Edge, Face }

public class PushPull : MonoBehaviour
{
    private PushPullMode pushPullMode = PushPullMode.None;
    private List<Vector3> vertices;
    private List<Face> faces;
    private Material material; // Added for drawing

    private const float MIN_PUSH_PULL_DISTANCE = 0.1f; // Minimum allowed distance
    private const float MAX_PUSH_PULL_DISTANCE = 5f; // Maximum allowed distance

    // Start is called before the first frame update
    void Start()
    {
        // Create a cube object (replace with your cube creation logic)
        Cube cube = new Cube();
        vertices = cube.GetVertices();
        faces = cube.GetFaces();

        // Set the material for drawing (assuming a shared material)
        material = new Material(Shader.Find("Diffuse")); // Replace with your desired shader

        // Draw the cube (replace with your drawing logic)
        DrawCube(cube);
    }

    // Update is called once per frame
    void Update()
    {
        // Update PushPullMode based on user input or GUI interaction
        PushPullMode current = GetPushPullModeFromGUI();

        if (pushPullMode != PushPullMode.None)
        {
            // Get user-specified distance, ensuring it's within limits
            float pushPullDistance = Mathf.Clamp(GetPushPullDistanceFromGUI(), MIN_PUSH_PULL_DISTANCE, MAX_PUSH_PULL_DISTANCE);

            // Perform push/pull operation based on the selected mode
            switch (pushPullMode)
            {
                case PushPullMode.Face:
                    PerformFacePushPull(GetSelectedFaceIndexFromGUI(), pushPullDistance);
                    break;
                case PushPullMode.Edge:
                    PerformEdgePushPull(GetSelectedEdgeIndexFromGUI(), pushPullDistance);
                    break;
                case PushPullMode.Vertex:
                    PerformVertexPushPull(GetSelectedVertexIndexFromGUI(), pushPullDistance);
                    break;
            }
        }
    }

    void DrawCube(Cube cube)
    {
        // Create a Mesh object for rendering
        Mesh mesh = new Mesh();
        mesh.vertices = cube.GetVertices().ToArray(); // Convert to array for Mesh API

        // Add triangles (faces) to the Mesh
        List<int> triangles = new List<int>();
        foreach (Face face in cube.GetFaces())
        {
            triangles.AddRange(face.GetIndices());
        }
        mesh.triangles = triangles.ToArray();

        // Create a MeshFilter and MeshRenderer to render the Mesh
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
    }

    // Push/Pull operations (replace with your specific implementations)

    void PerformFacePushPull(int selectedFaceIndex, float distance)
    {
        Face selectedFace = faces[selectedFaceIndex];
        Vector3 normal = selectedFace.GetNormal();

        // Translate the face along its normal direction
        for (int i = 0; i < selectedFace.GetVertices().Count; i++)
        {
            int vertexIndex = selectedFace.GetVertices()[i];
            vertices[vertexIndex] += normal * distance;
        }

        // Consider scaling or extruding the face for more advanced modifications
        // ... (your implementation)
    }

    void PerformEdgePushPull(int selectedEdgeIndex, float distance)
    {
        // Get the connected vertices of the selected edge
        Face face1 = faces[GetFaceIndexFromEdgeIndex(selectedEdgeIndex, 0)];
        Face face2 = faces[GetFaceIndexFromEdgeIndex(selectedEdgeIndex, 1)];
        int vertexIndex1 = face1.GetEdgeVertexIndex(selectedEdgeIndex);
        int vertexIndex2 = face2.GetEdgeVertexIndex(selectedEdgeIndex);

        // Calculate the edge direction
        Vector3 edgeDirection = vertices[vertexIndex2] - vertices[vertexIndex1];

        // Move the connected vertices along the direction
        vertices[vertexIndex1] += edgeDirection * distance;
        vertices[vertexIndex2] += edgeDirection * distance;

        // Consider scaling or chamfering connected faces for smooth transitions
        // ...

    }
}
*/