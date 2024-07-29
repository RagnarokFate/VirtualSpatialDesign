using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;


//class that will manage the game objects and tools and data
public class GameManager : MonoBehaviour
{
    // Static instance for Singleton
    private static GameManager _instance;

    // Public property to access the instance
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    // Prevent the Singleton from being destroyed when scenes are loaded
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // List of game objects
    public List<GameObject> gameObjectList = new List<GameObject>();

    // Active game object
    public GameObject activeGameObject;
    public int activeGameObjectIndex;

    // Active ProBuilder Object
    public ProBuilderMesh activeProBuilderObject;

    // Active tool (Assuming Tool is a class you've defined)
    public MainTool currentMainTool;
    public TransformTool currentTransformTool;
    public BrushTool currentBrushTool; // push pull, extrude, etc.

    public int twoD_Counter;
    public int threeD_Counter;

    //setting default values for drawline
    public Color drawLineColor;
    //public Material drawLineMaterial = new Material(Shader.Find("Sprites/Default"));
    public Material drawLineMaterial;


    // Methods to manipulate the game objects, tools, etc.
    

    public void AddGameObject(GameObject obj)
    {
        if (!gameObjectList.Contains(obj))
        {
            gameObjectList.Add(obj);
        }
        Debug.Log("Game Object Added: " + obj.name + "number : " + gameObjectList.Count);
    }

    public List<GameObject> GetGameObjects()
    {
        return new List<GameObject>(gameObjectList);
    }

    // =====================================================================================================================
    // Getter and Setter
    public void SetActiveGameObject(GameObject gameObject)
    {
        activeGameObject = gameObject;
    }
    public void setActiveGameObjectIndex(int index)
    {
        activeGameObjectIndex = index;
    }

    public void SetActiveProBuilderObject(ProBuilderMesh proBuilderObject)
    {
        activeProBuilderObject = proBuilderObject;
    }

    public void SetCurrentMainTool(MainTool tool)
    {
        currentMainTool = tool;
    }

    public void SetCurrentTransformTool(TransformTool tool)
    {
        currentTransformTool = tool;
    }

    public void SetCurrentBrushTool(BrushTool brushKit)
    {
        currentBrushTool = brushKit;
    }

    


    // GetClosestObject creted to get the closest object to the mouse position
    public GameObject GetClosestObject(Vector3 mousePosition)
    {
        GameObject closestObject = null;
        float minDistance = float.MaxValue;

        foreach (GameObject obj in gameObjectList)
        {
            Vector3 objPosition = Camera.main.WorldToScreenPoint(obj.transform.position);
            float distance = Vector3.Distance(objPosition, mousePosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestObject = obj;
            }
        }

        if (closestObject != null)
        {
            Debug.Log("Closest Object: " + closestObject.name);
        }
        else
        {
            Debug.Log("No Object Found");
        }

        return closestObject;
    }

}

