using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public string toLoad;
	
	// Update is called once per frame
	void Update ()
	{
		OnKeyDown ();
	}

	void OnKeyDown()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel(toLoad); 
		}
	}
}
