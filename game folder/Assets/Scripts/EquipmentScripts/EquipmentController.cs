using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class EquipmentController : MonoBehaviour {
	public PlayerController m_playerController;

	public enum equipmentType{
		cannon,
		chassis,
		engine,
		hull,
		shield
	}

	public equipmentType m_myType;

	public int m_equipmentLevel = 1;
	public float m_creditValue = 1.0f;
	public int m_damageType = 0;
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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
