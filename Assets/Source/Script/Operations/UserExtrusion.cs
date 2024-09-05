using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using UnityEditor.ProBuilder;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class UserExtrusion
{
    private bool locked;
    private Vector2 initialMousePos;
    private float extrusionValue;
    private float finalExtrusionValue;
    private bool isExtruding;

    public bool meshExtrusionLock;
    public float extrusionSensitivity = 0.01f;

    private Face selectedFace;

    private List<Vector3> previousVerticesValues;
    private List<Face> previousFacesValues;
    public UserExtrusion()
    {
        meshExtrusionLock = false;
        locked = false;
        isExtruding = false;
    }

    // Reset the object color upon deselecting/unclicking Active GameObject
    public void HandleExtrusion()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;

        if (gameObject != null)
        {
            ProBuilderMesh proBuilderMesh = gameObject.GetComponent<ProBuilderMesh>();
            if (proBuilderMesh == null)
            {
                Debug.LogError("No ProBuilderMesh component found on this GameObject.");
                return;
            }

            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Left-click to select a face
                if (Input.GetMouseButtonDown(0) && !locked)
                {
                    selectedFace = new UserSelectEditor(proBuilderMesh).getCloestFace(hit.point);
                    if (selectedFace != null)
                    {
                        Debug.Log("Selected Face: " + selectedFace);
                        proBuilderMesh.SetFaceColor(selectedFace, Color.yellow);
                        proBuilderMesh.ToMesh();
                        proBuilderMesh.Refresh();
                        locked = true;
                    }
                    finalExtrusionValue = 0f;
                }

                // Right-click to deselect the face
                if (Input.GetMouseButtonDown(1) && locked)
                {
                    if (selectedFace != null)
                    {
                        proBuilderMesh.SetFaceColor(selectedFace, Color.magenta);
                    }
                    locked = false;
                }

                // Start extrusion on left-click and hold
                if (Input.GetMouseButtonDown(0) && locked && selectedFace != null)
                {
                    initialMousePos = mousePos;
                    extrusionValue = 0f;
                    isExtruding = true;

                    StorePreviousValues(proBuilderMesh);
                }

                // Continue extrusion while holding the left mouse button
                if (Input.GetMouseButton(0) && isExtruding && selectedFace != null)
                {

                    Vector2 mouseDelta = mousePos - initialMousePos;
                    float mouseMagnitude = mouseDelta.magnitude;
                    extrusionValue = mouseMagnitude * extrusionSensitivity;

                    // Optional: Preview the extrusion in real-time
                    List<Face> facesToExtrude = new List<Face> { selectedFace };
                    proBuilderMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, extrusionValue);
                    proBuilderMesh.ToMesh();
                    proBuilderMesh.Refresh();
                    initialMousePos = Input.mousePosition;
                    finalExtrusionValue += extrusionValue;

                }

                // Stop extrusion on left mouse button release
                if (Input.GetMouseButtonUp(0) && isExtruding && selectedFace != null)
                {
                    Debug.Log("Final Extrusion Value: " + finalExtrusionValue);

                    string text = "Extrusion on face " + selectedFace.ToString() + " with a value of " + finalExtrusionValue;
                    FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("EditorMenu").GetComponent<Canvas>().transform);

                    isExtruding = false;
                    locked = false;

                    RestorePreviousValues(proBuilderMesh);
                    // Optional: Preview the extrusion in real-time
                    List<Face> facesToExtrude = new List<Face> { selectedFace };
                    proBuilderMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, finalExtrusionValue);
                    proBuilderMesh.ToMesh();
                    proBuilderMesh.Refresh();


                    if (extrusionValue == 0.0f)
                    {
                        finalExtrusionValue = 0f;
                        return;
                    }
                    
                    


                }
            }
        }
    }

    

    public void LockExtrusion()
    {
        meshExtrusionLock = true;
    }

    public void UnlockExtrusion()
    {
        meshExtrusionLock = false;
    }

    private void StorePreviousValues(ProBuilderMesh mesh)
    {
        previousVerticesValues = mesh.positions.ToList();
        previousFacesValues = mesh.faces.ToList();
    }

    private void RestorePreviousValues(ProBuilderMesh mesh)
    {
        mesh.positions = previousVerticesValues;
        mesh.faces = previousFacesValues;

        mesh.RebuildWithPositionsAndFaces(previousVerticesValues, previousFacesValues);

    }



    public static void SimplifyProBuilderMesh(ProBuilderMesh mesh, float tolerance)
    {
        // Step 1: Merge close vertices
        Vector3[] vertices = mesh.positions.ToArray();
        Dictionary<int, int> vertexMap = new Dictionary<int, int>();
        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();
        List<Color> newColors = new List<Color>();
        Dictionary<int, int> indexMap = new Dictionary<int, int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < newVertices.Count; j++)
            {
                if (Vector3.Distance(newVertices[j], vertices[i]) < tolerance)
                {
                    vertexMap[i] = j;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                vertexMap[i] = newVertices.Count;
                newVertices.Add(vertices[i]);
                newUVs.Add(mesh.textures[i]); // Assuming UVs are in the same order as vertices
                newColors.Add(mesh.colors[i]); // Assuming colors are in the same order as vertices
            }
        }

        // Step 2: Update faces
        List<Face> newFaces = new List<Face>();
        foreach (Face face in mesh.faces)
        {
            HashSet<int> uniqueIndexes = new HashSet<int>();
            foreach (int index in face.indexes)
            {
                uniqueIndexes.Add(vertexMap[index]);
            }

            if (uniqueIndexes.Count >= 3)
            {
                newFaces.Add(new Face(uniqueIndexes.ToArray()));
            }
        }

        // Step 3: Update the mesh with new vertices, UVs, colors, and faces
        mesh.RebuildWithPositionsAndFaces(newVertices.ToArray(), newFaces.ToArray());

        // Update UVs and colors
        mesh.textures = newUVs.ToArray();
        mesh.colors = newColors.ToArray();

        // Step 4: Refresh the mesh
        mesh.ToMesh();
        mesh.Refresh();
    }


}
