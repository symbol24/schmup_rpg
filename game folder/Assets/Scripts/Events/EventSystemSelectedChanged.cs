using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventSystemSelectedChanged : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject previousGameObjectSelected;
    public event EventHandler SelectedGameObjectChanged;

	// Use this for initialization
	void Start ()
	{
	    eventSystem = EventSystem.current;
	    previousGameObjectSelected = eventSystem.currentSelectedGameObject;
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (eventSystem.currentSelectedGameObject != previousGameObjectSelected)
        {
            previousGameObjectSelected = eventSystem.currentSelectedGameObject;
            if (SelectedGameObjectChanged != null) SelectedGameObjectChanged(this, EventArgs.Empty);
        }
    }
}
