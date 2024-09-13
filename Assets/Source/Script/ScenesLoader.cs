using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{

    // ================== Edit Mode Scene ==================
    public void LoadEditModeScene()
    {
        GameObject objects = GameObject.Find("Objects");
        if (GameManager.Instance.activeGameObject == null)
        {
            Debug.Log($"There wasn't an active object to DDOL Before entering Editor Scene");
            return;
        }
        
        DontDestroyOnLoad(objects);

        DisplaySoloObject();
        StoreObjectTransform();
        resetTools();
        SceneManager.LoadScene("Editor");

        //set the active object to the center of the scene
        GameManager.Instance.activeGameObject.transform.position = new Vector3(0, 0, 0);
        //GameManager.Instance.activeGameObject.transform.parent = GameObject.Find("EditGameObject").transform;

    }

    public void DisplaySoloObject()
    {
        foreach (GameObject gameObject in GameManager.Instance.GetGameObjects())
        {
            if (gameObject == GameManager.Instance.activeGameObject)
            {
                // render the mesh with transparent material
                gameObject.SetActive(true);
                continue;
            }
            gameObject.SetActive(false);
        }
    }

    public void StoreObjectTransform()
    {
        GameManager.Instance.tempPosition = GameManager.Instance.activeGameObject.transform.position;
    }


    // ===================== Main Scene =====================
    public void LoadMainScene()
    {
        GameObject objects = GameObject.Find("Objects");
        if (GameManager.Instance.activeGameObject == null)
        {
            Debug.Log($"There wasn't an active object to DDOL Before entering Main Scene");
            return;
        }
        DontDestroyOnLoad(objects);


        DisplayAllObjects();
        LoadObjectTransform();
        resetTools();

        SceneManager.LoadScene("Main");

    }


    public void DisplayAllObjects()
    {
        foreach (GameObject gameObject in GameManager.Instance.GetGameObjects())
        {
            gameObject.SetActive(true);

        }
    }

    public void LoadObjectTransform()
    {
        GameManager.Instance.activeGameObject.transform.position = GameManager.Instance.tempPosition;
    }

    protected void resetTools()
    {
        GameManager.Instance.currentEditorTool = EditorTool.none;
        GameManager.Instance.currentTool = Tool.none;
        GameManager.Instance.lastTool = Tool.none;

    }


    // ===================== Settings Scene =====================
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }


    // ===================== Start Scene =====================
    public void LoadWelcomeScene()
    {
        SceneManager.LoadScene("Welcome");
    }

    // ===================== Start Scene =====================
    public void InitalizeMainScene()
    {
        SceneManager.LoadScene("Main");
    }


}

