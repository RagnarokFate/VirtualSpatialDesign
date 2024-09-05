using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;

public class FaceSubtraction
{
    private List<Vector3> vertices;
    private List<Face> faces;

    private Face mainFace;
    private Face subFace;

    public FaceSubtraction(List<Vector3> vertices, List<Face> faces)
    {
        this.vertices = vertices;
        this.faces = faces;
    }

    public FaceSubtraction(List<Vector3> vertices, List<Face> faces, Face mainFace, Face subFace)
    {
        this.vertices = vertices;
        this.faces = faces;
        this.mainFace = mainFace;
        this.subFace = subFace;
    }

    /// <summary>
    /// Subtracts subFace from mainFace, modifies the mesh, and returns the intersection points.
    /// </summary>
    public void SubtractFace(ProBuilderMesh pbMesh, Face mainFace, Face subFace)
    {
        List<Vector3> meshVertices = pbMesh.positions.ToList();
        List<Face> meshFaces = pbMesh.faces.ToList();

        // Get main face and subface indices
        List<int> mainFaceIndices = mainFace.distinctIndexes.ToList();
        List<int> subFaceIndices = subFace.distinctIndexes.ToList();


        mainFaceIndices = GetOrderedRectangleIndices(mainFaceIndices.ToArray(), meshVertices.ToList()).ToList();
        subFaceIndices = GetOrderedRectangleIndices(subFaceIndices.ToArray(), meshVertices.ToList()).ToList();

        List<Vector3> intersectionPoints = new List<Vector3>();

        // Find intersection points between mainFace and subFace edges
        for (int i = 0; i < mainFaceIndices.Count; i++)
        {
            Vector3 mainStart = meshVertices[mainFaceIndices[i]];
            Vector3 mainEnd = meshVertices[mainFaceIndices[(i + 1) % mainFaceIndices.Count]];

            for (int j = 0; j < subFaceIndices.Count; j++)
            {
                Vector3 subStart = meshVertices[subFaceIndices[j]];
                Vector3 subEnd = meshVertices[subFaceIndices[(j + 1) % subFaceIndices.Count]];

                if (LineSegmentsIntersect(mainStart, mainEnd, subStart, subEnd, out Vector3 intersection))
                {
                    intersectionPoints.Add(intersection);
                }
            }
        }

        // Generate new subfaces based on intersection points
        List<Face> subFaces;
        List<Vector3> modifiedVertices;
        (subFaces,modifiedVertices) = CreateSubFaces(intersectionPoints, mainFaceIndices, subFaceIndices, meshVertices);

        RemoveFaceWithoutRemovingVertices(pbMesh, mainFace);
        meshFaces.AddRange(subFaces);

        // Update the mesh with new vertices and faces
        pbMesh.RebuildWithPositionsAndFaces(modifiedVertices, meshFaces);
        pbMesh.ToMesh();
        pbMesh.Refresh();
    }

