using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketPanelView : MonoBehaviour
{
    public GameObject PocketPanel;
    public Button PocketButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandlePocketPanel()
    {
        bool isActive = !PocketPanel.activeSelf;
        PocketPanel.SetActive(isActive);
        if(isActive)
        {
            PocketButton.gameObject.SetActive(false);
        }
        else
        {
            PocketButton.gameObject.SetActive(true);
        }

    }

    public void ClosePocketPanel()
    {
        PocketPanel.SetActive(false);
        PocketButton.gameObject.SetActive(true);
    }

    public void setShapeType(Shape3DType shapeType)
    {
        ClosePocketPanel();
        MainMenu.shapeType = shapeType;
    }

    public void setDrawObject(DrawObject drawObject)
    {
        ClosePocketPanel();
        MainMenu.drawObject = drawObject;
    }

    // 2d shape buttons
    public void onPointButton()
    {
        setDrawObject(DrawObject.Point);
        PocketButton.gameObject.SetActive(true);

    }
    public void onQuadButton()
    {
        setDrawObject(DrawObject.Quad);
        PocketButton.gameObject.SetActive(true);
    }
    public void onRectangleButton()
    {
        setDrawObject(DrawObject.Rectangle);
        PocketButton.gameObject.SetActive(true);
    }
    public void onPolygonButton()
    {
        setDrawObject(DrawObject.Polygon);
        PocketButton.gameObject.SetActive(true);
    }


    // 3d shape buttons
    public void onArchButton()
    {
        setShapeType(Shape3DType.Arch);
        PocketButton.gameObject.SetActive(true);
    }
    public void onConeButton()
    {
        setShapeType(Shape3DType.Cone);
        PocketButton.gameObject.SetActive(true);
    }
    public void onCubeButton()
    {
        setShapeType(Shape3DType.Cube);
        PocketButton.gameObject.SetActive(true);
    }
    public void onCylinderButton()
    {
        setShapeType(Shape3DType.Cylinder);
        PocketButton.gameObject.SetActive(true);
    }
    public void onPipeButton()
    {
        setShapeType(Shape3DType.Pipe);
        PocketButton.gameObject.SetActive(true);
    }
    public void onPlaneButton()
    {
        setShapeType(Shape3DType.Plane);
        PocketButton.gameObject.SetActive(true);
    }
    public void onSphereButton()
    {
        setShapeType(Shape3DType.Sphere);
        PocketButton.gameObject.SetActive(true);
    }
    public void onSpriteButton()
    {
        setShapeType(Shape3DType.Sprite);
        PocketButton.gameObject.SetActive(true);
    }
    public void onStairButton()
    {
        setShapeType(Shape3DType.Stair);
        PocketButton.gameObject.SetActive(true);
    }
    public void onTorusButton()
    {
        setShapeType(Shape3DType.Torus);
        PocketButton.gameObject.SetActive(true);
    }


}
