using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;



public class temp : MonoBehaviour
{
    private GameObject gameObject;

    private List<Vector3> outputVertices = new List<Vector3>();
    private List<Face> outputFaces = new List<Face>();


    void Start()
    {
        gameObject = new GameObject("MainFace");
        Face mainFace = CreateMainFace();
        // Added SubFace
        Face subFace = CreateSubFace(gameObject);

        ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();

        if (pbMesh == null)
        {
            Debug.LogError("No ProBuilderMesh component found on this GameObject.");
            return;
        }

        List<Vector3> vertices = pbMesh.positions.ToList();
        List<Face> faces = pbMesh.faces.ToList();

        // Rearrange the vertices of the main face to have minimum distance between the main face and sub face
        // RearragePoints(vertices, mainFace, subFace);

        // SubtractFace(pbMesh, mainFace, subFace);

        //subtract the face creating 9 new faces and remove the main face aftwards
        FaceSubtraction faceSubtraction = new FaceSubtraction(vertices, faces);
        faceSubtraction.SubtractFace(pbMesh, mainFace, subFace);

        List<Face> facesToExtrude = new List<Face> { subFace };
        
        /*pbMesh.Extrude(facesToExtrude, ExtrudeMethod.FaceNormal, 1.0f);
        pbMesh.ToMesh();
        pbMesh.Refresh();*/
    }

    // step 1 : create both main and sub face
    private Face CreateMainFace()
    {
        // Main GameObject
        List<Vector3> vertices = new List<Vector3>();
        Face mainface = new Face();

        vertices.Add(new Vector3(0, 0, 1)); // 0
        vertices.Add(new Vector3(1, 0, 1)); // 1
        vertices.Add(new Vector3(1, 0, 0)); // 2
        vertices.Add(new Vector3(0, 0, 0)); // 3

                        /*
        (0,0,1)  #A      ------------------ (1,0,1) # B
                        | \              1 |
                        |   \              |
                        |     \            |
            z           |       \          |
                        |          \       |
                        |             \    |
                        |               \  |
                        |                 \|
         (0,0,0) #D     -------------------- (1,0,0) #C
                                 x
                         */

        int[] indices = new int[] { 0, 1, 2, 2, 3, 0 };
        mainface = new Face(indices);


        List<Face> faces = new List<Face>();
        faces.Add(mainface);

        ProBuilderMesh pbMesh = gameObject.AddComponent<ProBuilderMesh>();
        pbMesh.RebuildWithPositionsAndFaces(vertices, faces);
        pbMesh.ToMesh();
        pbMesh.Refresh();


        return mainface;
    }
    private Face CreateSubFace(GameObject gameObject)
    {
        // Main GameObject
        List<Vector3> vertices = new List<Vector3>();
        Face subface = new Face();

        vertices.Add(new Vector3(0.25f, 0, 0.75f)); // 4
        vertices.Add(new Vector3(0.5f, 0, 0.75f)); // 5
        vertices.Add(new Vector3(0.5f, 0, 0.25f)); // 6
        vertices.Add(new Vector3(0.25f, 0, 0.25f)); // 7

                        /*
      (1/4,0,3/4)  #W    ------------------   (1/2,0,3/4) # X
      height = 1/2      | \                |
                        |   \              |
                        |     \            |
            z           |       \          |
                        |          \       |
                        |             \    |
                        |               \  |
                        |                 \|
      (1/4,0,1/4) #Z     -------------------- (1/2,0,1/4) #Y
                             width = 1/4
                                 x
                         */


        int[] indices = new int[] { 4, 5, 6, 6, 7, 4 };
        subface = new Face(indices);


        ProBuilderMesh pbMesh = gameObject.GetComponent<ProBuilderMesh>();
        List<Vector3> GameObject_vertices = pbMesh.positions.ToList();
        List<Face> GameObject_faces = pbMesh.faces.ToList();

        GameObject_vertices.AddRange(vertices);
        GameObject_faces.Add(subface);

        pbMesh.RebuildWithPositionsAndFaces(GameObject_vertices, GameObject_faces);
        pbMesh.ToMesh();
        pbMesh.Refresh();
        return subface;

    }

    private void SubtractFace(ProBuilderMesh pbMesh, Face mainFace, Face subFace)
    {
        // (List<Face>, List<Vector3>)
        List<Vector3> vertices = pbMesh.positions.ToList();
        List<Face> faces = pbMesh.faces.ToList();

        // Convert ReadOnlyCollection<int> to List<int>
        List<int> mainFaceIndices = mainFace.distinctIndexes.ToList();
        List<int> subFaceIndices = subFace.distinctIndexes.ToList();

        // Iterate through each edge of the main face
        for (int i = 0; i < mainFaceIndices.Count; i++)
        {
            Vector3 start = vertices[mainFaceIndices[i]];
            Vector3 end = vertices[mainFaceIndices[(i + 1) % mainFaceIndices.Count]];

            // Check for intersection with sub face edges
            for (int j = 0; j < subFaceIndices.Count; j++)
            {
                Vector3 subStart = vertices[subFaceIndices[j]];
                Vector3 subEnd = vertices[subFaceIndices[(j + 1) % subFaceIndices.Count]];

                if (LineSegmentsIntersect(start, end, subStart, subEnd, out Vector3 intersection))
                {
                    //Debug.Log("Intersection found at " + intersection);
                    outputVertices.Add(intersection);
                }
            }
        }

        createSubFaces(outputVertices, mainFaceIndices, subFaceIndices, vertices);


    }

