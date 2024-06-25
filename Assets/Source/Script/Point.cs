using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Point : MonoBehaviour
{
    Vector3 point;
    ProBuilderMesh pbMesh;
    public GameObject pointPrefab;
    private GameObject PointParent;
    public Point(Vector3 input, GameObject pointPrefab)
    {
        point = input;
        pbMesh = ProBuilderMesh.Create();
        this.pointPrefab = pointPrefab;
        try
        {
            PointParent = GameObject.Find("Points");
        }
        catch (System.Exception)
        {
            Debug.LogError("Points parent GameObject not found in the scene. Please create one and name it 'Points'.");
            throw;
        }
    }

    public GameObject CreatePoint()
    {
        Debug.Log("Create Point");
        GameObject gameObject = new GameObject("Point");
        List<Vector3> pointList = new List<Vector3> { point };
        pbMesh.Clear(); // Clear previous shape
        pbMesh.positions = pointList;
        pbMesh.ToMesh();
        pbMesh.Refresh();

        Instantiate(pointPrefab, point, Quaternion.identity);

        // Assign the ProBuilder mesh to the GameObject's MeshFilter and MeshRenderer
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.sharedMesh = pbMesh.GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.sharedMaterial = pbMesh.GetComponent<MeshRenderer>().sharedMaterial;

        gameObject.gameObject.transform.parent = PointParent.transform;
        return gameObject;
    }
}
