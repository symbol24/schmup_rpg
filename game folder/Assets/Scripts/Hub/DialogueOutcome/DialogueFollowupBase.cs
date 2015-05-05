using UnityEngine;
using System.Collections;

public abstract class DialogueFollowupBase : MonoBehaviour {


    protected DialogueCharacter _dialogue;
	// Use this for initialization
	void Start ()
	{
        _dialogue = GetComponent<DialogueCharacter>();
        if (_dialogue == null) Debug.Log("No dialogue to follow up");
        Init();
	}

    protected abstract void Init();
	
	// Update is called once per frame
	void Update () {
	
	}
}
