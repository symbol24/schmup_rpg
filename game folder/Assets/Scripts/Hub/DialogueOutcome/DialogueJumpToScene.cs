using System;
using UnityEngine;
using System.Collections;

public class DialogueJumpToScene : DialogueFollowupBase
{

    public string sceneToLoad;
	// Use this for initialization
    protected override void Init()
    {
        _dialogue.Activated += DialogueOnActivated;
    }

    private void DialogueOnActivated(object s, EventArgs eventArgs)
    {
        _dialogue.Queue.QueueEmptied += (sender, args) => Application.LoadLevel(sceneToLoad);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
