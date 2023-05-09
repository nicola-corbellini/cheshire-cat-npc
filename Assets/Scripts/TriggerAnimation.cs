using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerAnimation : MonoBehaviour
{
    private ChatClient _script;

    private string _sentiment;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        _script = GetComponent<ChatClient>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_script._catMessage.sentiment == "rude")
        {
            animator.SetTrigger("Damage");
            _script._catMessage.sentiment = null;
        }
        else if (_script._catMessage.sentiment == "sleep")
        {
            animator.SetTrigger("SleepStart");
            _script._catMessage.sentiment = null;
        }
        else if (_script._catMessage.sentiment == "magic")
        {
            animator.SetTrigger("Spell");
            _script._catMessage.sentiment = null;
        }
        else if (_script._catMessage.sentiment == "joy")
        {
            animator.SetTrigger("Fly");
            _script._catMessage.sentiment = null;
        }
    }
}
