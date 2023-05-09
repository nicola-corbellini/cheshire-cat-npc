using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEditor.PackageManager;


public class ChatClient : MonoBehaviour
{
    public TMP_InputField playerChat, catChat;
    private WebSocket _ws;

    // Define a custom class exposing a public property called "text".
    // This is necessary as the JsonUtility.ToJson method only converts public properties
    private class Message {
        public string text;
    }
    
    public class CatMessage
    {
        public string content;
        public string sentiment;
    }

    public CatMessage _catMessage;

    private void Awake()
    {
        _catMessage = new CatMessage();
    }

    IEnumerator Start()
    {
        // Websocket url
        _ws = new WebSocket("ws://localhost:1865/ws");
        
        // Log status of connection in the console
        Debug.Log("Initial State : " + _ws.ReadyState);
        
        // Connect to Websocket
        _ws.Connect();
        
        // Listener to Websocket
        _ws.OnMessage += (sender, e) =>
        {
            // Log received data in the console
            //Debug.Log($"Received {e.Data} from " + ((WebSocket)sender).Url + "");  

            // Custom function to parse the received data
            _catMessage = ParseOutput(e.Data);
            Debug.Log(_catMessage.sentiment);
        };
        yield return 0;
    }

    private void Update()
    {
        // Detect when Enter/Return is pressed
        if(Input.GetKeyDown(KeyCode.Return))
        {
            // New Message instance assigning the typed text to the "text" key
            var data = new Message { text = playerChat.text };
            
            // Convert Message instance to JSON
            var json = JsonUtility.ToJson(data);
            
            // Send JSON to Websocket
            _ws.Send(json);
        }
        
        // Output the answer in the Input Field object in Unity
        catChat.text = _catMessage.content;
    }
    
    /// <summary>
    /// Since the received text is a string representation of a JSON with a known structure,
    /// we can split the string between two keys of interest.
    /// </summary>
    private CatMessage ParseOutput(string message)
    {
        _catMessage = JsonUtility.FromJson<CatMessage>(message);

        return _catMessage;
    }
}