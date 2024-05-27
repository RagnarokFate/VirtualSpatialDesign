using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FramePer : MonoBehaviour
{
    public int avgFrameRate;
    public TextMeshPro display_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = $"{avgFrameRate} FPS";
    }
}
