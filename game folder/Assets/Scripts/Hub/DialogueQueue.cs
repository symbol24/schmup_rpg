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


    // Use this for initialization
    private void Start()
    {
        //if (showText == null) showText = GetComponent<DialogueContainer>();
        //if (showText == null) showText = FindObjectOfType<DialogueContainer>();
        availableDialogues = FindObjectsOfType<DialogueUIContainer>();
        if (availableDialogues == null) Debug.LogError("NoShowText component in " + gameObject.name);
        //showText.gameObject.SetActive(false);
    }

    public void Enqueue(string text)
    {
        var toPass = (DialogueDataObject)previousDataObject.Clone();
        toPass.Text = text;
        Queue.Enqueue(toPass);

    }

    private DialogueDataObject previousDataObject;
    public void Enqueue(DialogueDataObject text)
    {
        previousDataObject = text;
        Queue.Enqueue(text);
        if (Queue.Count == 1)
        {
            StartCoroutine(CheckForQueue());
        }
    }

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
        while (currentActiveString.Length < toWrite.Length)
        {
            currentActiveString = toWrite.Substring(0, currentActiveString.Length + 1);
            container.TextToShow = currentActiveString;
            yield return new WaitForSeconds(intervalPerLetter);
        }
        yield return new WaitForSeconds(intervalToWaitWhenCompleted);
        if (Queue.Any())
        {
            StartCoroutine(CheckForQueue());
        }
        else
        {
            container.SetEverythingInactive();
            container.TextToShow = string.Empty;
            _previousActiveContainer = null;
        }
    }

    private DialogueUIContainer FindDialogueContainer(DialogueDataObject dataObject)
    {
        var ret = availableDialogues.First(c => c.Identifier == dataObject.Character);
        return ret;
    }

}
