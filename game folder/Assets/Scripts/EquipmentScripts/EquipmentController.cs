using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization;
using System; 

[Serializable]
public class EquipmentController : MonoBehaviour {
	public PlayerController m_playerController;

	public enum equipmentType : int{
		cannon,
		chassis,
		engine,
		hull,
		shield
	}

    public string m_equipmentName;
	public equipmentType m_myType;

	public int m_equipmentLevel = 1;
	public float m_creditValue = 1.0f;
	public EnergyType m_damageType = 0;
	public string m_Owner = "player";

	//base values
	public float[] m_baseValues = new float[7];
//	public float m_baseDamage = 0.0f;
//	public float m_baseFireRate = 0.0f;
//	public float m_baseHealth = 0.0f;
//	public float m_baseArmour = 0.0f;
//	public float m_baseSpeed = 0.0f;
//	public float m_baseEnergy = 0.0f;
//	public float m_baseShield = 0.0f;

	//modifiers
	public float[] m_ValueModifiers = new float[7];
//	public float m_damageModifier = 1.0f;
//	public float m_fireRateModifier = 1.0f;
//	public float m_speedModifier = 1.0f;
//	public float m_armourModifier = 1.0f;
//	public float m_energyModifier = 1.0f;
//	public float m_shieldModifier = 1.0f;
//	public float m_healthModifier = 1.0f;


	public virtual void Init(PlayerController player){
		m_playerController = player;
	}

	public virtual T GetSavableObjectInternal<T>() where T : EquipmentData
	{
		var ret = Activator.CreateInstance<T> ();
		ret.m_prefabName = this.gameObject.name;
		ret.m_baseValues = m_baseValues;
		ret.m_Owner = m_Owner;
		ret.m_ValueModifiers = m_ValueModifiers;
		ret.m_creditValue = m_creditValue;
		ret.m_damageType = m_damageType;
		ret.m_equipmentLevel = m_equipmentLevel;
		ret.m_myType = m_myType;

		return ret;
	}

	public virtual void LoadFromInternal(EquipmentData data)
	{
		m_baseValues = data.m_baseValues;
		m_Owner = data.m_Owner;
		m_ValueModifiers = data.m_ValueModifiers;
		m_creditValue = data.m_creditValue;
		m_damageType = data.m_damageType;
		m_equipmentLevel = data.m_equipmentLevel;
		m_myType = data.m_myType;
        m_equipmentName = data.m_equipmentName;
	}

}
