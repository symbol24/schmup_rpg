using System;
using UnityEngine;
using System.Collections;

public class DialogueShopMenu : DialogueFollowupBase
{
    public ShopMenu shopMenu;
    // Use this for initialization
    protected override void Init()
    {
        _dialogue.Activated += DialogueOnActivated;
    }

    private void DialogueOnActivated(object sender, EventArgs eventArgs)
    {
        _dialogue.Queue.QueueEmptied += Queue_QueueEmptied;
    }

    void Queue_QueueEmptied(object sender, EventArgs e)
    {
        shopMenu.gameObject.SetActive(true);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
