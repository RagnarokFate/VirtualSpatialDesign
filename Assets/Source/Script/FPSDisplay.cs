using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	// Text component to display the information
	public Text displayText;

	// Variables to calculate FPS
	private int frameCount = 0;
	private float deltaTime = 0.0f;
	private float fps = 0.0f;
	private float updateInterval = 1.0f;

	void Start()
	{
		try
		{
			displayText = GameObject.Find("Label").GetComponent<Text>();
		}
        catch
        {
            Debug.LogError("FPSDisplay: Can't find the Text component.");
        }

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
		displayText.text = fpsString + gameManagerState;
	}
}