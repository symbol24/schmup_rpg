using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Thanks : MonoBehaviour 
{
	//public float delaytime = 5;

    void Update()
	{
		onkeydown();
	}
	void onkeydown()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("loader");
		}
	}
}
