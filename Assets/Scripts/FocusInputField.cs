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

    void Start()
    {
        // Set to zero the counter of time we pressed Tab
        _tabPressed = 0;
        
        // Get the Player Input to disable it while typing in the chat
        _playerInput = player.GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        // Check if we are inside the Gambler Cat Box Collider
        if (_triggered)
        {
            // Activate the Chat GUI
            canvas.SetActive(true);
            
            // Check if Tab is pressed and even number of times.
            // This has to be done to give focus to the Text Area and be able to type inside
            if (Input.GetKeyDown(KeyCode.Tab) && _tabPressed % 2 == 0)
            {
                // Focus on Text Area
                inputField.Select();
                
                // Disable movement system or we'll move around while typing
                
                _playerInput.enabled = false;
                // Increase the counter
                _tabPressed++;
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && _tabPressed % 2 != 0)
            {
                // If we pressed Tab an odd number of times, enable back the Player Input
                _playerInput.enabled = true;
                
                // Increase the counter
                _tabPressed++;
            }
        }
        else
        {
            // Make the chat Canvas disappear when we're not inside the Gambler Cat Box Collider
            canvas.SetActive(false);
        }
    }
    
    // Set _trigger boolean to true when entering the Gambler Cat Box Collider
    private void OnTriggerEnter(Collider other)
    {
        _triggered = true;
    }

    // Set _trigger boolean to false when exiting the Gambler Cat Box Collider
    private void OnTriggerExit(Collider other)
    {
        _triggered = false;
    }
}
