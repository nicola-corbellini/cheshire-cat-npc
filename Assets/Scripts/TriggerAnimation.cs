using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    private ChatClient _script;
    private string _sentiment;
    public Animator animator;
    
    void Start()
    {
        // Get the other script component attached to the same GameObject as this script
        _script = GetComponent<ChatClient>();
    }
    
    void Update()
    {
        // Check the sentiment label stored in the received message and use it to trigger predefined animations
        if (_script._catMessage.sentiment == "rude")
        {
            animator.SetTrigger("Damage");
            // Reset the sentiment value after triggering the animation
            _script._catMessage.sentiment = null;
        }
        else if (_script._catMessage.sentiment == "sleep")
        {
            animator.SetTrigger("SleepStart");
            _script._catMessage.sentiment = null;
        }
        else if (_script._catMessage.sentiment == "joy")
        {
            animator.SetTrigger("Jump");
            _script._catMessage.sentiment = null;
        }
    }
}
