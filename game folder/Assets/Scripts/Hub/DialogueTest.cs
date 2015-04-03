using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    public Button button;
    private DialogueQueue dialogQueue;

    public CharacterIdentifier whosTalking1st = CharacterIdentifier.Bartender;
    public string textToSend1st = "Testing if queue is working properly";
    public CharacterIdentifier whosTalking2nd = CharacterIdentifier.Bartender;
    public string textToSend2nd = "Testing if queue keeps working";
    public CharacterIdentifier whosTalking3rd = CharacterIdentifier.Bartender;
    public string textToSend3rd = "It should be working if this disapears";

	// Use this for initialization
	void Start ()
	{
	    dialogQueue = FindObjectOfType<DialogueQueue>();
	    button = GetComponent<Button>();
        button.onClick.AddListener(button_OnClick);
	}

    private void button_OnClick()
    {
        dialogQueue.Enqueue(new DialogueDataObject()
        {
            Character = whosTalking1st,
            Text = textToSend1st,
        });
        dialogQueue.Enqueue(new DialogueDataObject()
        {
            Character = whosTalking2nd,
            Text = textToSend2nd,
        });
        dialogQueue.Enqueue(new DialogueDataObject()
        {
            Character = whosTalking3rd,
            Text = textToSend3rd,
        });
    }

    // Update is called once per frame
	void Update () {
	
	}
}
