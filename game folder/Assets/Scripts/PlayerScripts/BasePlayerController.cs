using UnityEngine;
using System.Collections;

public class BasePlayerController : MonoBehaviour {
	//the base values compiled
	public float[] m_playerBaseStatValues = new float[7];
	
	//the modifiers compiled
	public float[] m_playerStatModifiers = new float[7];
	
	//the caalculated info
	public float m_playerDamage = 1.0f;
	public float m_playerFireRate = 1.0f;
	public float m_maxPlayerHP = 1.0f;
	public float m_playerArmor = 0.0f;
	public float m_playerMouvementSpeed = 5.0f;
	public float m_maxPlayerEnergy = 10.0f;
	public float m_maxPlayerShield = 10.0f;

	//credits and level stuff
	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;

	public void UpdateValuesModifiers(EquipmentController thisEquipment){
		for(int i = 0; i < m_playerBaseStatValues.Length; i++){
			m_playerBaseStatValues[i] += thisEquipment.m_baseValues[i];
			m_playerStatModifiers[i] += thisEquipment.m_ValueModifiers[i];
		}
	}

	public void CalculateStats(){
		m_playerDamage = m_playerBaseStatValues[0] + (m_playerBaseStatValues[0] * m_playerStatModifiers[0]);
		m_playerFireRate =  m_playerBaseStatValues[1] + (m_playerBaseStatValues[1] * m_playerStatModifiers[1]);
		m_maxPlayerHP =  m_playerBaseStatValues[2] + (m_playerBaseStatValues[2] * m_playerStatModifiers[2]);
		m_playerArmor =  m_playerBaseStatValues[3] + (m_playerBaseStatValues[3] * m_playerStatModifiers[3]);
		m_playerMouvementSpeed =  m_playerBaseStatValues[4] + (m_playerBaseStatValues[4] * m_playerStatModifiers[4]);
		m_maxPlayerEnergy =  m_playerBaseStatValues[5] + (m_playerBaseStatValues[5] * m_playerStatModifiers[5]);
		m_maxPlayerShield =  m_playerBaseStatValues[6] + (m_playerBaseStatValues[6] * m_playerStatModifiers[6]);

		print ("m_playerDamage: " + m_playerDamage);
		print ("m_playerFireRate: " + m_playerFireRate);
		print ("m_maxPlayerHP: " + m_maxPlayerHP);
		print ("m_playerArmor: " + m_playerArmor);
		print ("m_playerMouvementSpeed: " + m_playerMouvementSpeed);
		print ("m_maxPlayerEnergy: " + m_maxPlayerEnergy);
		print ("m_maxPlayerShield: " + m_maxPlayerShield);
	}

	public void WeaponSwitchUpdateValuesModifiers(EquipmentController oldWeapon, EquipmentController newWeapon){
		for(int i = 0; i < m_playerBaseStatValues.Length; i++){
			m_playerBaseStatValues[i] -= oldWeapon.m_baseValues[i];
			m_playerStatModifiers[i] -= oldWeapon.m_ValueModifiers[i];
		}

		for(int i = 0; i < m_playerBaseStatValues.Length; i++){
			m_playerBaseStatValues[i] += newWeapon.m_baseValues[i];
			m_playerStatModifiers[i] += newWeapon.m_ValueModifiers[i];
		}

		CalculateStats ();
	}
}
