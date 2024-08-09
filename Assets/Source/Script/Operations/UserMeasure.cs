using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UserMeasureData
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float distance;
}

public class UserMeasure
{
    private UserMeasureData userMeasureData;
    private bool isMeasuring = false;
    private DrawLine drawLine;

    // Initialize the UserMeasureData and get the DrawLine component
    public UserMeasure(DrawLine drawLine)
    {
        userMeasureData = new UserMeasureData();
        this.drawLine = drawLine;
    }

    // Handle user input for measuring the distance
    public void HandleMeasure()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            StartMeasure();
        }

        if (Input.GetMouseButton(0) && isMeasuring) // Left mouse button held down
        {
            UpdateMeasure();
        }

        if (Input.GetMouseButtonUp(0) && isMeasuring) // Left mouse button released
        {
            EndMeasure();
        }
    }

    private void StartMeasure()
    {
        isMeasuring = true;
        userMeasureData.startPoint = GetMouseWorldPosition();
        drawLine.DestroyLine(); // Reset the line before starting a new measurement
        drawLine.UpdateLine(userMeasureData.startPoint); // Start drawing the line from the first point
    }

    private void UpdateMeasure()
    {
        userMeasureData.endPoint = GetMouseWorldPosition();
        drawLine.DestroyLine(); // Reset the line each time to create a dynamic effect
        drawLine.UpdateLine(userMeasureData.startPoint); // Draw from start point to current position
        drawLine.UpdateLine(userMeasureData.endPoint);   // Draw to current position
    }

    private void EndMeasure()
    {
        isMeasuring = false;
        userMeasureData.endPoint = GetMouseWorldPosition();
        userMeasureData.distance = Vector3.Distance(userMeasureData.startPoint, userMeasureData.endPoint);

        // Draw the final line between the two points
        drawLine.DestroyLine();
        drawLine.UpdateLine(userMeasureData.startPoint);
        drawLine.UpdateLine(userMeasureData.endPoint);

        Debug.Log("Distance: " + userMeasureData.distance.ToString("F2") + " units");
    }

    // Convert mouse position to world position
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set a proper Z distance from the camera
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
