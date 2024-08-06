using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEditor : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEditModeScene()
    {
        if (GameManager.Instance.activeGameObject != null)
        {
            DontDestroyOnLoad(GameManager.Instance.activeGameObject);
        }
        
        SceneManager.LoadScene("Editor");
    }

    public void UnloadEditModeScene()
    {
        SceneManager.UnloadSceneAsync("Editor");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.activeGameObject != null)
        {
            GameObject transferredObject = GameManager.Instance.activeGameObject;
            transferredObject.transform.position = new Vector3(0, 0, 0); // Set the position as needed
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
