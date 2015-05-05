using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventSystemSelectedChanged : MonoBehaviour
{
    private EventSystem eventSystem;
    public GameObject PreviousGameObjectSelected { get; private set; }
    public event EventHandler SelectedGameObjectChanged;

	// Use this for initialization
	void Start ()
	{
	    eventSystem = EventSystem.current;
	    PreviousGameObjectSelected = eventSystem.currentSelectedGameObject;
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (eventSystem.currentSelectedGameObject != PreviousGameObjectSelected)
        {
            PreviousGameObjectSelected = eventSystem.currentSelectedGameObject;
            if (SelectedGameObjectChanged != null) SelectedGameObjectChanged(this, EventArgs.Empty);
        }
    }
}
