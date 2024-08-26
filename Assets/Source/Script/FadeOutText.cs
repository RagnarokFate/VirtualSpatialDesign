using UnityEngine;
using UnityEngine.UI;

public class FadeOutText : MonoBehaviour
{
    private Text uiText;
    private float timer;
    private float fadeDuration;
    private Color textColor;
    private string textContent;

    // Singleton instance
    private static FadeOutText instance;

    public static void Show(float duration, Color color, string content, Vector2 screenPosition, Transform canvasTransform)
    {
        // If an instance already exists, destroy it
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        // Create a new GameObject for the text
        GameObject fadeOutTextObj = new GameObject("FadeOutText");
        instance = fadeOutTextObj.AddComponent<FadeOutText>();
        instance.Initialize(duration, color, content, screenPosition, canvasTransform);
    }

    private void Initialize(float duration, Color color, string content, Vector2 screenPosition, Transform canvasTransform)
    {
        fadeDuration = duration;
        textColor = color;
        textContent = content;

        // Set the Canvas as the parent
        gameObject.transform.SetParent(canvasTransform, false);

        // Add and configure the Text component
        uiText = gameObject.AddComponent<Text>();
        uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        uiText.text = textContent;
        uiText.color = textColor;
        uiText.fontSize = 24;

        // Set line to center and horizontal overflow alignment
        uiText.alignment = TextAnchor.MiddleCenter;
        uiText.horizontalOverflow = HorizontalWrapMode.Overflow;

        // Position the text on the screen using RectTransform
        RectTransform rectTransform = uiText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = screenPosition;

        // Initialize the timer
        timer = fadeDuration;
    }

    private void Update()
    {
        if (uiText == null)
        {
            Debug.LogWarning("uiText is null. Check if Initialize method was called.");
            return;
        }

        // Update the timer
        timer -= Time.deltaTime;

        // Calculate the alpha value
        float alpha = Mathf.Clamp01(timer / fadeDuration);

        // Update the text color with the new alpha value
        uiText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

        // Destroy the GameObject once the text is fully transparent
        if (timer <= 0f)
        {
            Destroy(gameObject);
            instance = null; // Reset the instance reference
        }
    }
}
