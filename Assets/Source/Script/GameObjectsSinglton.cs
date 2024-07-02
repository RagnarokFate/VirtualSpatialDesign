using System.Collections.Generic;
using UnityEngine;

public class GameObjectsSingleton : MonoBehaviour
{
    private static GameObjectsSingleton _instance;

    public static GameObjectsSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameObjectsSingleton>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameObjectsSingleton>();
                    singletonObject.name = typeof(GameObjectsSingleton).ToString() + " (Singleton)";

                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }

    private List<GameObject> gameObjects = new List<GameObject>();

    // Prevent the constructor from being used.
    private GameObjectsSingleton() { }

    void Awake()
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

    public void AddGameObject(GameObject obj)
    {
        if (!gameObjects.Contains(obj))
        {
            gameObjects.Add(obj);
        }
    }

    public List<GameObject> GetGameObjects()
    {
        return new List<GameObject>(gameObjects);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
