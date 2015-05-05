using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueCharacter : MonoBehaviour
{
    public DialogueQueue Queue { get; private set; }
    public DialogueDataObject[] dialogues;
    public Button button;
    public event EventHandler Activated;
	// Use this for initialization
	void Start ()
	{
	    Queue = FindObjectOfType<DialogueQueue>();
        button = GetComponent<Button>();
        button.onClick.AddListener(button_OnClick);
	}

    private void button_OnClick()
    {
        Debug.Log("buttonClicked");
        foreach (var dialogue in dialogues)
        {
            Queue.Enqueue(dialogue);
        }
        if (Activated != null) Activated(this, EventArgs.Empty);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
