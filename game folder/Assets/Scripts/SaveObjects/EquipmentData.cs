using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class EquipmentData {
	public EquipmentController.equipmentType m_myType;
	
	public string m_prefabName = "";
	public int m_equipmentLevel = 1;
	public float m_creditValue = 1.0f;
	public int m_damageType = 0;
	public string m_Owner = "player";
	
	public float[] m_baseValues = new float[7];
	public float[] m_ValueModifiers = new float[7];
	
	//for cannon
	public float m_baseWeaponDamage = 1.0f;
	public float m_baseWeaponFireRate = 0.05F;
	
	//for shield
	public float m_regenerationDelay = 2.0f;
	public float m_timeToFull = 3.0f;
}
