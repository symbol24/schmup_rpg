﻿using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ProjectileBehavior : MonoBehaviour {
	protected ProjectileController m_Controller;
	
	public void Init (ProjectileController controller) {
		m_Controller = controller;
	}

	public virtual void Start(){
		transform.position = m_Controller.transform.position;
		
	}
	
	public virtual void Update(){
		
	}
	
	public virtual void UpdateBehavior(){

		}
}
