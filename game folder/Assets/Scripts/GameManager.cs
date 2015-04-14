using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 

public class GameManager : MonoBehaviour {
	//the player ship
	//public PlayerController m_PlayerShip;

	//the player and enemy bullets
//	public int m_BulletAmount;
//	public ProjectileController[] m_ProjectilePrefabs;
//	public Stack<ProjectileController>[] m_ProjectileStacks;
//	public Stack<ProjectileController>[] m_PlayerProjectileStacks;

	//game state enum
	public enum gameState{
		playing, paused, gameover, dead
	}
	public gameState m_CurrentState;

	
	//life icons top left of screen
	public int m_NumberOfLives;
//	public GameObject m_LifeIconPrefab;
//	public GameObject[] m_LifeIconsDisplayed;

	//explosions when dead
	public GameObject longPinkExplosionPrefab;
	public int numberOfExplosions;
	public float explosionOffset;
	public float explosionAnimationTime;
	public float deathControlDelay;

	//score info
//	public GUIText m_ScoreGUI;
//	public float m_TotalScore = 0;
//	private float m_TotalKills = 0;
//	public float m_TargetScore;

	//controls
	public float m_VertValue;
	public float m_HorValue;
	public float m_firebutton;
	public float m_altFireButton;
	public float[] m_switchButtons = new float[2];
	public float m_pauseButton;
	public float m_backButton;
	
	//menu dely timer
	public float m_MenuDelayTimer = 0.0f;
	public float m_MenuDeadSpot = 0.1f;

	//end game messages
	public string m_VictoryMessage;
	public string m_LoseMessage;

	//for firing status and shield management
	public bool m_isShooting = false;

	//for the powerups! no longer used
	//public PowerUpController m_PowerUpPrefab;

	//next level!
	public string m_NextLevel = "Hub";

	public float m_limiterX = 4.9f;

	
	void Start(){
//		m_PlayerShip = SaveLoad.LoadPlayer ();
//		m_PlayerShip.CalculateStats ();
		//creating the life icons at top of screen
//		m_LifeIconsDisplayed = new GameObject[m_NumberOfLives];
//		for(int i = 0; i < m_NumberOfLives; i++){
//			GameObject tempLifeIcon = Instantiate(m_LifeIconPrefab, new Vector2(m_LifeIconPrefab.transform.position.x - i, m_LifeIconPrefab.transform.position.y), m_LifeIconPrefab.transform.rotation) as GameObject;
//			m_LifeIconsDisplayed[i] = tempLifeIcon;
//		}
		//setting the game state to playing
		m_CurrentState = gameState.playing;

		//for some reason at work, i had to force the time scale to have the game play once the start is passed
		Time.timeScale = 1.0f;
	}

	
	// Update is called once per frame
	void Update () {
		
		//get both controller and keyboard axis's
		m_VertValue = Input.GetAxis("Vertical");
		m_HorValue = Input.GetAxis("Horizontal");
		m_firebutton = Input.GetAxis("Fire");
		m_altFireButton = Input.GetAxis("Alt Fire");
		m_switchButtons[0] = Input.GetAxis("Switch Left");
		m_switchButtons[1] = Input.GetAxis("Switch Right");
		m_pauseButton = Input.GetAxis("Pause");
		m_backButton = Input.GetAxis("Back");
	}
//
//	public void SetPlayerShip(PlayerController playerShip){
//		m_PlayerShip = playerShip;
//	}

	//reduce the amount of lives, remove a visible life icon and trigger endgame
	public void DecreaseLives(){
		m_NumberOfLives -= 1;
		//m_LifeIconsDisplayed[m_NumberOfLives].SetActive(false);
		if(m_NumberOfLives <= 0){
			SetGameOver(m_LoseMessage);
		}
	}
	
//	public void UpdateScore(float score){
//		m_TotalKills++;
//		m_TotalScore += score;
//		m_ScoreGUI.text = m_TotalScore.ToString ();
//	}

	//endgame process
	public void SetGameOver(string message){
		m_CurrentState = gameState.gameover;
		EndGameMenu egm = GetComponent<EndGameMenu> ();
		if (egm != null) {
			egm.DisplayGameOverScreen(message);
		}
	}

	public void ChangeLevel(){
		Application.LoadLevel(m_NextLevel);
	}

	public IEnumerator DeathExplosion(PlayerController player, float currentHP){
		m_CurrentState = gameState.dead;
		Transform transformForExplosion = player.transform;
		player.GetComponent<Renderer>().enabled = false;
		player.currentCannon.gameObject.SetActive (false);
		GameObject explosion;
		for(int i = 0; i < numberOfExplosions; i++){
			float xOffset = (Random.Range(-explosionOffset, explosionOffset));
			Vector3 newPos = transformForExplosion.transform.position + new Vector3(xOffset, -(explosionOffset * i), 0);			               
			explosion = Instantiate (longPinkExplosionPrefab, newPos, transformForExplosion.transform.rotation) as GameObject;
			Destroy(explosion, explosionAnimationTime);
			yield return new WaitForSeconds(explosionAnimationTime/2);
		}
		yield return new WaitForSeconds(deathControlDelay);
		DecreaseLives();
		if(m_NumberOfLives > 0){
			player.RepositionShip();
			player.GetComponent<Renderer>().enabled = true;
			player.currentCannon.gameObject.SetActive (true);
			m_CurrentState = gameState.playing;
		}
	}
}

