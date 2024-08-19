using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class UserExtrusion
{
    private bool locked;
    private Vector2 initialMousePos;
    private float extrusionValue;
    private bool isExtruding;

    public bool meshExtrusionLock;
    public float extrusionSensitivity = 0.1f;

    private Face selectedFace;

    public UserExtrusion()
    {
        meshExtrusionLock = false;
        locked = false;
        isExtruding = false;
    }

    // Reset the object color upon deselecting/unclicking Active GameObject
    public void HandleExtrusion()
    {
        GameObject gameObject = GameManager.Instance.activeGameObject;

        if (gameObject != null)
        {
            ProBuilderMesh proBuilderMesh = gameObject.GetComponent<ProBuilderMesh>();
            if (proBuilderMesh == null)
            {
                Debug.LogError("No ProBuilderMesh component found on this GameObject.");
                return;
            }

            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Left-click to select a face
                if (Input.GetMouseButtonDown(0) && !locked)
                {
                    selectedFace = new UserSelectEditor(proBuilderMesh).getCloestFace(hit.point);
                    if (selectedFace != null)
                    {
                        Debug.Log("Selected Face: " + selectedFace);
                        proBuilderMesh.SetFaceColor(selectedFace, Color.red);
                        locked = true;
                    }
                }

                // Right-click to deselect the face
                if (Input.GetMouseButtonDown(1) && locked)
                {
                    if (selectedFace != null)
                    {
                        proBuilderMesh.SetFaceColor(selectedFace, Color.white);
                    }
                    locked = false;
                }

                // Start extrusion on left-click and hold
                if (Input.GetMouseButtonDown(0) && locked && selectedFace != null)
                {
                    initialMousePos = mousePos;
                    extrusionValue = 0f;
                    isExtruding = true;
                }

                // Continue extrusion while holding the left mouse button
                if (Input.GetMouseButton(0) && isExtruding && selectedFace != null)
                {
                    Vector2 mouseDelta = mousePos - initialMousePos;
                    float mouseMagnitude = mouseDelta.magnitude;
                    extrusionValue = mouseMagnitude * extrusionSensitivity;

                    // Optional: Preview the extrusion in real-time
                    List<Face> facesToExtrude = new List<Face> { selectedFace };
                    proBuilderMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, extrusionValue);
                    proBuilderMesh.ToMesh();
                    proBuilderMesh.Refresh();
                }

                // Stop extrusion on left mouse button release
                if (Input.GetMouseButtonUp(0) && isExtruding && selectedFace != null)
                {
                    Debug.Log("Final Extrusion Value: " + extrusionValue);

                    isExtruding = false;
                    locked = false;

                    // Optionally finalize the extrusion (repeating the extrusion step to apply it)
                    List<Face> facesToExtrude = new List<Face> { selectedFace };
                    proBuilderMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, extrusionValue);

                    proBuilderMesh.ToMesh();
                    proBuilderMesh.Refresh();
                }
            }
        }
    }

    public void LockExtrusion()
    {
        meshExtrusionLock = true;
    }

    public void UnlockExtrusion()
    {
        meshExtrusionLock = false;
    }
}
