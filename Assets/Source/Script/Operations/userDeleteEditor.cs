using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UIElements;

public class userDeleteEditor : UserSelectEditor
{
    // Properites
    private GameObject VertexPrefab;

    private GameObject VertexReference;


    protected bool isMouseHolding = false;




    public userDeleteEditor(GameObject VertexPrefab)
    {
        this.VertexPrefab = VertexPrefab;
    }


    //functions
    public void HandleDeleteEditor()
    {
        if (GameManager.Instance.activeGameObject != null)
        {
            GameObject activeGameObject = GameManager.Instance.activeGameObject;

            if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Vertex)
            {
                HandleVertexDeletion(activeGameObject);
            }
            else if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Edge)
            {
                // NOTHING
            }
            else if (GameManager.Instance.selectModeToEdit == SelectModeToEdit.Face)
            {
                HandleFaceDeletion(activeGameObject);
            }



        }
    }

    public void HandleVertexDeletion(GameObject activeGameObject)
    {
        ProBuilderMesh pbMesh = activeGameObject.GetComponent<ProBuilderMesh>();
        if (pbMesh == null) return;

        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == activeGameObject)
        {
            UserSelectEditor userSelectEditor = new UserSelectEditor(pbMesh);
            Face closestFace = userSelectEditor.getCloestFace(hit.point);
            Vector3 cloesetVertex = userSelectEditor.getCloestVertex(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                // Create the vertex at the hit point
                VertexReference = GameObject.Instantiate(VertexPrefab, hit.point, Quaternion.identity);
                VertexReference.name = "Vertex";
                isMouseHolding = true;
            }
            else if (Input.GetMouseButton(0) && isMouseHolding)
            {
                // Move the vertex to the current hit point
                if (VertexReference != null)
                {
                    VertexReference.transform.position = hit.point;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                // Place the vertex at the hit point and finalize the mesh update
                isMouseHolding = false;
                Debug.Log("Mouse Up");
                if (VertexReference != null)
                {
                    VertexReference.transform.position = hit.point;

                    string text = "Vertex placed at " + hit.point.ToString() + " on face " + closestFace.ToString() + "closet vertex is " + cloesetVertex;
                    FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("EditorMenu").GetComponent<Canvas>().transform);

                    // Add the vertex to the mesh
                    RemoveVertex(pbMesh, cloesetVertex);

                }
            }
        }
    }




    public void HandleFaceDeletion(GameObject activeGameObject)
    {
        ProBuilderMesh pbMesh = activeGameObject.GetComponent<ProBuilderMesh>();
        if (pbMesh == null) return;

        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == activeGameObject)
        {
            UserSelectEditor userSelectEditor = new UserSelectEditor(pbMesh);
            Face closestFace = userSelectEditor.getCloestFace(hit.point);

            // Check for mouse down (begin rectangle creation)
            if (Input.GetMouseButtonDown(0) && !isMouseHolding)
            {
                // Create a new GameObject with a LineRenderer to represent the rectangle edges
                GameObject lineObject = new GameObject("RectanglePreview");
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 5; // 4 vertices and close the rectangle (first and last point same)
                lineRenderer.loop = true; // Make it a closed shape
                lineRenderer.useWorldSpace = true;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;

                // Store the first vertex (hit point)
                Vector3 startPoint = hit.point;
                lineRenderer.SetPosition(0, startPoint);
                lineRenderer.SetPosition(4, startPoint); // Closing the loop

                VertexReference = lineObject;
                isMouseHolding = true;
                return;
            }
            else if (Input.GetMouseButton(0) && isMouseHolding)
            {
                // Update the rectangle as the mouse is dragged
                if (VertexReference != null)
                {
                    LineRenderer lineRenderer = VertexReference.GetComponent<LineRenderer>();

                    // Calculate the current rectangle's vertices
                    Vector3 currentMousePos = hit.point;
                    Vector3 startPoint = lineRenderer.GetPosition(0);

                    Vector3 corner1 = new Vector3(startPoint.x, 1.0f, currentMousePos.z);
                    Vector3 corner2 = currentMousePos;
                    Vector3 corner3 = new Vector3(currentMousePos.x, 1.0f, startPoint.z);

                    // Update the LineRenderer with the new rectangle vertices
                    lineRenderer.SetPosition(1, corner1);
                    lineRenderer.SetPosition(2, corner2);
                    lineRenderer.SetPosition(3, corner3);
                }
            }
            else if (Input.GetMouseButtonUp(0) && isMouseHolding)
            {
                // Finalize the rectangle and add it to the mesh
                isMouseHolding = false;
                Debug.Log("Mouse Up - Finalizing Rectangle");

                if (VertexReference != null)
                {
                    LineRenderer lineRenderer = VertexReference.GetComponent<LineRenderer>();

                    // Get the final rectangle vertices
                    Vector3[] rectangleVertices = new Vector3[4];
                    for (int i = 0; i < 4; i++)
                    {
                        rectangleVertices[i] = lineRenderer.GetPosition(i);
                        rectangleVertices[i].y = 1.0f; // Ensure the rectangle is on the same plane as the mesh
                    }

                    // Add vertices to the mesh
                    List<Vector3> vertices = pbMesh.positions.ToList();
                    List<Face> faces = pbMesh.faces.ToList();


                    int[] vertexIndices = new int[] { 0, 1, 2, 2, 3, 0 };


                    for (int i = 0; i < vertexIndices.Length; i++)
                    {
                        vertexIndices[i] += vertices.Count; //offset the indices
                    }

                    Face newFace = new Face(vertexIndices);




                    vertices.AddRange(rectangleVertices);
                    faces.Add(newFace);

                    pbMesh.positions = vertices;
                    pbMesh.faces = faces;

                    vertexIndices = closestFace.distinctIndexes.ToArray();
                    int index1 = vertexIndices[0], index2 = vertexIndices[1], index3 = vertexIndices[2], index4 = vertexIndices[3];

                    //subtract the face creating 9 new faces and remove the main face aftwards
                    GenerateHole generateHole = new GenerateHole(vertices, faces);
                    generateHole.DeleteSubFace(pbMesh, closestFace, newFace);

                    // Destroy the LineRenderer object as it is no longer needed
                    GameObject.Destroy(VertexReference);
                    VertexReference = null;
                }
            }
        }

    }


    public void RemoveVertex(ProBuilderMesh pbMesh, Vector3 vertex)
    {
        List<Vector3> vertices = pbMesh.positions.ToList();
        List<Face> faces = pbMesh.faces.ToList();

        // Find the vertex index
        int vertexIndex = -1;
        for (int i = 0; i < vertices.Count; i++)
        {
            if (vertices[i] == vertex)
            {
                vertexIndex = i;
                break;
            }
        }

        if (vertexIndex == -1) return;

        // Remove the vertex from the mesh
        vertices.RemoveAt(vertexIndex);

        // Remove any faces that contain the vertex
        List<Face> facesToRemove = new List<Face>();
        foreach (Face face in faces)
        {
            Vector3 vertex_1 = vertices[face.distinctIndexes[0]];
            Vector3 vertex_2 = vertices[face.distinctIndexes[1]];
            Vector3 vertex_3 = vertices[face.distinctIndexes[2]];
            try
            {
                Vector3 vertex_4 = vertices[face.distinctIndexes[3]];
            }
            catch (Exception e) { Debug.LogError(e); }


            if (vertex_1 == vertex || vertex_2 == vertex || vertex_3 == vertex)
            {
                facesToRemove.Add(face);
            }
            else
            {
                continue;
            }

        }

        foreach (Face face in facesToRemove)
        {
            faces.Remove(face);
        }


        pbMesh.RebuildWithPositionsAndFaces(vertices, faces);
        pbMesh.ToMesh();
        pbMesh.Refresh();
    }





}


