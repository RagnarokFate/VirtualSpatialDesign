using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SelectingObject { Vertex, Edge, Face, none };


public class SelectOperation
{
    SelectingObject selectingObject = SelectingObject.none;
    float threshold = 0.1f;
    Vector3 mousePosition;

    Vector3 selectedVertex;
    Vector2 selectedEdge;
    Vector3 selectedFace;

    List<Vector3> vertices;
    List<Vector2> edges;
    List<Vector3> faces;


    public SelectOperation(float threshold, Vector3 MousePosition)
    {
        this.threshold = threshold;
        this.mousePosition = MousePosition;

        this.selectedVertex = Vector3.zero;
        this.selectedEdge = Vector2.zero;
        this.selectedFace = Vector3.zero;
    }

    public void setMeshData(List<Vector3> vertices, List<Vector2> edges, List<Vector3> faces)
    {
        this.vertices = vertices;
        this.edges = edges;
        this.faces = faces;
    }

    public void setOperationType(SelectingObject selectingObject)
    {
        this.selectingObject = selectingObject;
    }

    public Vector3 GetVertex()
    {
        if (selectingObject == SelectingObject.Vertex)
        {
            foreach (Vector3 vertex in vertices)
            {
                if (Vector3.Distance(vertex, mousePosition) < threshold)
                {
                    selectedVertex = vertex;
                    return selectedVertex;
                }
            }
        }
        return Vector3.zero;
    }

    public Vector2 GetEdge()
    {
        if (selectingObject == SelectingObject.Edge)
        {
            foreach (Vector2 edge in edges)
            {
                if (Vector2.Distance(edge, mousePosition) < threshold)
                {
                    selectedEdge = edge;
                    return selectedEdge;
                }
            }
        }
        return Vector2.zero;
    }

    public Vector3 GetFace()
    {
        if (selectingObject == SelectingObject.Face)
        {
            foreach (Vector3 face in faces)
            {
                if (Vector3.Distance(face, mousePosition) < threshold)
                {
                    selectedFace = face;
                    return selectedFace;
                }
            }
        }
        return Vector3.zero;
    }

    public void ClearSelection()
    {
        selectedVertex = Vector3.zero;
        selectedEdge = Vector2.zero;
        selectedFace = Vector3.zero;
    }

    public void setSelection()
    {
        if (selectingObject == SelectingObject.Vertex)
        {
            selectedVertex = GetVertex();
        }

        if (selectingObject == SelectingObject.Edge)
        {
            selectedEdge = GetEdge();
        }

        if (selectingObject == SelectingObject.Face)
        {
            selectedFace = GetFace();
        }

    }

    public void PrintSelection()
    {
        if (selectingObject == SelectingObject.Vertex)
        {
            Debug.Log("Selected Vertex: " + selectedVertex);
        }

        if (selectingObject == SelectingObject.Edge)
        {
            Debug.Log("Selected Edge: " + selectedEdge);
        }

        if (selectingObject == SelectingObject.Face)
        {
            Debug.Log("Selected Face: " + selectedFace);
        }
    }

}
