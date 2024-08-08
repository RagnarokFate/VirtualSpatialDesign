using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class DisplayGameObject : MonoBehaviour
{
    private Transform mainTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObject()
    {
        GameManager.Instance.activeGameObject.transform.position = mainTransform.position;
    }

    public void DisplayObject()
    {
        foreach (GameObject gameObject in GameManager.Instance.GetGameObjects())
        {
            if (gameObject == GameManager.Instance.activeGameObject)
            {
                continue;
            }
            gameObject.SetActive(false);
        }
    }

    public void DisplayAllObjects()
    {
        foreach (GameObject gameObject in GameManager.Instance.GetGameObjects())
        {
            if (gameObject == GameManager.Instance.activeGameObject)
            {
                gameObject.SetActive(true);
            }
            
        }
    }
}
