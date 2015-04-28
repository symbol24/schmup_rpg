using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectMoveToHighlighted : MonoBehaviour
{
    private ScrollRect scrollRect;
    private EventSystem eventSystem;
    private EventSystemSelectedChanged eventSystemSelectedChanged;

	// Use this for initialization
	void Start ()
	{
	    eventSystem = EventSystem.current;
	    eventSystemSelectedChanged = FindObjectOfType<EventSystemSelectedChanged>();
        eventSystemSelectedChanged.SelectedGameObjectChanged += eventSystemSelectedChanged_SelectedGameObjectChanged;
	}

    void eventSystemSelectedChanged_SelectedGameObjectChanged(object sender, EventArgs e)
    {
        
    }
}
