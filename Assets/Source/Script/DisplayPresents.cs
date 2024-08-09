using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPresents : MonoBehaviour
{
    private bool visible = false;

    public GameObject presentsView;

    void Start()
    {
        presentsView = GameObject.Find("PresentsView");
        presentsView.SetActive(visible);
    }

    public void TogglePocketButton()
    {
        visible = !visible;
        presentsView.SetActive(visible);
        if (visible)
        {
            LoadPlanePresents();
            LoadMeshPresents();
        }

    }



    private void LoadPlanePresents()
    {
        if (visible && GameManager.Instance.currentTool == Tool.draw)
        {
            // Load the presents
            Dropdown dropdown = GameObject.Find("PresentsView").GetComponent<Dropdown>();
            dropdown.options.Clear();
            dropdown.options.Add(new Dropdown.OptionData() { text = "Select a Present" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Point" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Quad" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Rectangle" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Polygon" });
        }
    }
    private void LoadMeshPresents()
    {
        if (visible && GameManager.Instance.currentTool == Tool.insert)
        {
            // Load the presents
            Dropdown dropdown = GameObject.Find("PresentsView").GetComponent<Dropdown>();
            dropdown.options.Clear();
            dropdown.options.Add(new Dropdown.OptionData() { text = "Select a Present" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Cube" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Sphere" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Cylinder" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Pipe" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Plane" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Torus" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Stair" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Arch" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Cone" });
            dropdown.options.Add(new Dropdown.OptionData() { text = "Sprite" });

        }
    }
}
