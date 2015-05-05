using UnityEngine;
using System.Collections;

public class ScrollRectButtonScript : MonoBehaviour
{

    public int position;
    private ScrollRectMoveToHighlighted container;
	// Use this for initialization
	void Start ()
	{
	    container = GetComponentInParent<ScrollRectMoveToHighlighted>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
