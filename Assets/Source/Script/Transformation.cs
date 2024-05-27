using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Transformation : MonoBehaviour
{
    private Mesh mesh;
    private List<UnityEngine.Vector3> originalVertices;
    private List<Vector3> modifiedVertices;

    private Vector3 Centerpoint;

    private void Awake()
    {
        // Get the mesh component
        Cube mesh = GetComponent<Cube>();

        // Make a copy of the original vertices to use for resetting
        originalVertices = mesh.GetVertices();
    }

    // Translate the mesh vertices by the given amount in each axis
    public void Translate(Vector3 translation)
    {

        for(int i = 0; i < originalVertices.Count; i++) 
        {
            modifiedVertices[i] = originalVertices[i] + translation;
        }

    }

    // Rotate the mesh vertices around the given axis by the specified angle
    public void Rotate(Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        for (int i = 0; i < originalVertices.Count; i++)
        {
            modifiedVertices[i] = rotation * originalVertices[i];
        }

    }

    // Scale the mesh vertices by the given amount in each axis
    public void Scale(Vector3 scale)
    {
        for (int i = 0; i < originalVertices.Count; i++)
        {
            modifiedVertices[i] = Vector3.Scale(originalVertices[i], scale);
        }

    }

    // Reset the mesh vertices to their original positions
    public void ResetMesh() 
    {
        modifiedVertices = originalVertices;
    }

    // Start is called before the first frame update
    void Start()
    {
        float minimum_x = float.PositiveInfinity;
        float minimum_y = float.PositiveInfinity;
        float minimum_z = float.PositiveInfinity;

        float maximum_x = float.NegativeInfinity;
        float maximum_y = float.NegativeInfinity;
        float maximum_z = float.NegativeInfinity;

        foreach (var vector in originalVertices)
        {
            minimum_x = math.min(vector.x, minimum_x);
            minimum_y = math.min(vector.y, minimum_y);
            minimum_z = math.min(vector.z, minimum_z);

            maximum_x = math.max(vector.x, maximum_x);
            maximum_y = math.max(vector.y, maximum_y);
            maximum_z = math.max(vector.z, maximum_z);


        }
        Vector3 MinimumVector = new Vector3(minimum_x, minimum_y, minimum_z);
        Vector3 MaximumVector = new Vector3(maximum_x,maximum_y,maximum_z);

        Vector3 center = (MinimumVector + MaximumVector) / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
 