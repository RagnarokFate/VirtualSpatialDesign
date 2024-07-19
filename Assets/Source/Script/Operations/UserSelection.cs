using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UserSelection
{
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;

    public Transform highlight;
    public Transform selection;

    public RaycastHit raycastHit;

    public Vector2 mousePosition;

    //change object color upon hovering over it

    // Constructor
    public UserSelection()
    {
        this.mousePosition = Vector2.zero;
    }
    public UserSelection(Vector2 mousePosition)
    {
        this.mousePosition = mousePosition;
    }
    // set an updated mouse position
    public void setUpdatedMousePosition(Vector2 mousePosition)
    {
        this.mousePosition = mousePosition;
    }


    public void HandleHighlight()
    {
        // TODO
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        ray.direction = Camera.main.transform.forward;
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                Debug.Log("highlight hit:   " + highlight.transform.name);

                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }

    //change object color upon selecting/clicking on it
    public void HandleSelection()
    {
        // TODO
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selection = raycastHit.transform;
                if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                    // GameObject closestObject = GameManager.Instance.GetClosestObject(mousePosition);
                    GameManager.Instance.SetActiveGameObject(selection.gameObject);
                    Debug.Log("Selected Object: " + selection.name);
                }
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                }
            }

            
        }
    }


}
