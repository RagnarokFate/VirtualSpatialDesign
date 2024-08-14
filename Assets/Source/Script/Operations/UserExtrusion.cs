using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

public class UserExtrusion
{
    private bool locked;
    private Vector2 initialMousePos;
    private float initialExtrusion;
    private bool isExtruding;



    public bool meshExtrusionLock;
    public float extrusionSensitivity = 0.1f;

    public UserExtrusion()
    {
        meshExtrusionLock = false;
        locked = false;
        isExtruding = false;
    }

    //reset back object color upon deselecting/unclicking Active GameObject
    public void HandleExtrusion()
    {
        if (Input.GetMouseButtonDown(0))
        {            
            ProBuilderMesh pbMesh = GameManager.Instance.activeGameObject.GetComponent<ProBuilderMesh>();

            // List<Face> facesToExtrude = GameManager.Instance.editorfaces;
            List<Face> facesToExtrude = new List<Face> { pbMesh.faces[0] };
            // List<Face> facesToExtrude = new List<Face> { pbMesh.faces[0], pbMesh.faces[1] };


            float extrusionFactor = 1.0f;

            pbMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, extrusionFactor);
            pbMesh.ToMesh();
            pbMesh.Refresh();

        }

    }

    /*public void HandleExtrusion()
    {
        GameManager.Instance.currentTransformTool = TransformTool.none;
        GameManager.Instance.currentMainTool = MainTool.none;

        GameObject gameObject = GameManager.Instance.activeGameObject;

        if (gameObject != null)
        {

            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                float extrusionFactor;
                if (Input.GetMouseButtonDown(0)) // Start Extrusion on mouse down
                {
                    extrusionFactor = 0.0f;
                    initialMousePos = mousePos;
                    initialExtrusion = 0.0f;
                    isExtruding = true;
                }

                if (Input.GetMouseButton(0) && isExtruding) // Continue Extrusion while holding mouse button
                {
                    Vector2 mouseDelta = mousePos - initialMousePos;
                    extrusionFactor = mouseDelta.magnitude; // Adjust Extrusion sensitivity              

                }

                if (Input.GetMouseButtonUp(0)) // Stop Extrusion on mouse up
                {
                    isExtruding = false;

                    ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();

                    // Ensure we have a valid ProBuilder mesh
                    if (pbMesh == null)
                    {
                        Debug.LogError("No ProBuilderMesh component found on this GameObject.");
                        return;
                    }
                    // Get all faces of the mesh
                    var faces = pbMesh.faces;

                    try
                    {
                        // Perform the extrusion
                        //pbMesh.Extrude(faces, ExtrudeMethod.FaceNormal, extrusionFactor);

                        pbMesh.faces = ExtrudeElements.Extrude(pbMesh, pbMesh.faces, ExtrudeMethod.FaceNormal, 10.0f);

                        pbMesh.ToMesh();
                        pbMesh.Refresh();
                    }
                    catch (KeyNotFoundException knfe)
                    {
                        Debug.LogError($"Extrusion failed due to a missing key in the dictionary: {knfe.Message}");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Extrusion failed: {e.Message}");
                    }
                }
            }


        }
        else
        {
            return;
        }

    }*/

    public void LockExtrusion()
    {
        meshExtrusionLock = true;
    }

    public void UnlockExtrusion()
    {
        meshExtrusionLock = false;
    }

}
