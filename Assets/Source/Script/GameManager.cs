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


    private void Start()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<GameManager>();

            GameObject singletonObject = new GameObject();
            _instance = singletonObject.AddComponent<GameManager>();
            singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
        }
    }


    // List of game objects
    public List<GameObject> gameObjectList = new List<GameObject>();

    // Active game object
    public GameObject activeGameObject;

    // Active tool (Assuming Tool is a class you've defined)
    public Tool currentTool = Tool.none;
    public Tool lastTool = Tool.none;


    //Editor Parameters
    public EditorTool currentEditorTool = EditorTool.none;
    public EditorTool lastEditorTool = EditorTool.none;
    public SelectModeToEdit selectModeToEdit = SelectModeToEdit.none;

    public int twoD_Counter;
    public int threeD_Counter;

    //scene switches
    public Vector3 tempPosition;



    public void AddGameObject(GameObject obj)
    {
        if (!gameObjectList.Contains(obj))
        {
            gameObjectList.Add(obj);
            Debug.Log("Game Object Added: " + obj.name + "| number : " + gameObjectList.Count);
        }
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


    // =====================================================================================================================
    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("GameManager State:");

        sb.AppendLine("Active GameObject:");
        if (activeGameObject != null)
        {
            sb.AppendLine($"  Name: {activeGameObject.name}");
        }
        else
        {
            sb.AppendLine("  None");
        }


        sb.AppendLine($"Current Menu Tool: {currentTool.ToString()}");
        sb.AppendLine($"Current Editor Tool: {currentEditorTool.ToString()}");

        sb.AppendLine($"2D Counter: {twoD_Counter}");
        sb.AppendLine($"3D Counter: {threeD_Counter}");


        sb.AppendLine("Game Objects:");
        if (gameObjectList.Count > 0)
        {
            foreach (var obj in gameObjectList)
            {
                sb.AppendLine($"  {obj.name}");
            }
        }
        else
        {
            sb.AppendLine("  None");
        }

        return sb.ToString();
    }


    public bool IsItAMesh(GameObject gameObject)
    {
        if (gameObject.transform.parent.name == "Meshs")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetEditorGameObject(GameObject gameObject)
    {
        activeGameObject = gameObject;
        DontDestroyOnLoad(gameObject);


    }

}

