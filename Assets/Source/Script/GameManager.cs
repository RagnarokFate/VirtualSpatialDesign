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

    // Prevent the Singleton from being destroyed when scenes are loaded
  /*  private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log($"{this} has become the manager and was set DDOL");
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.Log($"{this} was Destroyed because a manager already exists");
            Destroy(gameObject);
        }
    }*/



    // List of game objects
    public List<GameObject> gameObjectList = new List<GameObject>();

    // Active game object
    public GameObject activeGameObject;

    // Active tool (Assuming Tool is a class you've defined)
    public MainTool currentMainTool = MainTool.none;
    public TransformTool currentTransformTool = TransformTool.none;
    public BrushTool currentBrushTool = BrushTool.none; // push pull, extrude, etc.

    public int twoD_Counter;
    public int threeD_Counter;

    //setting default values for drawline
    public Color drawLineColor;
    //public Material drawLineMaterial = new Material(Shader.Find("Sprites/Default"));
    public Material drawLineMaterial;


    //Editor Parameters
    public SelectModeToEdit selectModeToEdit = SelectModeToEdit.Vertex;

    public void AddGameObject(GameObject obj)
    {
        if (!gameObjectList.Contains(obj))
        {
            gameObjectList.Add(obj);
        }
        Debug.Log("Game Object Added: " + obj.name + "| number : " + gameObjectList.Count);
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


        sb.AppendLine("Current Main Tool:");
        if (currentMainTool != null)
        {
            sb.AppendLine($"  Tool: {currentMainTool.ToString()}");
        }
        else
        {
            sb.AppendLine("  None");
        }

        sb.AppendLine("Current Transform Tool:");
        if (currentTransformTool != null)
        {
            sb.AppendLine($"  Tool: {currentTransformTool.ToString()}");
        }
        else
        {
            sb.AppendLine("  None");
        }

        sb.AppendLine("Current Brush Tool:");
        if (currentBrushTool != null)
        {
            sb.AppendLine($"  Tool: {currentBrushTool.ToString()}");
        }
        else
        {
            sb.AppendLine("  None");
        }

        sb.AppendLine($"2D Counter: {twoD_Counter}");
        sb.AppendLine($"3D Counter: {threeD_Counter}");

        sb.AppendLine("Draw Line Color:");
        sb.AppendLine($"  Color: {drawLineColor}");

        sb.AppendLine("Draw Line Material:");
        if (drawLineMaterial != null)
        {
            sb.AppendLine($"  Material: {drawLineMaterial.name}");
        }
        else
        {
            sb.AppendLine("  None");
        }

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

