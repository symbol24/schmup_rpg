using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class HPSystemController : BaseBarSystemController {

	public override void Start(){
		base.Start ();
		m_maxValue = m_player.m_maxPlayerHP;
		m_currentValue = m_maxValue;
	}
}
