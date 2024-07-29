using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public enum Shape3DType { Arch, Cone, Cube, Cylinder, Pipe, Plane, Sphere , Sprite, Stair, Torus, None }

public class UserInsertion
{
	public Shape3DType shapeType;
	public Vector3 PivotPoint;

    ProBuilderMesh pbMesh;
    GameObject MeshParent;

    public UserInsertion()
    {
        try
        {
            MeshParent = GameObject.Find("Meshs");
        }
        catch (System.Exception)
        {
            Debug.LogError("Quads parent GameObject not found in the scene. Please create one and name it 'Quads'.");
            throw;
        }
    }


    public void SetShapeType(Shape3DType shapeType)
    {
        this.shapeType = shapeType;
    }



    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleInsertion()
	{
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 meshPosition = new Vector3(0, 0, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                meshPosition = hit.point;
            }

            Debug.Log("Left Mouse Clicked");
            if(shapeType == Shape3DType.Cube)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Cube, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Sphere)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Sphere, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Cylinder)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Cylinder, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Pipe)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Pipe, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Plane)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Plane, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Torus)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Torus, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Stair)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Stair, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Arch)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Arch, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Cone)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Cone, PivotLocation.FirstCorner);
            }
            else if (shapeType == Shape3DType.Sprite)
            {
                pbMesh = ShapeGenerator.CreateShape(ShapeType.Sprite, PivotLocation.FirstCorner);
            }

            pbMesh.transform.parent = MeshParent.transform;
            pbMesh.transform.position = meshPosition;

            // gameObject = polygon.CreatePolygon();
            GameObject gameObject = pbMesh.gameObject;
            gameObject.name += " " + GameManager.Instance.gameObjectList.Count.ToString();
            GameManager.Instance.AddGameObject(gameObject);
            GameManager.Instance.SetActiveProBuilderObject(pbMesh);
            GameManager.Instance.SetActiveGameObject(gameObject);
            GameManager.Instance.threeD_Counter++;
        }
        /*if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse Clicked");
        }*/
    }

}
