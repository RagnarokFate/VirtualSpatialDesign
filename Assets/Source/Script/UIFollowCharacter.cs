using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCharacter : MonoBehaviour
{
    public Transform characterTransform; // Reference to the character's transform
    public RectTransform pocketPanel; // Reference to the pocket UI panel
    public RectTransform toolbarPanel; // Reference to the toolbar UI panel
    public RectTransform statsPanel; // Reference to the stats UI panel
    public RectTransform settingsPanel; // Reference to the settings UI panel
    public Vector3 pocketOffset; // Offset for the pocket panel
    public Vector3 toolbarOffset; // Offset for the toolbar panel
    public Vector3 statsOffset; // Offset for the stats panel
    public Vector3 settingsOffset; // Offset for the settings panel
    public Camera uiCamera; // Reference to the UI camera (for screen space - camera mode)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterTransform != null)
        {
            Vector3 pocketPosition = characterTransform.position + pocketOffset;
            Vector3 toolbarPosition = characterTransform.position + toolbarOffset;
            Vector3 statsPosition = characterTransform.position + statsOffset;
            Vector3 settingsPosition = characterTransform.position + settingsOffset;

            if (uiCamera != null)
            {
                pocketPanel.position = uiCamera.WorldToScreenPoint(pocketPosition);
                toolbarPanel.position = uiCamera.WorldToScreenPoint(toolbarPosition);
                statsPanel.position = uiCamera.WorldToScreenPoint(statsPosition);
                settingsPanel.position = uiCamera.WorldToScreenPoint(settingsPosition);
            }
            else
            {
                pocketPanel.position = pocketPosition;
                toolbarPanel.position = toolbarPosition;
                statsPanel.position = statsPosition;
                settingsPanel.position = settingsPosition;
            }
        }
    }

}
