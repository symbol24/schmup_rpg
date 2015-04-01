using UnityEngine;
using System.Collections;

public class BasePlayerController : MonoBehaviour {
	//the player basic info
	private float m_playerDamage = 1.0f;
	private float m_playerFireRate = 1.0f;
	private float m_maxPlayerHP = 1.0f;
	private float m_currentHP = 1.0f;
	private float m_playerArmor = 0.0f;
	private float m_playerMouvementSpeed = 5.0f;
	private float m_maxPlayerEnergy = 10.0f;
	private float m_currentEnergy = 10.0f;
	private float m_currentCredits = 0.0f;
	private float m_experiencePoints = 0.0f;

	// Use this for initialization
	void Start () {
		m_currentHP = m_maxPlayerHP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetPlayerDamage(){
		return m_playerDamage;
	}
	
	public void SetPlayerDamage(float newDamage){
		m_playerDamage = newDamage;
	}
	
	public float GetPlayerFireRate(){
		return m_playerFireRate;
	}
	
	public void SetPlayerFireRate(float newFireRate){
		m_playerFireRate = newFireRate;
	}

	public float GetPlayerMaxHP(){
		return m_maxPlayerHP;
	}
	
	public void SetPlayerMaxHP(float newHP){
		m_maxPlayerHP = newHP;
	}

	public float GetPlayerCurrentHP(){
		return m_currentHP;
	}
	
	public void SetPlayerCurrentHP(float newHP){
		m_currentHP = newHP;
	}

	public float GetPlayerArmour(){
		return m_playerArmor;
	}
	
	public void SetPlayerArmour(float newArmour){
		m_playerArmor = newArmour;
	}
	
	public float GetPlayerCurrentEnergy(){
		return m_currentEnergy;
	}
	
	public void SetPlayerCurrentEnergy(float newCurrentEnergy){
		m_currentEnergy = newCurrentEnergy;
	}
	
	public float GetPlayerMaxEnergy(){
		return m_maxPlayerEnergy;
	}
	
	public void SetPlayerMaxEnergy(float newMaxEnergy){
		m_maxPlayerEnergy = newMaxEnergy;
	}

	public float GetPlayerSpeed(){
		return m_playerMouvementSpeed;
	}
	
	public void SetPlayerSpeed(float newSpeed){
		m_playerMouvementSpeed = newSpeed;
	}
}
