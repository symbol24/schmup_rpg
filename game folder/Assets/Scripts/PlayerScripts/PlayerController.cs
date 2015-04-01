using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public GameManager m_GameManager;

	//the player basic info
	private float m_playerDamage = 1.0f;
	private float m_playerFireRate = 1.0f;
	private int m_maxPlayerHP = 1;
	private int m_currentHP = 1;
	private int m_playerArmor = 0;
	private float m_playerMouvementSpeed = 5.0f;
	private float m_maxPlayerEnergy = 10.0f;
	private float m_currentEnergy = 10.0f;
	private float m_currentCredits = 0.0f;

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
	public CannonController[] instanciatedCannons;
	public CannonController currentCannon;
	public int cannonID;
	public float cannonSelectionDelay = 0.2f;
	private float cannonSelectionTimer = 0.0f;
	private int cannonSelectionDirection = 0;

	//the shields used for absorbtion effect
	public GameObject m_Shield;

	//hit comparerererer
	public string target;

	void Start(){
		startingPosition = transform.position;
		anim = GetComponent<Animator>();
		m_GameManager = GameObject.Find ("GameManagerObj").GetComponent<GameManager> ();

		//instantiating the first cannon
		instanciatedCannons = new CannonController[cannons.Length];
		for (int i = 0; i < cannons.Length; i++) {
			instanciatedCannons[i] = Instantiate (cannons[i], cannonPoint.transform.position, cannonPoint.transform.rotation) as CannonController;
			instanciatedCannons[i].transform.parent = transform;
			if(i == cannonID) {
				currentCannon = instanciatedCannons[i];
				currentCannon.m_IsAvailable = true;
			}else {
				instanciatedCannons[i].gameObject.SetActive(false);
				instanciatedCannons[i].m_IsAvailable = false;
			}
		}
		m_currentHP = m_maxPlayerHP;
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
		if(m_currentHP <= 0){
			m_currentHP = m_maxPlayerHP;
			StartCoroutine(m_GameManager.DeathExplosion(m_currentHP));
		}else{
			m_Shield.SetActive (false);
		}
	}

	public void RepositionShip(){
		transform.position = startingPosition;
		currentCannon.transform.position = cannonPoint.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Owner == target) {
			Instantiate (pinkExplosionPrefab, tempBullet.transform.position, tempBullet.transform.rotation);
			m_currentHP = m_GameManager.Hit(tempBullet.m_DamageValue, m_currentHP, m_playerArmor);
			CheckHealth();
			tempBullet.pushBullet(tempBullet);
		}
	}

	//changing the equipped gun with the rpess of a button!
	private void ChangeSelectedCannon(int direction){
		int previousID = cannonID;
		bool foundCannon = false;
		if (direction == 0 && cannonID > 0) {
			while(!foundCannon && cannonID > 0){
				cannonID--;
				if(instanciatedCannons[cannonID].m_IsAvailable) foundCannon = true;
			}
			if(!foundCannon) cannonID = previousID;
		}else if(direction == 1 && cannonID < instanciatedCannons.Length-1){
			while(!foundCannon && cannonID < (instanciatedCannons.Length-1)){
				cannonID++;
				if(instanciatedCannons[cannonID].m_IsAvailable) foundCannon = true;
			}
			if(!foundCannon) cannonID = previousID;
		}
		if (cannonID != previousID) {
			currentCannon.gameObject.SetActive(false);
			currentCannon = instanciatedCannons[cannonID];
			currentCannon.gameObject.SetActive(true);
		}
	}

	public void ActivateCannon(int newCannonID){
		instanciatedCannons [newCannonID].m_IsAvailable = true;
		cannonID = newCannonID;
		currentCannon.gameObject.SetActive(false);
		currentCannon = instanciatedCannons[cannonID];
		currentCannon.gameObject.SetActive(true);
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
}
