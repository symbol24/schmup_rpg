using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class DialogueQueue : MonoBehaviour
{
    private Queue<DialogueDataObject> _queue = new Queue<DialogueDataObject>();
    private Queue<DialogueDataObject> Queue
    {
        get { return _queue; }
        set { _queue = value; }
    }

    public float intervalToCheckQueue = 0.5f;
    public float intervalPerLetter = 0.05f;
    public float intervalToWaitWhenCompleted = 3f;
    public IEnumerable<DialogueUIContainer> availableDialogues;
    public event EventHandler QueueEmptied;
    public event EventHandler QueueStarted;


    // Use this for initialization
    private void Start()
    {
        availableDialogues = FindObjectsOfType<DialogueUIContainer>();
        if (availableDialogues == null) Debug.LogError("NoShowText component in " + gameObject.name);
    }


    public void FastForward()
    {
        if (_isWritingText) _isWritingText = false;
        else if (_isWaitingForNextDialog) _isWaitingForNextDialog = false;
        else Debug.Log("Attempting to Fastforward and nothing to fast forward found");
    }
    public void Enqueue(string text)
    {
        var toPass = (DialogueDataObject)previousDataObject.Clone();
        toPass.Text = text;
        Queue.Enqueue(toPass);
    }
    public void Enqueue(DialogueDataObject text)
    {
        previousDataObject = text;
        Queue.Enqueue(text);
        if (Queue.Count == 1)
        {
            if (QueueStarted != null) QueueStarted(this, EventArgs.Empty);
            StartCoroutine(CheckForQueue());
        }
    }
    #region HandlingQueueForDialogs
    private DialogueDataObject previousDataObject;
    private IEnumerator CheckForQueue()
    {
        yield return new WaitForEndOfFrame();
        if (Queue.Any())
        {
            StartCoroutine(StartWritingText(Queue.Dequeue()));
        }
        else
        {
            if (QueueEmptied != null) QueueEmptied(this, EventArgs.Empty);
        }
    }
    private DialogueUIContainer _previousActiveContainer;
    private bool _isWritingText = false;
    private bool _isWaitingForNextDialog = false;
    private IEnumerator StartWritingText(DialogueDataObject dialogueDataObject)
    {
        var container = FindDialogueContainer(dialogueDataObject);
        if (_previousActiveContainer == null)
        {
            _previousActiveContainer = container;
            container.SetEverythingActive();
        }
        else if (_previousActiveContainer != container)
        {
            _previousActiveContainer.SetEverythingInactive();
            _previousActiveContainer = container;
            container.SetEverythingActive();
        }
        string currentActiveString = string.Empty;
        container.Title = dialogueDataObject.Title;
        var toWrite = dialogueDataObject.Text;
        _isWritingText = true;
        while (currentActiveString.Length < toWrite.Length && _isWritingText)
        {
            currentActiveString = toWrite.Substring(0, currentActiveString.Length + 1);
            container.TextToShow = currentActiveString;
            yield return new WaitForSeconds(intervalPerLetter);
        }
        container.TextToShow = dialogueDataObject.Text;
        _isWritingText = false;
        _isWaitingForNextDialog = true;
        float currentTime = 0f;
        while (currentTime < intervalToWaitWhenCompleted && _isWaitingForNextDialog)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        if (!Queue.Any())
        {
            container.SetEverythingInactive();
            container.TextToShow = string.Empty;
            _previousActiveContainer = null;
        }
        StartCoroutine(CheckForQueue());
    }
    private DialogueUIContainer FindDialogueContainer(DialogueDataObject dataObject)
    {
        var ret = availableDialogues.First(c => c.Identifier == dataObject.Character);
        return ret;
    }
    #endregion HandlingQueueForDialogs
}
