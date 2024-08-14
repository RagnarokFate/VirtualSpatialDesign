using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using static UnityEngine.Rendering.DebugUI;

public class ElementSelectPanel : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject SelectingModePanel;

    // Start is called before the first frame update
    void Start()
    {
        // SelectingModePanel = GameObject.Find("SelectingModePanel");
        // SelectingModePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    public void HandleSelectElement()
    {
        bool isSelecting = !SelectingModePanel.activeSelf;
        SelectingModePanel.SetActive(isSelecting);
    }


    private Face FindClosestFace(ProBuilderMesh pbMesh, Vector3 worldPos)
    {
        Face closestFace = null;
        float minDistance = float.MaxValue;

        foreach (var face in pbMesh.faces)
        {
            Vector3 faceCenter = GetFaceCenter(pbMesh, face);
            Vector3 faceCenterWorld = pbMesh.transform.TransformPoint(faceCenter);
            float distance = Vector3.Distance(worldPos, faceCenterWorld);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestFace = face;
            }
        }

        return closestFace;
    }

    private int FindClosestVertex(ProBuilderMesh pbMesh, Vector3 worldPos, ref float minDistance)
    {
        int closestVertex = -1;

        for (int i = 0; i < pbMesh.positions.Count; i++)
        {
            Vector3 vertexWorldPos = pbMesh.transform.TransformPoint(pbMesh.positions[i]);
            float distance = Vector3.Distance(worldPos, vertexWorldPos);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestVertex = i;
            }
        }

        return closestVertex;
    }


    private Vector3 GetFaceCenter(ProBuilderMesh pbMesh, Face face)
    {
        Vector3 sum = Vector3.zero;

        foreach (int index in face.indexes)
        {
            sum += pbMesh.positions[index];
        }

        return sum / face.indexes.Count;
    }





}
