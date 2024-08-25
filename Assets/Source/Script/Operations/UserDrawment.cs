using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;

public enum DrawObject { Point, Quad, Rectangle, Polygon, none }

public class UserDrawment
{
    public List<Vector3> vertices = new List<Vector3>();
    public DrawObject drawObject;
    ProBuilderMesh pbMesh;

    private DrawLine drawLine;

    public UserDrawment()
    {
        GameObject gameObject = new GameObject("DrawObject");
        drawLine = gameObject.AddComponent<DrawLine>();
    }

    public void SetDrawObject(DrawObject drawObject)
    {
        this.drawObject = drawObject;
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleDrawment()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 offset = new Vector3(0, 0.00001f, 0);
                Vector3 point_pos = hit.point + offset;
                // print the point position and it's index in vertices array
                Debug.Log("Point Index: " + vertices.Count + "Point Position: " + point_pos);
                vertices.Add(point_pos);
                drawLine.UpdateLine(point_pos);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject gameObject;
            pbMesh = ProBuilderMesh.Create();

            if (drawObject == DrawObject.Point)
            {
                // Create a point
            }
            else if (drawObject == DrawObject.Quad)
            {
                // Create a quad
                Quad quad = new Quad(vertices);
                pbMesh = quad.CreateQuadProBuilder();
            }
            else if (drawObject == DrawObject.Rectangle)
            {
                // Create a rectangle
                Rectangle rectangle = new Rectangle(vertices);
                pbMesh = rectangle.CreateRectangleProBuilder();
            }
            else
            if (drawObject == DrawObject.Polygon)
            {
                // Create a polygon from the vertices (clockwise by default
                Debug.Log("Polygon : ");
                Polygon polygon = new Polygon(vertices);
                pbMesh = polygon.CreatePolygonProBuilder();
            }

            // gameObject = polygon.CreatePolygon();
            gameObject = pbMesh.gameObject;
            gameObject.name += " " + GameManager.Instance.gameObjectList.Count.ToString();
            gameObject.tag = "Selectable";
            gameObject.AddComponent<MeshCollider>().convex = true;

            // gameObject.AddComponent<Rigidbody>();

            GameManager.Instance.AddGameObject(gameObject);
            GameManager.Instance.SetActiveGameObject(gameObject);
            GameManager.Instance.twoD_Counter++;
            vertices.Clear();
            drawLine.DestroyLine();

            drawObject = DrawObject.none;


        }
    }

}
