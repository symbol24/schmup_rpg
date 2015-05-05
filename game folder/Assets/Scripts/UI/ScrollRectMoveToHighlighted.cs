using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectMoveToHighlighted : MonoBehaviour
{
    private Image imageMask;
    private ScrollRect scrollRect;
    private EventSystem eventSystem;
    private EventSystemSelectedChanged eventSystemSelectedChanged;
    public GameObject AreaToScroll;
    private RectTransform rectTransformAreaToScroll;
    private List<Button> buttonsInArea;
    private float steps;

	// Use this for initialization
	void Start ()
	{
	    eventSystem = EventSystem.current;
	    eventSystemSelectedChanged = FindObjectOfType<EventSystemSelectedChanged>();
        eventSystemSelectedChanged.SelectedGameObjectChanged += eventSystemSelectedChanged_SelectedGameObjectChanged;
	    imageMask = GetComponent<Image>();
	    rectTransformAreaToScroll = AreaToScroll.GetComponent<RectTransform>();
	    buttonsInArea = AreaToScroll.GetComponentsInChildren<Button>().OrderByDescending(c => c.transform.position.y).ToList();
	    steps = rectTransformAreaToScroll.rect.height/(float) buttonsInArea.Count();
	}

    void eventSystemSelectedChanged_SelectedGameObjectChanged(object sender, EventArgs e)
    {
        if (buttonsInArea.Any(c => c.gameObject == eventSystem.currentSelectedGameObject))
        {
            Debug.Log("SelectedInArea");
            var button = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            var currentIndex = buttonsInArea.IndexOf(button);
            rectTransformAreaToScroll.anchoredPosition = new Vector2(
                rectTransformAreaToScroll.anchoredPosition.x,
                steps * currentIndex);

        }
        else if (buttonsInArea.Any(c => c.gameObject == eventSystemSelectedChanged.PreviousGameObjectSelected))
        {
            
        }
    }
}
