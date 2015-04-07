using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class EnergySystemController : BaseBarSystemController {
	public override void Start(){
		base.Start ();
		m_maxValue = m_player.m_maxPlayerEnergy;
		m_currentValue = m_maxValue;
	}

	public void ChangeEnergyTotal(string operation, int value){
		if(operation=="add"){
			if(m_currentValue + value <= m_maxValue) m_currentValue += value;
			else m_currentValue = m_maxValue;
		}else if(operation=="substract"){
			if(m_currentValue - value >= 0) m_currentValue -= value;
			else m_currentValue = 0;
		}
	}
}
