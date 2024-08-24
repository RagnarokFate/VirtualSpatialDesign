using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;


public struct LineProperties
{
    public float width;
    public Color color;
    public Material material;
    public float tolerance;


    public void setDefaultProperties()
    {
        this.width = 0.1f;
        this.color = Color.red;
        this.material = new Material(Shader.Find("Unlit/Color"));
        this.tolerance = 0.1f;
    }
}

public class CustomShape
{
    private List<Vector3> vertices;
    private bool isDrawing = false;

    private LineProperties lineProperties;

    public GameObject CustomShapeObject;

    // ROOT OBJECT
    private GameObject CustomShapeParent;



    public CustomShape()
    {
        this.vertices = new List<Vector3>();
        lineProperties.setDefaultProperties();

        try
        {
            CustomShapeParent = GameObject.Find("CustomShape");
        }
        catch (System.Exception)
        {
            Debug.LogError("CustomShape parent GameObject not found in the scene. Please create one and name it 'CustomShape'.");
            throw;
        }

    }


    public void HandleCustomDrawing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 offset = Vector3.up * 0.01f;
            Vector3 worldPos = (hit.collider.gameObject.name == "Floor") ? hit.point + offset : hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                isDrawing = true;

                vertices.Add(worldPos);

                CustomShapeObject = new GameObject("CustomShapeObject ");
                LineRenderer lineRenderer = CustomShapeObject.AddComponent<LineRenderer>();

                if (lineRenderer == null)
                {
                    Debug.LogError("Failed to add LineRenderer to LineObject.");
                    return;
                }

                CustomShapeObject.transform.parent = CustomShapeParent.transform;
                lineRenderer.material = lineProperties.material;
                lineRenderer.startWidth = lineProperties.width;
                lineRenderer.startColor = lineProperties.color;
                lineRenderer.endColor = lineProperties.color;
                lineRenderer.endWidth = lineProperties.width;
                

            }
            else if (isDrawing)
            {
                vertices.Add(worldPos);
                LineRenderer lineRenderer = CustomShapeObject.GetComponent<LineRenderer>();
                if (lineRenderer == null)
                {
                    Debug.LogError("LineRenderer component is missing from CustomShapeObject.");
                    return;
                }

                lineRenderer.positionCount = vertices.Count;
                lineRenderer.SetPositions(vertices.ToArray());
            }

            if (Input.GetMouseButtonUp(0))
            {
                LineRenderer lineRenderer = CustomShapeObject.GetComponent<LineRenderer>();
                if (lineRenderer == null)
                {
                    Debug.LogError("LineRenderer component is missing from CustomShapeObject.");
                    return;
                }

                lineRenderer.Simplify(lineProperties.tolerance);
                isDrawing = false;
                List<int> indices = new List<int>();
                List<Vector3> simplifiedVertices = new List<Vector3>();
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    indices.Add(i);
                    simplifiedVertices.Add(lineRenderer.GetPosition(i));
                }
                ProBuilderMesh proBuilderMesh =  ProBuilderMesh.Create();
                Face face = new Face(indices.ToArray());
                List<Face> faces = new List<Face> { face };

                GameObject.Destroy(CustomShapeObject);

                proBuilderMesh.gameObject.name = "CustomShapeMesh";
                proBuilderMesh.transform.parent = CustomShapeParent.transform;
                proBuilderMesh.RebuildWithPositionsAndFaces(simplifiedVertices, faces);
                proBuilderMesh.ToMesh();
                proBuilderMesh.Refresh();

                vertices.Clear();
            }

            
        }
    }


    public void DrawCustomShape()
    {

    }
}