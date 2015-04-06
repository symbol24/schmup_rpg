using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : BasePlayerController {
	public GameManager m_GameManager;

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
	public CannonController[] cannons;
	public CannonController[] instantiatedCannons;
	public CannonController currentCannon;
	public int cannonID;
	public float cannonSelectionDelay = 0.2f;
	private float cannonSelectionTimer = 0.0f;
	private int cannonSelectionDirection = 0;

	//equipment prefabs!
	public EquipmentController[] m_listofEquipmentPrefabs;
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

	public void ActivateCannon(int newCannonID){
		instantiatedCannons [newCannonID].m_IsAvailable = true;
		cannonID = newCannonID;
		currentCannon.gameObject.SetActive(false);
		currentCannon = instantiatedCannons[cannonID];
		currentCannon.gameObject.SetActive(true);
	}

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
			m_instantiatedEquipment[i].Init(this);
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
			m_HPBar.SetCurrentValue( m_GameManager.Hit(tempBullet.m_DamageValue, m_HPBar.GetCurrentValue(), m_playerArmor));
			CheckHealth();
			tempBullet.DestroyObjectAndBehaviors();
		}
	}
}