    /// <summary>
    /// Determines if two line segments intersect, and returns the intersection point.
    /// </summary>
    private bool LineSegmentsIntersect(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2, out Vector3 intersection)
    {
        intersection = Vector3.zero;

        Vector3 r = p2 - p1;
        Vector3 s = q2 - q1;
        float rxs = Vector3.Cross(r, s).magnitude;

        Vector3 pq = q1 - p1;

        float pqxr = Vector3.Cross(pq, r).magnitude;
        float pqxs = Vector3.Cross(pq, s).magnitude;

        // Collinear case, check for overlap
        if (rxs < Mathf.Epsilon && pqxr < Mathf.Epsilon)
        {
            float t0 = Vector3.Dot(pq, r) / Vector3.Dot(r, r);
            float t1 = t0 + Vector3.Dot(s, r) / Vector3.Dot(r, r);

            if (Mathf.Max(0, t0) <= Mathf.Min(1, t1))
            {
                intersection = p1 + Mathf.Max(0, t0) * r;
                return true;
            }
            return false;
        }

        // Parallel and non-intersecting case
        if (rxs < Mathf.Epsilon)
            return false;

        // Calculate t and u values for intersection
        float t = Vector3.Cross(pq, s).magnitude / rxs;
        float u = Vector3.Cross(pq, r).magnitude / rxs;

        if (t >= 0 && u >= 0)
        {
            intersection = p1 + t * r;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates subfaces based on the intersection points and vertices.
    /// </summary>
    private (List<Face>, List<Vector3>) CreateSubFaces(List<Vector3> intersectionPoints, List<int> mainFaceIndices, List<int> subFaceIndices, List<Vector3> meshVertices)
    {
        List<Face> subFaces = new List<Face>();
        List<Vector3> modifiedVertices = new List<Vector3>(vertices);
        vertices.AddRange(intersectionPoints);


        // Extract the main face and subface vertex indices
        int AIndex = mainFaceIndices[0]; // Main face vertex A
        int BIndex = mainFaceIndices[1]; // Main face vertex B
        int CIndex = mainFaceIndices[2]; // Main face vertex C
        int DIndex = mainFaceIndices[3]; // Main face vertex D

        int WIndex = subFaceIndices[0];  // Subface vertex W
        int XIndex = subFaceIndices[1];  // Subface vertex X
        int YIndex = subFaceIndices[2];  // Subface vertex Y
        int ZIndex = subFaceIndices[3];  // Subface vertex Z


        // Add intersection points to the vertex list and get their indices
        int I1Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[1]); // Intersection AB & WZ
        int I2Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[0]); // Intersection AB & XY
        int I3Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[2]); // Intersection BC & WX
        int I4Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[3]); // Intersection BC & YZ
        int I5Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[4]); // Intersection CD & ZY
        int I6Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[5]); // Intersection CD & WX
        int I7Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[7]); // Intersection DA & XW
        int I8Index = modifiedVertices.Count; modifiedVertices.Add(intersectionPoints[6]); // Intersection DA & ZW


        // Extract indices for main and subfaces
        int A = mainFaceIndices[0], B = mainFaceIndices[1], C = mainFaceIndices[2], D = mainFaceIndices[3];
        int W = subFaceIndices[0], X = subFaceIndices[1], Y = subFaceIndices[2], Z = subFaceIndices[3];

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
        // subFaces.Add(CreateRectangle(WIndex, XIndex, YIndex, ZIndex));

        // Middle-right rectangle: X - I3 - I4 - Y (Middle-right area between BC and YZ)
        subFaces.Add(CreateRectangle(XIndex, I3Index, I4Index, YIndex));

        // Bottom-left rectangle: I7 - Z - I6 - D (Bottom-left area between AD and ZX)
        subFaces.Add(CreateRectangle(I7Index, ZIndex, I6Index, DIndex));

        // Bottom-middle rectangle: Z - Y - I5 - I6 (Bottom-middle area between CD and ZY)
        subFaces.Add(CreateRectangle(ZIndex, YIndex, I5Index, I6Index));

        // Bottom-right rectangle: Y - I4 - C - I5 (Bottom-right area between CD and YZ)
        subFaces.Add(CreateRectangle(YIndex, I4Index, CIndex, I5Index));

        return (subFaces, modifiedVertices);
    }

    /// <summary>
    /// Helper function to create a rectangle face from 4 vertices.
    /// </summary>
    private Face CreateRectangle(int AIndex, int BIndex, int CIndex, int DIndex)
    {
        int[] indices = new int[] { AIndex, BIndex, CIndex, CIndex, DIndex, AIndex };
        return new Face(indices);
    }


    public static int[] GetOrderedRectangleIndices(int[] rectangleIndices, List<Vector3> vertices)
    {
        if (rectangleIndices.Length != 4)
        {
            throw new System.ArgumentException("There must be exactly 4 indices to form a rectangle.");
        }

        // Extract the four rectangle vertices
        Vector3[] rectangleVertices = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            rectangleVertices[i] = vertices[rectangleIndices[i]];
        }

        // Calculate the centroid (average of all vertex positions)
        Vector3 centroid = (rectangleVertices[0] + rectangleVertices[1] + rectangleVertices[2] + rectangleVertices[3]) / 4.0f;

        // Calculate the angle from the centroid to each vertex
        var vertexAngles = rectangleIndices.Select(index =>
        {
            Vector3 vertex = vertices[index];
            float angle = Mathf.Atan2(vertex.z - centroid.z, vertex.x - centroid.x); // Angle in the x-z plane
            return new { Index = index, Angle = angle };
        }).ToList();

        // Sort vertices by angle in a counterclockwise (or clockwise) manner
        var sortedVertexAngles = vertexAngles.OrderBy(v => v.Angle).ToList();

        // Return the indices in the correct order
        return sortedVertexAngles.Select(v => v.Index).ToArray();
    }

    public void RemoveFaceWithoutRemovingVertices(ProBuilderMesh pbMesh, Face faceToRemove)
    {
        // Get the current list of faces
        List<Face> faces = new List<Face>(pbMesh.faces);

        // Remove the target face
        faces.Remove(faceToRemove);

        // Assign the updated faces list back to the mesh
        pbMesh.faces = faces;

        // Rebuild the mesh to apply the changes
        pbMesh.ToMesh();
        pbMesh.Refresh();
    }
}
