using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class HPSystemController : BaseBarSystemController {

	public override void Start(){
		base.Start ();
	}

	public override void SetPlayerShip(PlayerController playerShip){
		base.SetPlayerShip (playerShip);
		
		m_maxValue = playerShip.m_maxPlayerHP;
		m_currentValue = m_maxValue;
	}
}
