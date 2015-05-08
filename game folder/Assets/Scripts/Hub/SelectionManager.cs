using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public bool isSingleChecked = true;
    private ButtonContainer[] availableChecks;
    public float timeToFade = 2f;
    public float timeBeforePicFadeout = 2f;
    private ButtonContainer currentActiveButton;
    private DialogueQueue _dialogueQueue;

	// Use this for initialization
	void Start ()
	{
        var disorganizedToggles = FindObjectsOfType<ButtonContainer>();
	    availableChecks = disorganizedToggles.OrderBy(c => c.MainButton.transform.position.x).ToArray();
	    foreach (var availableCheck in availableChecks)
	    {
            availableCheck.ButtonPressed += availableCheck_ButtonPressed;
	    }
        availableChecks.First().OnClick();
	    EventSystem.current.SetSelectedGameObject(availableChecks.First().gameObject);
	    _dialogueQueue = FindObjectOfType<DialogueQueue>();
        _dialogueQueue.QueueEmptied += DialogueQueueOnQueueEmptied;
	    _dialogueQueue.QueueStarted += DialogueQueueOnQueueStarted;
	}

    private void DialogueQueueOnQueueStarted(object sender, EventArgs eventArgs)
    {
        currentActiveButton.ButtonsInteractible = false;
    }
    private void DialogueQueueOnQueueEmptied(object sender, EventArgs eventArgs)
    {
        currentActiveButton.ButtonsInteractible = true;
    }

    void Update()
    {
        
    }


    #region HandlingTransitions
    private bool _inButtonPressProgress = false;
    void availableCheck_ButtonPressed(object sender, EventArgs e)
    {
        if (_inButtonPressProgress) return;
        _inButtonPressProgress = true;
        var button = (ButtonContainer) sender;
        var currentButtonInstanceID = button.GetInstanceID();
        if (currentActiveButton != null && currentButtonInstanceID == currentActiveButton.GetInstanceID())
        {
            _inButtonPressProgress = false;
            return;
        }
        if (currentActiveButton != null)
        {
            currentActiveButton.StartFadeout(timeToFade);
            currentActiveButton.FinishedFadeOut += currentActiveButton_FinishedFadeOut;
        }
        else
        {
            button.StartFadeIn(timeToFade);
            _inButtonPressProgress = false;
        }
        currentActiveButton = button;
    }
    void currentActiveButton_FinishedFadeOut(object sender, EventArgs e)
    {
        var button = (ButtonContainer) sender;
        button.FinishedFadeOut -= currentActiveButton_FinishedFadeOut;
        currentActiveButton.StartFadeIn(timeToFade);
        currentActiveButton.FinishedFadeIn += currentActiveButton_FinishedFadeIn;
    }
    void currentActiveButton_FinishedFadeIn(object sender, EventArgs e)
    {
        _inButtonPressProgress = false;
    }
    #endregion HandlingTransitions
}
