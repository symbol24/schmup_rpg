using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour 
{
    public Button MainButton { get; private set; }
    public event EventHandler ButtonPressed;
    public event EventHandler FinishedFadeIn;
    public event EventHandler FinishedFadeOut;
    public Graphic[] _uiElementsToCarryAlong;
    public Graphic[] UIElements { get { return _uiElementsToCarryAlong; } }
    public Stack<Button> PossibleSpriteButtons;

    private bool _buttonsInteractible = true;
    public bool ButtonsInteractible
    {
        get { return _buttonsInteractible; }
        set
        {
            if (_buttonsInteractible != value)
            {
                _buttonsInteractible = value;
                MainButton.interactable = _buttonsInteractible;
                foreach (var possibleSpriteButton in PossibleSpriteButtons)
                {
                    possibleSpriteButton.interactable = _buttonsInteractible;
                }
            }
        }
    }

	// Use this for initialization
	void Awake ()
	{
	    MainButton = GetComponentInChildren<Button>();
        MainButton.onClick.AddListener(OnClick);
        if(_uiElementsToCarryAlong == null) _uiElementsToCarryAlong = new Graphic[0];
        PossibleSpriteButtons = new Stack<Button>();
	    foreach (var uiElement in UIElements)
	    {
	        var button = uiElement.GetComponent<Button>();
            if(button != null) PossibleSpriteButtons.Push(button);
	    }
	    HideAllUIElementsEverything();

	}

    public void OnClick()
    {
        if (ButtonsInteractible)
        {
            if (ButtonPressed != null) ButtonPressed(this, EventArgs.Empty);
        }
    }



    #region HandlingTransitions
    private bool previousToggleValue = false;
    private Stack<IEnumerator> _fadeinCoroutines = new Stack<IEnumerator>();
    public void StartFadeIn(float time)
    {
        foreach (var possibleSpriteButton in PossibleSpriteButtons)
        {
            possibleSpriteButton.interactable = true;
        }
        if (_fadeoutCoroutines.Any())
        {
            StopCoroutines(_fadeoutCoroutines);
            _fadeoutCoroutines.Clear();
        }
        foreach (var uiElement in UIElements)
        {
            //_fadeinCoroutines.Push(FadeIn(uiElement,time));
            print(uiElement.name);
            ContinueFadeinInChildren(time, uiElement);
        }
        StartCoroutines(_fadeinCoroutines);
    }
    private void ContinueFadeinInChildren(float time, Graphic uiElement)
    {
        if (uiElement.gameObject.activeInHierarchy && uiElement.gameObject.activeSelf)
        {
            var uiElements = uiElement.gameObject.GetComponentsInChildren<Graphic>(false);
            foreach (var item in uiElements.Where(graph => graph != null))
            {
                //print(item.name);
                if (item.gameObject.activeInHierarchy)
                {
                    _fadeinCoroutines.Push(FadeIn(item, time));
                }
            }
        }
    }
    private Stack<IEnumerator> _fadeoutCoroutines = new Stack<IEnumerator>();
    public void StartFadeout(float time)
    {
        foreach (var possibleSpriteButton in PossibleSpriteButtons)
        {
            possibleSpriteButton.interactable = false;
        }

        if (_fadeinCoroutines.Any())
        {
            StopCoroutines(_fadeinCoroutines);
            _fadeinCoroutines.Clear();
        }
        foreach (var uiElement in UIElements)
        {
            /*var children = uiElement.GetComponentsInChildren<Graphic>();
            foreach (Graphic child in children)
            {
                
            }*/
            ContinueFadeoutInChildren(time, uiElement);
            //_fadeoutCoroutines.Push(FadeOut(uiElement,time));
        }
        StartCoroutines(_fadeoutCoroutines);
    }

    private void ContinueFadeoutInChildren(float time, Graphic uiElement)
    {
        foreach (var item in uiElement.GetComponentsInChildren<Graphic>(false).Where(graph => graph != null))
        {
            if (item.gameObject.activeInHierarchy)
            {
                _fadeoutCoroutines.Push(FadeOut(item, time));
            }
        }
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

    public IEnumerator FadeIn(Graphic gUIText, float fadeTimer)
    {
        float speed = 1.0f / fadeTimer;
        for (float t = 0.0f; t < 1.0; t += Time.deltaTime * speed)
        {
            float a = Mathf.Lerp(0.0f, 1.0f, t);
            Color faded = gUIText.color;
            faded.a = a;
            gUIText.color = faded;
            yield return 0;
        }
        if (FinishedFadeIn != null) FinishedFadeIn(this, EventArgs.Empty);
    }
    public IEnumerator FadeOut(Graphic gUIText, float fadeTimer)
    {
        float speed = 1.0f / fadeTimer;
        for (float t = 0.0f; t < 1.0; t += Time.deltaTime * speed)
        {
            float a = Mathf.Lerp(1.0f, 0.0f, t);
            Color faded = gUIText.color;
            faded.a = a;
            gUIText.color = faded;
            yield return 0;
        }
        if (FinishedFadeOut != null) FinishedFadeOut(this, EventArgs.Empty);
    }

    private void HideAllUIElementsEverything()
    {
        foreach (var uiElement in UIElements)
        {
            uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, 0);
        }
    }
    #endregion HandlingTransitions
}
