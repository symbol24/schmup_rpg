using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    public Text textFirstLine;
    public Text textSecondLine;
    public string Title { get; set; }
    public string TextToShow { get; set; }
    public CharacterIdentifier _identifier;
    public CharacterIdentifier Identifier { get { return _identifier; } }

    private void OnGUI()
    {
        textFirstLine.text = Title;
        textSecondLine.text = TextToShow;
    }

    // Use this for initialization
	void Start ()
	{
	    Title = string.Empty;
	    TextToShow = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
