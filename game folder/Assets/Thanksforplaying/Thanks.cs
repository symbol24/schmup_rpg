using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Thanks : MonoBehaviour 
{
    public string toLoad = "Splash";

    void Start()
    {
        PlayerContainer.instance.ClearPlayer();
    }

    void Update()
	{
		onkeydown();
	}
	void onkeydown()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(toLoad);
		}
	}
}
