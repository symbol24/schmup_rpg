using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public bool isSingleChecked = true;
    private ButtonContainer[] availableChecks;
    public float timeToFade = 2f;
    public float timeBeforePicFadeout = 2f;

	// Use this for initialization
	void Start ()
	{
        var disorganizedToggles = FindObjectsOfType<ButtonContainer>();
	    availableChecks = disorganizedToggles.OrderBy(c => c.MainToggle.transform.position.x).ToArray();
	    foreach (var availableCheck in availableChecks)
	    {
	        availableCheck.MainToggle.isOn = false;
            availableCheck.ToggleChanged += AvailableCheckOnToggleChanged;
	    }
        
	}

    private void AvailableCheckOnToggleChanged(object sender, EventArgs eventArgs)
    {
        var containerReceived = sender as ButtonContainer;
        if (containerReceived != null)
        {
            var currentButtonInstanceID = containerReceived.GetInstanceID();
            if (containerReceived.MainToggle.isOn)
            {
                StartCoroutine(CoroutineDelayed(() => containerReceived.StartFadeIn(timeToFade), timeBeforePicFadeout));
                foreach (var buttonContainer in availableChecks)
                {
                    if (currentButtonInstanceID != buttonContainer.GetInstanceID())
                    {
                        buttonContainer.MainToggle.isOn = false;
                    }
                }
            }
            else
            {
                containerReceived.StartFadeout(timeToFade);
            }
        }
    }

    // Update is called once per frame
	void Update () {
	
	}

    private IEnumerator CoroutineDelayed(Action toDo, float time)
    {
        yield return new WaitForSeconds(time);
        toDo.Invoke();
    }
}
