using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    string MeshModel_name = "Cube";

    List<Vector3> Vertices = new List<Vector3>();
    List<Vector3> Faces = new List<Vector3>();
    List<Vector2> TextureCoordination = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        // Define cube vertices
        Vertices.Add(new Vector3(-1, -1, -1)); // 0
        Vertices.Add(new Vector3(1, -1, -1));  // 1
        Vertices.Add(new Vector3(1, 1, -1));   // 2
        Vertices.Add(new Vector3(-1, 1, -1));  // 3
        Vertices.Add(new Vector3(-1, -1, 1));  // 4
        Vertices.Add(new Vector3(1, -1, 1));   // 5
        Vertices.Add(new Vector3(1, 1, 1));    // 6
        Vertices.Add(new Vector3(-1, 1, 1));   // 7

        // Define cube faces (triangles)
        Faces.Add(new Vector3( 0, 1, 2 ));  // Front face
        Faces.Add(new Vector3( 0, 2, 3 ));
        Faces.Add(new Vector3( 1, 5, 6 ));  // Right face
        Faces.Add(new Vector3( 1, 6, 2 ));
        Faces.Add(new Vector3( 5, 4, 7 ));  // Back face
        Faces.Add(new Vector3( 5, 7, 6 ));
        Faces.Add(new Vector3( 4, 0, 3 ));  // Left face
        Faces.Add(new Vector3( 4, 3, 7 ));
        Faces.Add(new Vector3( 3, 2, 6 ));  // Top face
        Faces.Add(new Vector3( 3, 6, 7 ));
        Faces.Add(new Vector3( 4, 5, 1 ));  // Bottom face
        Faces.Add(new Vector3( 4, 1, 0 ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector3> GetVertices() { 
        return Vertices; 
    }

    public List<Vector3> GetFaces() {  
        return Faces; 
    }
    public string GetName()
    {
        return MeshModel_name;
    } 
    public List<Vector2> GetTextureCoordinations() 
    { 
        return TextureCoordination;
    }
}
