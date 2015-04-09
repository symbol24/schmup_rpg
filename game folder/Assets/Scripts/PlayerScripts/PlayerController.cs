using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization;
using System;
using System.Linq; 

[Serializable]
public class PlayerController : MonoBehaviour {
	public GameManager m_GameManager;

	//the base values compiled
	[SerializeField] protected float[] m_playerBaseStatValues = new float[7];
	
	//the modifiers compiled
	[SerializeField] protected float[] m_playerStatModifiers = new float[7];

	//the calculated info
	public float m_playerDamage = 1.0f;
	public float m_playerFireRate = 1.0f;
	public float m_maxPlayerHP = 1.0f;
	public float m_playerArmor = 0.0f;
	public float m_playerMouvementSpeed = 5.0f;
	public float m_maxPlayerEnergy = 10.0f;
	public float m_maxPlayerShield = 10.0f;

	//boxcollider
	private BoxCollider2D m_myCol;

	//screen limiters
	public float horLimit = 0.0f;
	public float vertLimit = 0.0f;
	private Vector2 velocity = Vector2.zero;

	//animator
	private Animator anim;

	//death state
	private bool isDead = false;
	private Vector3 startingPosition;

	//the explosion to use when a bullet hits
	public GameObject pinkExplosionPrefab;

	//the cannons!
	public GameObject cannonPoint;

	[SerializeField] private CannonController[] cannons;
	private CannonController[] instantiatedCannons;
	public CannonController currentCannon;
	public int cannonID;
	public float cannonSelectionDelay = 0.2f;
	private float cannonSelectionTimer = 0.0f;
	private int cannonSelectionDirection = 0;

	//equipment prefabs!
	[SerializeField] private EquipmentController[] m_listofEquipmentPrefabs;
	private EquipmentController[] m_instantiatedEquipment;
		
	//UI energy, health and shield!
	public EnergySystemController m_energyBar;
	public HPSystemController m_HPBar;
	public ShieldHPSystemController m_shieldBar;

	//hit comparerererer
	public string target;

	void Start(){
		startingPosition = transform.position;
		anim = GetComponent<Animator>();
		m_GameManager = GameObject.Find ("GameManagerObj").GetComponent<GameManager> ();
		
		m_myCol = GetComponent<BoxCollider2D> () as BoxCollider2D;

		//setup equipment
		SetupTempEquipment (m_listofEquipmentPrefabs);
		SetupCannons ();
		CalculateStats ();
		m_energyBar.SetStartValues (m_maxPlayerEnergy);
		m_HPBar.SetStartValues (m_maxPlayerHP);
		m_shieldBar.SetStartValues (m_maxPlayerShield);

		//this = SaveLoad.LoadPlayer ();
		//SaveLoad.SavePlayer (this);
		//m_instantiatedEquipment.First().GetSavableObject().SaveObject ("saveTest.xml");
		//SaveLoad.Save ();
	}
	
	void Update () {
		if(m_GameManager.m_CurrentState == GameManager.gameState.playing && !isDead){

			//cannon selection
			cannonSelectionDirection = -1;
			if(Input.GetKeyDown(KeyCode.C) || Input.GetKey(m_GameManager.m_CannonSelectionButtons[0])){
				if(Time.time >= cannonSelectionTimer){
					cannonSelectionDirection = 0;
					cannonSelectionTimer = Time.time + cannonSelectionDelay;
				}
			}else if(Input.GetKeyDown(KeyCode.V) || Input.GetKey(m_GameManager.m_CannonSelectionButtons[1])){
				if(Time.time >= cannonSelectionTimer){
					cannonSelectionDirection = 1;
					cannonSelectionTimer = Time.time + cannonSelectionDelay;
				}
			}
			if(cannonSelectionDirection != -1) ChangeSelectedCannon(cannonSelectionDirection);

			//move
			velocity.x = m_GameManager.m_HorValue * m_playerMouvementSpeed * Time.deltaTime;
			velocity.y = m_GameManager.m_VertValue * m_playerMouvementSpeed * Time.deltaTime;

			//changing the ship from side to side and idle
			if (m_GameManager.m_HorValue > 0.0f) {
				anim.SetInteger("direction",1);
			} else if (m_GameManager.m_HorValue < 0.0f) {
				anim.SetInteger("direction", -1);
			}else{
				anim.SetInteger("direction", 0);
			}

			//move the ship, cannon and shields
			//velocity = Vector2.ClampMagnitude(velocity, m_playerMouvementSpeed * Time.deltaTime);
			transform.Translate (velocity, Space.World);

			//clamping to screen size
			float clampedLimitX = Mathf.Clamp(transform.position.x, -horLimit, horLimit);
			float clampedLimitY = Mathf.Clamp(transform.position.y, -vertLimit, vertLimit);			
			transform.position = new Vector3 (clampedLimitX, clampedLimitY, 0.0f);
		}
	}

