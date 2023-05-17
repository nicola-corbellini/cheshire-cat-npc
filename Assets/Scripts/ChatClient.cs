using System.Collections;
using TMPro;
using UnityEngine;
using WebSocketSharp;


public class ChatClient : MonoBehaviour
{
    public TMP_InputField playerChat, catChat;
    private WebSocket _ws;

    // Define a custom class exposing a public property called "text".
    // This is necessary as the JsonUtility.ToJson method only converts public properties
    // and the Cat server expects the message to be formatted as a value of the "text" key.
    private class Message {
        public string text;
    }
    
    // Define a custom class that we'll use to store the Cat's answer received from the server.
    // The incoming JSON has many keys we are not interested in, so we just define two properties:
    // the "content" with the text answer and the "sentiment" with the detected label. 
    public class CatMessage
    {
        public string content;
        public string sentiment;
    }

    public CatMessage _catMessage;

    private void Awake()
    {
        // Instantiate the CatMessage class as soon as the GameObject this script is attached to is instantiated.
        _catMessage = new CatMessage();
    }
    
    // Coroutine to manage the WebSocket connection
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
            // Custom function to parse the received data
            _catMessage = ParseOutput(e.Data);
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
    

    private CatMessage ParseOutput(string message)
    {
        // Here is a custom function to parse the received text from the server.
        // We parse it as a JSON structered text and preserve only the two properties of CatMessage we defined above 
        _catMessage = JsonUtility.FromJson<CatMessage>(message);

        return _catMessage;
    }
}