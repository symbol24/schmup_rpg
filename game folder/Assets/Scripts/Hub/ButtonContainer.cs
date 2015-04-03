using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour 
{
    public Toggle MainToggle { get; private set; }
    public event EventHandler ToggleChanged;
    public Graphic[] _uiElementsToCarryAlong;
    public Graphic[] UIElements { get { return _uiElementsToCarryAlong; } }

	// Use this for initialization
	void Awake ()
	{
	    MainToggle = GetComponentInChildren<Toggle>();
        MainToggle.onValueChanged.AddListener(MainToggle_OnValueChanged);
	    previousToggleValue = MainToggle.isOn;
        if(previousToggleValue) StartFadeout(0);
        else StartFadeIn(0);
        if(_uiElementsToCarryAlong == null) _uiElementsToCarryAlong = new Graphic[0];

	}

    private bool previousToggleValue;
    private void MainToggle_OnValueChanged(bool changedTo)
    {
        if (previousToggleValue != changedTo)
        {
            previousToggleValue = changedTo;
            if (ToggleChanged != null) ToggleChanged(this, EventArgs.Empty);
        }
    }


    private Stack<IEnumerator> _fadeinCoroutines = new Stack<IEnumerator>();
    public void StartFadeIn(float time)
    {
        if (_fadeoutCoroutines.Any())
        {
            StopCoroutines(_fadeoutCoroutines);
            _fadeoutCoroutines.Clear();
        }
        foreach (var uiElement in UIElements)
        {
            _fadeinCoroutines.Push(uiElement.FadeIn(time));
        }
        StartCoroutines(_fadeinCoroutines);
    }
    private Stack<IEnumerator> _fadeoutCoroutines = new Stack<IEnumerator>();
    public void StartFadeout(float time)
    {
        if (_fadeinCoroutines.Any())
        {
            StopCoroutines(_fadeinCoroutines);
            _fadeinCoroutines.Clear();
        }
        foreach (var uiElement in UIElements)
        {
            _fadeoutCoroutines.Push(uiElement.FadeOut(time));
        }
        StartCoroutines(_fadeoutCoroutines);
    }

    private void StopCoroutines(IEnumerable<IEnumerator> enumerators)
    {
        foreach (var enumerator in enumerators)
        {
            StopCoroutine(enumerator);
        }
    }
    private void StartCoroutines(IEnumerable<IEnumerator> coroutines)
    {
        foreach (var enumerator in coroutines)
        {
            StartCoroutine(enumerator);
        }
    }
}
