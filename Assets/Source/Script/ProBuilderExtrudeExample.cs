using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class ProBuilderExtrudeExample : MonoBehaviour
{
    private ProBuilderMesh proBuilderMesh;

    void Start()
    {
        // Create a new ProBuilder cube
        proBuilderMesh = ShapeGenerator.CreateShape(ShapeType.Cube);

        // Position the cube at the origin
        proBuilderMesh.transform.position = Vector3.zero;

        // Add the ProBuilderMesh component to the GameObject
        proBuilderMesh.gameObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));

        // Refresh the mesh to apply the changes
        proBuilderMesh.ToMesh();
        proBuilderMesh.Refresh();

        // Extrude one of the cube's faces
        ExtrudeFace();
    }

    void ExtrudeFace()
    {
        // Select a face to extrude (e.g., the first face in the list)
        Debug.Log("Number of faces: " + proBuilderMesh.faces.Count);

        List<Face> facesToExtrude = new List<Face> { proBuilderMesh.faces[0], proBuilderMesh.faces[1] };

        // Define the extrusion parameters
        float distance = 2.0f; // Distance to extrude

        // Perform the extrusion using ExtrudeFaces
        proBuilderMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, distance);

        // Refresh the mesh to apply the changes
        proBuilderMesh.ToMesh();
        proBuilderMesh.Refresh();
    }
}
