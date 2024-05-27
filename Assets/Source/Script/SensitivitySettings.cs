using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivitySettings : MonoBehaviour
{
    [SerializeField] private float _rotationSensitivity = 1.0f; // Default rotation sensitivity
    [SerializeField] private float _movementSensitivity = 1.0f; // Default movement sensitivity

    public static SensitivitySettings Instance { get; private set; } // Singleton access

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicate instances
        }
    }

    public float RotationSensitivity
    {
        get => _rotationSensitivity;
        set
        {
            _rotationSensitivity = Mathf.Clamp(value, 0.1f, 10.0f); // Set reasonable limits
        }
    }

    public float MovementSensitivity
    {
        get => _movementSensitivity;
        set
        {
            _movementSensitivity = Mathf.Clamp(value, 0.1f, 10.0f); // Set reasonable limits
        }
    }

    // Access and modify sensitivity in VR scripts (example):
    public void UpdateRotationSensitivity(float delta)
    {
        RotationSensitivity += delta;
    }

    public void UpdateMovementSensitivity(float delta)
    {
        MovementSensitivity += delta;
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
