using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour
{

    private DialogueQueue _dialogueQueue;
    public float _graceDelayforListeningToInput = 0.01f;
    private bool _listenToInput = false;

    private bool ListenToInput
    {
        get { return _listenToInput; }
        set
        {
            if (value)
                StartCoroutine(DelayActionCoroutine(() => _listenToInput = value, _graceDelayforListeningToInput));
            else _listenToInput = value;
        }
    }
    public List<string> buttonsToFastForwardDialogue;
    // Use this for initialization
    private void Start()
    {
        _dialogueQueue = FindObjectOfType<DialogueQueue>();
        _dialogueQueue.QueueEmptied += DialogueQueueOnQueueEmptied;
        _dialogueQueue.QueueStarted += DialogueQueueOnQueueStarted;
    }

    private void DialogueQueueOnQueueStarted(object sender, EventArgs eventArgs)
    {
        ListenToInput = true;
    }

    private void DialogueQueueOnQueueEmptied(object sender, EventArgs eventArgs)
    {
        ListenToInput = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_listenToInput && buttonsToFastForwardDialogue.Any(c => Input.GetButtonDown(c)))
        {
            _dialogueQueue.FastForward();
        }
    }

    private IEnumerator DelayActionCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
