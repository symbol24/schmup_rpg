﻿using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class PlanetController : MonoBehaviour {
	private float m_Speed = 0.2f;
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * m_Speed * Time.deltaTime, Space.Self);
	}
}
