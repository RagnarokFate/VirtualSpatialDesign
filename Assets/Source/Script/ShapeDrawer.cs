using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ShapeDrawer : MonoBehaviour
{
    private List<Vector3> points = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            PlacePoint();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Space key pressed
        {
            ConnectPoints();
        }
    }

    void PlacePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;
            points.Add(point);
        }
    }

    void ConnectPoints()
    {
        if (points.Count < 3)
        {
            Debug.Log("At least 3 points are required to form a shape.");
            return;
        }

        // Create a new ProBuilder mesh object
        ProBuilderMesh pbMesh = gameObject.AddComponent<ProBuilderMesh>();

        // Add the points as vertices to the mesh
        pbMesh.positions = points.ToArray();

        // Create a convex hull from the points
        pbMesh.ToMesh();
        pbMesh.Refresh();

        // Reset points after connecting
        points.Clear();
    }
}
