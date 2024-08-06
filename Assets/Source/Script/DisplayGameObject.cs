using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class DisplayGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*GameObject gameObject = GameManager.Instance.activeGameObject;
        if (gameObject != null)
        {
            *//*Debug.Log("Active Game Object: " + gameObject.name);

            ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create();

            proBuilderMesh.positions = gameObject.GetComponent<ProBuilderMesh>().positions;
            proBuilderMesh.faces = gameObject.GetComponent<ProBuilderMesh>().faces;

            proBuilderMesh.Refresh();
            proBuilderMesh.ToMesh();


            // Display the game object
            proBuilderMesh.gameObject.transform.position = new Vector3(0, 0, 0); // Set the position as needed*//*

            GameManager.Instance.activeGameObject = obj;
            DontDestroyOnLoad(obj);
        }
        else
        {
            return;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
