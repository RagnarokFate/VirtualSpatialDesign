using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserSelection
{
    private Material highlightMaterial;
    private Material selectionMaterial;

    private Material defaultMaterial;

    private GameObject selectedObject;
    private GameObject highlightedObject;

    public UserSelection(Material highlightInputMaterial, Material selectionInputMaterial)
    {
        highlightMaterial = highlightInputMaterial;
        selectionMaterial = selectionInputMaterial;

        defaultMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

    }

    // Add these fields to store the default material of the highlighted and selected objects separately
    private Material highlightedObjectDefaultMaterial;
    private Material selectedObjectDefaultMaterial;

    public void HandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Mouse Clicked at pos: " + hit.point.ToString());

            if (selectedObject != null)
            {
                // Restore the default material of the previously selected object
                selectedObject.GetComponent<MeshRenderer>().material = selectedObjectDefaultMaterial;
                selectedObject.GetComponent<MeshRenderer>().sharedMaterial = selectedObjectDefaultMaterial;
            }

            if (highlightedObject != null)
            {
                // Select the highlighted object
                selectedObject = hit.collider.gameObject;
                if (selectedObject.GetComponent<MeshRenderer>().material != selectionMaterial && selectedObject.CompareTag("Selectable"))
                {
                    string text = "Selected object :" + selectedObject.name + " at hit position of : " + hit.point.ToString();
                    FadeOutText.Show(3f, Color.blue, text, new Vector2(0, 350), GameObject.Find("MainMenuLayout").GetComponent<Canvas>().transform);

                    GameManager.Instance.SetActiveGameObject(selectedObject);
                    // Store the default material of the selected object
                    selectedObjectDefaultMaterial = selectedObject.GetComponent<MeshRenderer>().sharedMaterial;
                    selectedObject.GetComponent<MeshRenderer>().material = selectionMaterial;
                }
                highlightedObject = null;
            }
            else
            {
                // If no object is highlighted, just deselect the current object
                selectedObject = null;
            }
        }
    }

    public void HandleHighlight()
    {
        if (highlightedObject != null)
        {
            // Restore the default material of the previously highlighted object
            highlightedObject.GetComponent<MeshRenderer>().sharedMaterial = highlightedObjectDefaultMaterial;
            highlightedObject.GetComponent<MeshRenderer>().material = highlightedObjectDefaultMaterial;
            highlightedObject = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
        {
            highlightedObject = hit.collider.gameObject;
            if (highlightedObject.CompareTag("Selectable"))
            {
                // Store the default material of the highlighted object
                highlightedObjectDefaultMaterial = highlightedObject.GetComponent<MeshRenderer>().sharedMaterial;
                highlightedObject.GetComponent<MeshRenderer>().material = highlightMaterial;
            }
            else
            {
                highlightedObject = null;
            }
        }
    }



}
