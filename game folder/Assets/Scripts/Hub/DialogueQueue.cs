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
    public IEnumerable<DialogueContainer> showText;


    // Use this for initialization
    private void Start()
    {
        //if (showText == null) showText = GetComponent<DialogueContainer>();
        //if (showText == null) showText = FindObjectOfType<DialogueContainer>();
        if (showText == null) Debug.LogError("NoShowText component in " + gameObject.name);
        //showText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Enqueue(string text)
    {
        Queue.Enqueue(new DialogueDataObject{Text = text});

    }

    public void Enqueue(DialogueDataObject text)
    {
        Queue.Enqueue(text);
        if (Queue.Count == 1)
        {
            CheckForQueue();
        }
    }
    private void CheckForQueue()
{
        if (Queue.Any())
        {
            StartCoroutine(StartWritingText(Queue.Dequeue()));
        }
    }

    private IEnumerator StartWritingText(DialogueDataObject dialogueDataObject)
    {/*
        showText.gameObject.SetActive(true);
        string currentActiveString = string.Empty;
        showText.Title = dialogueDataObject.Title;
        var toWrite = dialogueDataObject.Text;
        while (currentActiveString.Length < toWrite.Length)
        {
            currentActiveString = toWrite.Substring(0, currentActiveString.Length + 1);
            showText.TextToShow = currentActiveString;
            yield return new WaitForSeconds(intervalPerLetter);
        }
        yield return new WaitForSeconds(intervalToWaitWhenCompleted);
        showText.gameObject.SetActive(false);
        showText.TextToShow = string.Empty;
        CheckForQueue();*/
        yield return null;
    }

}
