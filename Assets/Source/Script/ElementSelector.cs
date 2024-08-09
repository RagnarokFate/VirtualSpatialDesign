using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ElementSelector : MonoBehaviour
{
    bool isSelecting = false;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.activeGameObject == null)
        {
            return;
        }

        if (isSelecting)
        {
            Debug.Log("Selecting Element");
            // Select Element
            SelectModeToEdit currentSelectModeToEdit = GameManager.Instance.selectModeToEdit;

            if (currentSelectModeToEdit == SelectModeToEdit.Vertex)
            {
                // Select Vertex
                // TODO: Implement
            }
            else if (currentSelectModeToEdit == SelectModeToEdit.Edge)
            {
                // Select Edge

            }
            else if (currentSelectModeToEdit == SelectModeToEdit.Face)
            {
                // Select Face
                /*GameObject gameObject = GameManager.Instance.activeGameObject;
                ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();
                if (pbMesh == null)
                {
                    Debug.LogError("No ProBuilderMesh Component Found");
                    return;
                }

                if (Input.GetMouseButtonDown(0))
                {

                    Vector2 mousePosition = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Face face = FindClosestFace(pbMesh, hit.point);
                        if (face != null)
                        {
                            // Add the face to the list of selected faces
                            GameManager.Instance.editorfaces.Add(face);
                            Debug.Log("Face Selected: " + face.ToString());
                        }


                    }
                }*/
            }
        }
        else
        {
            // reset
            /*if(GameManager.Instance.editorEdges.Count > 0 || GameManager.Instance.editorVertices.Count > 0 || GameManager.Instance.editorfaces.Count > 0)
            {
                GameManager.Instance.editorVertices.Clear();
                GameManager.Instance.editorEdges.Clear();
                GameManager.Instance.editorfaces.Clear();
            }*/
            

            return;
        }

    }

    public void HandleSelectElement()
    {
        isSelecting = !(isSelecting);
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