    private bool LineSegmentsIntersect(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2, out Vector3 intersection)
    {
        intersection = Vector3.zero;

        Vector3 r = p2 - p1;
        Vector3 s = q2 - q1;
        float rxs = Vector3.Cross(r, s).magnitude;

        Vector3 pq = q1 - p1;

        float pqxr = Vector3.Cross(pq, r).magnitude;
        float pqxs = Vector3.Cross(pq, s).magnitude;

        // Check if r and s are collinear and overlapping
        if (rxs < Mathf.Epsilon && pqxr < Mathf.Epsilon)
        {
            // Collinear case, check for overlap
            float t0 = Vector3.Dot(pq, r) / Vector3.Dot(r, r);
            float t1 = t0 + Vector3.Dot(s, r) / Vector3.Dot(r, r);

            if (Mathf.Max(0, t0) <= Mathf.Min(1, t1))
            {
                intersection = p1 + Mathf.Max(0, t0) * r; // or some other midpoint if needed
                return true;
            }
            return false;
        }

        // Check if r and s are parallel and non-intersecting
        if (rxs < Mathf.Epsilon)
            return false;

        // Calculate the t and u values
        float t = Vector3.Cross(pq, s).magnitude / rxs;
        float u = Vector3.Cross(pq, r).magnitude / rxs;

        // Check if the segments intersect in the extended/inverted direction
        if (t >= 0 && u >= 0)
        {
            intersection = p1 + t * r;
            return true;
        }

        return false;
    }

    private List<Face> createSubFaces(List<Vector3> intersectionPoints, List<int> mainFaceIndices, List<int> subFaceIndices, List<Vector3> vertices)
    {
        List<Face> subFaces = new List<Face>();
        List<Vector3> allVertices = new List<Vector3>(vertices); // Combine vertices and intersection points

        // Add intersection points to the vertex list and get their indices
        int I1Index = allVertices.Count; allVertices.Add(intersectionPoints[1]); // Intersection AB & WZ
        int I2Index = allVertices.Count; allVertices.Add(intersectionPoints[0]); // Intersection AB & XY
        int I3Index = allVertices.Count; allVertices.Add(intersectionPoints[2]); // Intersection BC & WX
        int I4Index = allVertices.Count; allVertices.Add(intersectionPoints[3]); // Intersection BC & YZ
        int I5Index = allVertices.Count; allVertices.Add(intersectionPoints[4]); // Intersection CD & ZY
        int I6Index = allVertices.Count; allVertices.Add(intersectionPoints[5]); // Intersection CD & WX
        int I7Index = allVertices.Count; allVertices.Add(intersectionPoints[7]); // Intersection DA & XW
        int I8Index = allVertices.Count; allVertices.Add(intersectionPoints[6]); // Intersection DA & ZW

        // Extract the main face and subface vertex indices
        int AIndex = mainFaceIndices[0]; // Main face vertex A
        int BIndex = mainFaceIndices[1]; // Main face vertex B
        int CIndex = mainFaceIndices[2]; // Main face vertex C
        int DIndex = mainFaceIndices[3]; // Main face vertex D

        int WIndex = subFaceIndices[0];  // Subface vertex W
        int XIndex = subFaceIndices[1];  // Subface vertex X
        int YIndex = subFaceIndices[2];  // Subface vertex Y
        int ZIndex = subFaceIndices[3];  // Subface vertex Z

        // Create subfaces (rectangles) using all 8 intersection points and relevant vertices

        // Top-left rectangle: A - I1 - W - I8 (Top-left area)
        subFaces.Add(CreateRectangle(AIndex, I1Index, WIndex, I8Index));

        // Top-middle rectangle: I1 - I2 - X - W (Top-middle area between AB and WX)
        subFaces.Add(CreateRectangle(I1Index, I2Index, XIndex, WIndex));

        // Top-right rectangle: I2 - B - I3 - X (Top-right area between BC and XY)
        subFaces.Add(CreateRectangle(I2Index, BIndex, I3Index, XIndex));

        // Middle-left rectangle: I8 - W - Z - I7 (Middle-left area between AD and ZW)
        subFaces.Add(CreateRectangle(I8Index, WIndex, ZIndex, I7Index));

        // Center rectangle (subface): W - X - Y - Z (Subface rectangle in the center)
        subFaces.Add(CreateRectangle(WIndex, XIndex, YIndex, ZIndex));

        // Middle-right rectangle: X - I3 - I4 - Y (Middle-right area between BC and YZ)
        subFaces.Add(CreateRectangle(XIndex, I3Index, I4Index, YIndex));

        // Bottom-left rectangle: I7 - Z - I6 - D (Bottom-left area between AD and ZX)
        subFaces.Add(CreateRectangle(I7Index, ZIndex, I6Index, DIndex));

        // Bottom-middle rectangle: Z - Y - I5 - I6 (Bottom-middle area between CD and ZY)
        subFaces.Add(CreateRectangle(ZIndex, YIndex, I5Index, I6Index));

        // Bottom-right rectangle: Y - I4 - C - I5 (Bottom-right area between CD and YZ)
        subFaces.Add(CreateRectangle(YIndex, I4Index, CIndex, I5Index));

        return subFaces;
    }

    // Helper function to create a rectangle face given 4 vertex indices
    private Face CreateRectangle(int AIndex, int BIndex, int CIndex, int DIndex)
    {
        // Define the face indices in a counterclockwise manner for the 2 triangles making the rectangle
        int[] indices = new int[] { AIndex, BIndex, CIndex, CIndex, DIndex, AIndex };
        return new Face(indices);
    }

    
}
