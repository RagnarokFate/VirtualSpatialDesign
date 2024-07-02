using UnityEngine;

public class MeshUtils
{
    private GameObject gameObject;
    private Vector3 center;

    public MeshUtils(GameObject obj)
    {
        if (obj.GetComponent<MeshFilter>() == null)
        {
            throw new System.ArgumentException("The provided GameObject does not have a MeshFilter component.");
        }

        this.gameObject = obj;
        this.center = CalculateMeshCenter();
    }


    private Vector3 CalculateMeshCenter()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
        {
            // Get the bounds of the mesh
            Bounds bounds = meshFilter.mesh.bounds;

            // Calculate the center in local space
            Vector3 localCenter = bounds.center;

            // Convert local center to world space
            return gameObject.transform.TransformPoint(localCenter);
        }

        return gameObject.transform.position; // Fallback to object's position if no mesh is found
    }

    // Get the center of the mesh
    public Vector3 GetCenter()
    {
        return center;
    }
}
