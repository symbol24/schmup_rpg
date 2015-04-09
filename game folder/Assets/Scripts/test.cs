using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public float timer = 5.0f;
	public string levelname = "empty";

	// Use this for initialization
	void Start () {
		StartCoroutine(Test(timer, levelname));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Test(float timedelay, string level){
		yield return new WaitForSeconds(timedelay);
		
		Application.LoadLevel(level);
	}
}
