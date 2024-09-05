using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FPSDisplay : MonoBehaviour
{

	// Variables to calculate FPS
	private int frameCount = 0;
	private float deltaTime = 0.0f;
	private float fps = 0.0f;
	private float updateInterval = 1.0f;

    public GameObject displayText;


    void Start()
	{
        StartCoroutine(UpdateFPS());
	}

	void Update()
	{
		frameCount++;
		deltaTime += Time.unscaledDeltaTime;
	}

	IEnumerator UpdateFPS()
	{
		while (true)
		{
			yield return new WaitForSeconds(updateInterval);
			fps = frameCount / deltaTime;
			frameCount = 0;
			deltaTime = 0.0f;
			UpdateDisplay();
		}
	}

	void UpdateDisplay()
	{
		string fpsString = $"FPS: {fps:F2}\n";
		string gameManagerState = GameManager.Instance.ToString();
		displayText.GetComponent<Text>().text = fpsString + gameManagerState;
    }

    public void ToggleDisplay()
    {
		bool active = displayText.active;
        displayText.SetActive(!active);
    }
}
