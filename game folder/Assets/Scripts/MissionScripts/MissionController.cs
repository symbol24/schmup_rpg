using UnityEngine;
using System.Collections;

public class MissionController : MonoBehaviour {

	public enum MissionType {
		exploration,
		scavange,
		bounty,
	}

	private MissionType m_missionType;

	private enum SpawnStatus{
		waiting,
		spawningEnemies,
		spawningBoss,
		fighting,
	}

	private SpawnStatus m_spawnStatus;

	private PrefabContainer m_prefabDatabase;

	private PlayerController m_playerController;

	[SerializeField] private float m_minSpawnDelay = 0.0f;
	[SerializeField] private float m_maxSpawnDelay = 0.0f;
	private float m_spawnTimer = 0.0f;

	private int m_currentEnemytoSpawn = 0;
	private EnemyController[] m_listofEnemies;
	private EnemyController[] m_listofBosses;
	private EnemySpawnController m_enemySpawner;

	private EquipmentData m_rewardEquipment;

	public float m_spawnerX, m_spawnerY;

	private int m_totalSpawnCount;
	private int m_currentSpawnCount;
	public int m_delaySpawnCount;
	public int m_bossSpawnCount;

	// Use this for initialization
	void Start () {
		m_prefabDatabase = FindObjectOfType<PrefabContainer> ();


		GetMIssionInfo ();

		m_spawnStatus = SpawnStatus.spawningEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_spawnStatus) {
		case SpawnStatus.waiting:
			break;
		case SpawnStatus.spawningEnemies:
			if(m_spawnTimer <= Time.time){
				SpawnEnemySpawnController (MissionContainer.instance.m_listOfMissionEnemies);
				float delay = Random.Range(m_minSpawnDelay, m_maxSpawnDelay);
				m_spawnTimer = Time.time + delay;

			}
			break;
		case SpawnStatus.spawningBoss:
			SpawnEnemySpawnController (MissionContainer.instance.m_listofBosses);
			m_spawnStatus = SpawnStatus.waiting;
			break;
		}
	}

	private void GetMIssionInfo(){
		//enemyspawner prefab
		m_enemySpawner = m_prefabDatabase.GetEnemySpawner();

		//mission data from container
		m_missionType = MissionContainer.instance.m_MissionType;

		m_listofEnemies = new EnemyController[MissionContainer.instance.m_listOfMissionEnemies.Length];
		for(int i = 0; i < MissionContainer.instance.m_listOfMissionEnemies.Length; i++){
			m_listofEnemies[i] = m_prefabDatabase.GetEnemyPerName(MissionContainer.instance.m_listOfMissionEnemies[i].m_PrefabName);
		}
		if (m_listofEnemies.Length <= 0)
			print ("No enemies in enemy data");


		m_listofBosses = new EnemyController[MissionContainer.instance.m_listofBosses.Length];
		for(int i = 0; i < MissionContainer.instance.m_listofBosses.Length; i++){
			m_listofBosses[i] = m_prefabDatabase.GetEnemyPerName(MissionContainer.instance.m_listofBosses[i].m_PrefabName);
		}
		if (m_listofBosses.Length <= 0)
			print ("No bosses in boss data");

	}

	private void SpawnEnemySpawnController(EnemyData[] enemy){
		float x = Random.Range (-m_spawnerX, m_spawnerX); 
		Vector3 pos = new Vector3 (x, m_spawnerY, 0);

		EnemyController prefab = m_prefabDatabase.GetEnemyPerName (enemy[m_currentEnemytoSpawn].m_PrefabName);

		EnemySpawnController toSpawn = Instantiate (m_enemySpawner, pos, transform.rotation) as EnemySpawnController;
		toSpawn.SetEnemyToSpawn (prefab, enemy[m_currentEnemytoSpawn].m_spawnCount, enemy[m_currentEnemytoSpawn].m_spawnDelay);

		m_currentEnemytoSpawn++;
		if(m_currentEnemytoSpawn >= MissionContainer.instance.m_listOfMissionEnemies.Length)
			m_currentEnemytoSpawn = 0;
	}

	public void IncrementSpawnCount(int c){

	}
}
