using System;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusInputField : MonoBehaviour
{
    public GameObject canvas, player;
    public TMP_InputField inputField;
    private int _tabPressed;
    private PlayerInput _playerInput;
    private bool _triggered;

    // Start is called before the first frame update
    void Start()
    {
        _tabPressed = 0;
        _playerInput = player.GetComponent<PlayerInput>();
        // InputSystem.EnableDevice(Keyboard.current);
    }

    private void FixedUpdate()
    {
        if (_triggered)
        {
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Tab) && _tabPressed % 2 == 0)
            {
                inputField.Select();
                _playerInput.enabled = false;
                _tabPressed++;
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && _tabPressed % 2 != 0)
            {
                _playerInput.enabled = true;
                _tabPressed++;
            }
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggered = false;
    }
}
