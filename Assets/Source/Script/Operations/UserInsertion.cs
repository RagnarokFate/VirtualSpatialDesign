using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public enum Shape3DType { Arch, Cone, Cube, Cylinder, Pipe, Plane, Sphere , Sprite, Stair, Torus, None }

public class UserInsertion
{
	public Shape3DType shapeType = Shape3DType.Cube;
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
            Debug.LogError("Meshs parent GameObject not found in the scene. Please create one and name it 'Meshs'.");
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
            else
            {
                return;
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
            gameObject.tag = "Selectable";
            gameObject.name += " " + GameManager.Instance.gameObjectList.Count.ToString();

            MeshCollider gameObjectCollider = gameObject.AddComponent<MeshCollider>();
            gameObjectCollider.convex = true;

            /*gameObjectCollider.material = Resources.Load<PhysicMaterial>("Physics Material/Sticky");
            if (gameObjectCollider.material == null) 
            {
                Debug.LogError("Physics Material/Bouncy not found in the Resources folder. Please create one and name it 'Bouncy'.");
            }*/
            //gameObject.AddComponent<Rigidbody>();

            GameManager.Instance.AddGameObject(gameObject);
            GameManager.Instance.SetActiveGameObject(gameObject);
            GameManager.Instance.threeD_Counter++;
        }
        /*if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse Clicked");
        }*/
    }

}
