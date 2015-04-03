using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueUIContainer : MonoBehaviour
{
    public Graphic[] _graphicsToCarry;
    public Text textFirstLine;
    public Text textSecondLine;
    public Graphic[] GraphicsToCarry { get { return _graphicsToCarry; } }
    private string _title;
    public string Title
    {
        get { return textFirstLine.text; }
        set { textFirstLine.text = value; }
    }

    public string TextToShow
    {
        get { return textSecondLine.text; }
        set { textSecondLine.text = value; }
    }
    public CharacterIdentifier _identifier;
    public CharacterIdentifier Identifier { get { return _identifier; } }

    // Use this for initialization
	void Awake ()
	{
	    Title = string.Empty;
	    TextToShow = string.Empty;
        SetEverythingInactive();
	}

    public void SetEverythingActive()
    {
        foreach (var graphic in GraphicsToCarry)
        {
            graphic.enabled = true;
        }
        textFirstLine.enabled = true;
        textSecondLine.enabled = true;
    }

    public void SetEverythingInactive()
    {
        foreach (var graphic in GraphicsToCarry)
        {
            graphic.enabled = false;
        }
        textFirstLine.enabled = false;
        textSecondLine.enabled = false;
    }
}
