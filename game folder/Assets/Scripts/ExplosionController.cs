using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ExplosionController : MonoBehaviour {
	public float lifeTimer;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTimer);
	}

}
