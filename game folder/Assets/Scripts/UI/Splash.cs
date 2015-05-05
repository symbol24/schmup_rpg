using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		OnKeyDown ();
	}

	void OnKeyDown()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("loader"); 
		}
	}
}
