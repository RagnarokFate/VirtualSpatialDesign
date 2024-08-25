using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserMeasure
{
    private List<Vector3> vertices;

    public static GameObject LineObject;

    // Properties for lines
    public Color color;
    public float width;
    public Material material;
    // Simplification tolerance
    public float tolerance;

    private bool isMeasuring = false;

    // Lists to hold data
    private List<Vector3> lineVertices = new List<Vector3>();
    private List<GameObject> lineObjects = new List<GameObject>();
    private List<float> lineDistances = new List<float>();

    // Root GameObject
    private GameObject lineObjectList;


    public UserMeasure()
    {
        this.vertices = new List<Vector3>();
        this.color = Color.red;
        this.width = 0.1f;
        this.material = new Material(Shader.Find("Unlit/Color"));
        this.tolerance = 0.1f;

        lineObjectList = GameObject.Find("LineObjectList");
    }

    //create destructor
    ~UserMeasure()
    {
        vertices.Clear();
        GameObject.Destroy(LineObject);
    }

    public void HandleMeasure()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            /* Vector3 offset = Camera.main.transform.position - hit.point;
             if (offset != Vector3.zero)
             {
                 offset.Normalize();
                 offset *= -1;
             }
             Vector3 worldPos = hit.point + 2.0f * offset;*/
            Vector3 offset = Vector3.up * 0.01f;
            //Vector3 worldPos = (hit.collider.gameObject.name == "Floor") ? hit.point + offset : hit.point + (-0.01f) * new Vector3(1.0f,0.0f,1.0f).normalized;
            Vector3 worldPos = (hit.collider.gameObject.name == "Floor") ? hit.point + offset : hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                isMeasuring = true;

                vertices.Add(worldPos);

                LineObject = new GameObject("LineObject " + lineObjects.Count);
                LineRenderer lineRenderer = LineObject.AddComponent<LineRenderer>();

                if (lineRenderer == null)
                {
                    Debug.LogError("Failed to add LineRenderer to LineObject.");
                    return;
                }

                LineObject.transform.parent = lineObjectList.transform;
                lineRenderer.material = material;
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
                lineRenderer.startWidth = width;
                lineRenderer.endWidth = width;

                lineObjects.Add(LineObject);
            }
            else if (isMeasuring)
            {
                vertices.Add(worldPos);
                LineRenderer lineRenderer = LineObject.GetComponent<LineRenderer>();
                if (lineRenderer == null)
                {
                    Debug.LogError("LineRenderer component is missing from LineObject.");
                    return;
                }

                lineRenderer.positionCount = vertices.Count;
                lineRenderer.SetPositions(vertices.ToArray());
            }

            if (Input.GetMouseButtonUp(0))
            {
                LineRenderer lineRenderer = LineObject.GetComponent<LineRenderer>();
                if (lineRenderer == null)
                {
                    Debug.LogError("LineRenderer component is missing from LineObject.");
                    return;
                }

                lineRenderer.Simplify(tolerance);
                isMeasuring = false;

                float distance = CalculateDistance();
                lineDistances.Add(distance);

                Debug.Log("Distance: " + distance);
                GameObject distanceText = new GameObject("DistanceText " + lineObjects.Count);
                distanceText.transform.parent = LineObject.transform;
                distanceText.transform.position = (vertices[0] + vertices[vertices.Count - 1])/2.0f;
                distanceText.transform.position += Vector3.up * 0.2f;
                TextMesh textMesh = distanceText.AddComponent<TextMesh>();
                textMesh.text = distance.ToString("F2") + "m";
                textMesh.fontSize = 9;
                textMesh.color = Color.green;
                textMesh.alignment = TextAlignment.Center;
                textMesh.anchor = TextAnchor.MiddleCenter;

                textMesh.transform.LookAt(Camera.main.transform);
                textMesh.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                // Optionally, flip the text to avoid it being mirrored
                textMesh.transform.Rotate(0, 180, 0);
                vertices.Clear();
                
            }

            if (Input.GetMouseButtonDown(1))
            {
                //clear all lines
                foreach (GameObject line in lineObjects)
                {
                    GameObject.Destroy(line);
                }
                lineObjects.Clear();
                lineDistances.Clear();
            }
        }
    }


    public void UpdateDrawLine(Vector2 mouseInput)
    {
        LineRenderer lineRenderer = LineObject.GetComponent<LineRenderer>();
        Ray ray = Camera.main.ScreenPointToRay(mouseInput);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            vertices.Add(hit.point);
            lineRenderer.positionCount = vertices.Count;
            lineRenderer.SetPositions(vertices.ToArray());
        }
    }

    public void ClearLine()
    {
        vertices.Clear();
        LineObject.GetComponent<LineRenderer>().positionCount = 0;
    }

    public void setLoop()
    {
        LineObject.GetComponent<LineRenderer>().loop = true;
    }

    public float CalculateDistance()
    {
        float distance = 0;
        for (int i = 0; i < vertices.Count - 1; i++)
        {
            distance += Vector3.Distance(vertices[i], vertices[i + 1]);
        }
        return distance;
    }
}
