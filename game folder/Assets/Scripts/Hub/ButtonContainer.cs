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
    public Image[] _imagesRelated;
    public Image[] ImagesRelated { get { return _imagesRelated; } }
    public Text[] _textsRelated;
    public Text[] TextsRelated { get { return _textsRelated; } }
    public event EventHandler ToggleChanged;

	// Use this for initialization
	void Awake ()
	{
	    MainToggle = GetComponentInChildren<Toggle>();
        MainToggle.isOn = false;
	    if (_imagesRelated == null) _imagesRelated = new Image[0];
	    if (_textsRelated == null) _textsRelated = new Text[0];
	    foreach (var image in ImagesRelated)
	    {
	        StartCoroutine(image.FadeIn(0));
	    }

	}
	
    private bool previousState = false;
	// Update is called once per frame
	void Update () {
	    if (previousState != MainToggle.isOn)
	    {
	        previousState = MainToggle.isOn;
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
        foreach (var image in ImagesRelated) _fadeinCoroutines.Push(image.FadeIn(time));
        foreach (var text in TextsRelated) _fadeinCoroutines.Push(text.FadeIn(time));
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
        foreach (var image in ImagesRelated)
        {
            _fadeoutCoroutines.Push(image.FadeOut(time));
        }
        foreach (var text in TextsRelated)
        {
            _fadeoutCoroutines.Push(text.FadeOut(time));
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
