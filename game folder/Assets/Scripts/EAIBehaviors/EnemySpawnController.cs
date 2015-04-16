using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {
	private GameManager m_GameManager;
	private MissionController m_missionController;

	public EnemyController m_EAIPrefab;
	public int m_AmountToSpawn = 1;
	private int m_EnemyCounter = 0;
	private float m_SpawnRate = 0.0f;
	private float m_NextSpawn = 0.0f;
	private float m_SpawnAtY = 5.5f;
	private float m_Speed = 0.8f;
	private enum SpawnerState{
		moving,
		spawning,
		immobile,
		dying
	}
	private SpawnerState m_SpawnerState;
	private SpawnerState m_PreviousState;
    private EnemyData m_enemyData;


	// Use this for initialization
	void Start () {
		m_GameManager = FindObjectOfType<GameManager> ();
		m_SpawnerState = SpawnerState.moving;
		m_missionController = FindObjectOfType<MissionController> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_GameManager.m_CurrentState){
			case GameManager.gameState.playing:

				switch (m_SpawnerState){
					case SpawnerState.moving:
						transform.Translate (Vector3.down * m_Speed * Time.deltaTime, Space.World);
						if(transform.position.y <= m_SpawnAtY) {
							m_SpawnerState = SpawnerState.spawning;
						}
					break;

					case SpawnerState.spawning:
						if(m_GameManager != null && m_GameManager.m_CurrentState == GameManager.gameState.playing){
							//spawn enemies
						if (Time.time > m_NextSpawn && m_EnemyCounter <= m_AmountToSpawn ){
							m_NextSpawn = Time.time + m_SpawnRate;
							EnemyController eaiClone = Instantiate(m_EAIPrefab, transform.position, transform.rotation) as EnemyController;
                            eaiClone.LoadFromInternal(m_enemyData);
							eaiClone.gameObject.SetActive(true);
							m_missionController.IncrementSpawnCount();
							m_EnemyCounter++;
						}
						if(m_EnemyCounter >= m_AmountToSpawn)
							m_SpawnerState = SpawnerState.dying;
						}
					break;

					case SpawnerState.immobile:
						//do nothing!
					break;

					case SpawnerState.dying:
						Destroy(gameObject);
					break;
				}
			break;
		}
	}

	public void StopSpawners(){
		m_PreviousState = m_SpawnerState;
		m_SpawnerState = SpawnerState.immobile;
	}
	
	public void RestartState(){
		m_SpawnerState = m_PreviousState;
	}

	public void SetEnemyToSpawn(EnemyData data, EnemyController enemy, int Amount, float delay){
        m_enemyData = data;
		m_EAIPrefab = enemy;
		m_AmountToSpawn = Amount;
		m_SpawnRate = delay;
	}
}