	//checking health to change amount of shields and changing amount of lives if needed
	public void CheckHealth(){
		if(m_HPBar.GetCurrentValue() <= 0){
			m_HPBar.SetCurrentValue(m_maxPlayerHP);
			StartCoroutine(m_GameManager.DeathExplosion(m_HPBar.GetCurrentValue()));
		}
	}

	public void RepositionShip(){
		transform.position = startingPosition;
		currentCannon.transform.position = cannonPoint.transform.position;
	}

	//changing the equipped gun with the rpess of a button!
	private void ChangeSelectedCannon(int direction){
		int previousID = cannonID;
		bool foundCannon = false;
		if (direction == 0 && cannonID > 0) {
			while(!foundCannon && cannonID > 0){
				cannonID--;
				if(instantiatedCannons[cannonID].m_IsAvailable) foundCannon = true;
			}
			if(!foundCannon) cannonID = previousID;
		}else if(direction == 1 && cannonID < instantiatedCannons.Length-1){
			while(!foundCannon && cannonID < (instantiatedCannons.Length-1)){
				cannonID++;
				if(instantiatedCannons[cannonID].m_IsAvailable) foundCannon = true;
			}
			if(!foundCannon) cannonID = previousID;
		}
		if (cannonID != previousID) {
			currentCannon.gameObject.SetActive(false);
			currentCannon = instantiatedCannons[cannonID];
			currentCannon.gameObject.SetActive(true);
			WeaponSwitchUpdateValuesModifiers(instantiatedCannons[previousID], currentCannon);
		}
	}

//	public void ActivateCannon(int newCannonID){
//		instantiatedCannons [newCannonID].m_IsAvailable = true;
//		cannonID = newCannonID;
//		currentCannon.gameObject.SetActive(false);
//		currentCannon = instantiatedCannons[cannonID];
//		currentCannon.gameObject.SetActive(true);
//	}

	private void SetupCannons(){
		//instantiating the first cannon
		instantiatedCannons = new CannonController[cannons.Length];
		for (int i = 0; i < cannons.Length; i++) {
			instantiatedCannons[i] = Instantiate (cannons[i], cannonPoint.transform.position, cannonPoint.transform.rotation) as CannonController;
			instantiatedCannons[i].transform.parent = transform;
			if(i == cannonID) {
				currentCannon = instantiatedCannons[i];
				currentCannon.m_IsAvailable = true;
				UpdateValuesModifiers(currentCannon);
			}else {
				instantiatedCannons[i].gameObject.SetActive(false);
				instantiatedCannons[i].m_IsAvailable = true;
			}
		}
	}

	private void SetupTempEquipment(EquipmentController[] listofEquip){
		m_instantiatedEquipment = new EquipmentController[listofEquip.Length];
		for(int i = 0; i < listofEquip.Length; i++){
			m_instantiatedEquipment[i] = Instantiate(listofEquip[i], this.transform.position, this.transform.rotation) as EquipmentController;
			EquipmentData tempData = new EquipmentData();
			m_instantiatedEquipment[i].Init(this, tempData);
			m_instantiatedEquipment[i].transform.parent = transform;
			UpdateValuesModifiers(m_instantiatedEquipment[i]);
		}
	}

	public void UpdateCollider(bool status){
		m_myCol.enabled = status;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Owner == target) {
			Instantiate (pinkExplosionPrefab, tempBullet.transform.position, tempBullet.transform.rotation);
			m_HPBar.SetCurrentValue(DamageCalculators.Hit(tempBullet.m_DamageValue, m_HPBar.GetCurrentValue(), m_playerArmor));
			CheckHealth();
			tempBullet.DestroyObjectAndBehaviors();
		}
	}

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
