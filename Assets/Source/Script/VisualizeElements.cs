using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class VisualizeElements : MonoBehaviour
{
    public GameObject vertexPrefab;
    private GameObject vertixRootParent;
    private GameObject gridRootParent;

    // Reference to the Toggle component
    public Toggle vertexToggle;
    public Toggle gridToggle;

    void Start()
    {
        this.vertixRootParent = new GameObject("Vertices");
        this.gridRootParent = new GameObject("Grid");
    }
    public void ToggleVertices()
    {
        if (vertexToggle.isOn is true)
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (gameObject != null)
            {
                ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
                if (mesh != null)
                {
                    int counter = 0;
                    foreach (Vector3 vertex in mesh.positions)
                    {
                        GameObject vertexObject = GameObject.Instantiate(vertexPrefab, vertex, Quaternion.identity);
                        vertexObject.name = "Vertex " + counter;
                        vertexObject.transform.SetParent(vertixRootParent.transform);
                    }
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (vertixRootParent != null && vertixRootParent.transform.childCount > 0)
            {
                GameObject.Destroy(vertixRootParent);
                this.vertixRootParent = new GameObject("Vertices");
            }
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn is true)
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (gameObject != null)
            {
                ProBuilderMesh mesh = gameObject.GetComponent<ProBuilderMesh>();
                if (mesh != null)
                {
                    GameObject gridGameObject = new GameObject("Grid Game Object");
                    gridGameObject.transform.SetParent(gridRootParent.transform);
                    LineRenderer lineRenderer = gridGameObject.AddComponent<LineRenderer>();

                    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.green;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;


                    for (int i = 0; i < mesh.faces.Count; i++)
                    {
                        Face face = mesh.faces[i];
                        
                        for (int j = 0; j < face.indexes.Count; j++)
                        {
                            Vector3 vertex = mesh.positions[face.indexes[j]];
                            lineRenderer.positionCount = face.indexes.Count + 1;
                            lineRenderer.SetPosition(j, vertex);
                        }
                        lineRenderer.SetPosition(face.indexes.Count, mesh.positions[face.indexes[0]]);
                    }

                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GameObject gameObject = GameManager.Instance.activeGameObject;
            if (gridRootParent != null && gridRootParent.transform.childCount > 0)
            {
                GameObject.Destroy(gridRootParent);
                this.gridRootParent = new GameObject("Grid");
            }
            gameObject.SetActive(true);
        }

    }


}