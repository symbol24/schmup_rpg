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
		intro,
		waiting,
		spawningEnemies,
		waitToSpawnBoss,
		spawningBoss,
		outro,
	}
    private SpawnStatus m_spawnStatus;
    private SpawnStatus nextStatus;

	//the global
	private PrefabContainer m_prefabDatabase;

    //the manage
	private GameManager m_gameManager;

    //the player
    private PlayerController m_playerController;

    //spawn delay timer stuff
	[SerializeField] private float m_minSpawnDelay = 0.0f;
	[SerializeField] private float m_maxSpawnDelay = 0.0f;
	private float m_Timer = 0.0f;

    //intro delay
    [SerializeField] private float m_introDelay;

	private int m_currentEnemytoSpawn = 0;
	private EnemyController[] m_listofEnemies;
	private EnemyController[] m_listofBosses;
	private EnemySpawnController m_enemySpawner;

	private EquipmentData m_rewardEquipment;

	public float m_spawnerX, m_spawnerY;

	private int m_killCount;
	private int m_currentSpawnCount;
	public int m_delaySpawnCount;
	public int m_bossSpawnCount;

	// Use this for initialization
	void Start () {
		m_prefabDatabase = FindObjectOfType<PrefabContainer> ();
		m_gameManager = FindObjectOfType<GameManager> ();
		m_spawnerX = m_gameManager.m_limiterX;
		GetMissionInfo ();
        m_Timer = Time.time + m_introDelay;
		m_spawnStatus = SpawnStatus.intro;
        DisplayIntro();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_gameManager.m_CurrentState == GameManager.gameState.playing) {
		    switch (m_spawnStatus) {
		    case SpawnStatus.intro:
                    if (m_Timer <= Time.time) {

                        nextStatus = SpawnStatus.spawningEnemies;

                        //empty exploration goes to outro
                        if (MissionContainer.instance.m_isMissionEmpty)
                            nextStatus = SpawnStatus.outro;

				        m_spawnStatus = nextStatus;
                    }
			    break;
		    case SpawnStatus.waiting:
                    //do nothing
			    break;
		    case SpawnStatus.spawningEnemies:
			    if(m_Timer <= Time.time && m_currentSpawnCount < m_delaySpawnCount){
				    SpawnEnemySpawnController (MissionContainer.instance.m_listOfMissionEnemies);
				    float delay = Random.Range(m_minSpawnDelay, m_maxSpawnDelay);
				    m_Timer = Time.time + delay;

			    }
			    break;
		    case SpawnStatus.waitToSpawnBoss:
			    if(m_currentSpawnCount <=0)
				    m_spawnStatus = SpawnStatus.spawningBoss;
			    break;
		    case SpawnStatus.spawningBoss:
			    SpawnEnemySpawnController (MissionContainer.instance.m_listofBosses);
			    m_spawnStatus = SpawnStatus.waiting;
			    break;
            case SpawnStatus.outro:
                //ask to return to hub
                break;
		    }
        }
	}

    private void DisplayIntro(){
        //implement mission intro stuff
        print("Misison Type = " + m_missionType);
        switch (m_missionType)
        {
            case MissionType.bounty:
                //do somthing to show the mission has a boss to kill
                //there may or may not be rewards
                break;
            case MissionType.exploration:
                //show if there is something here or not
                //there may or may not be rewards
                break;
            case MissionType.scavange:
                //show that the goal is to retrive rewards
                //tehre may or may not be a boss
                break;
        }
    }

	private void GetMissionInfo(){
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

        if (enemy[m_currentEnemytoSpawn] == null) m_currentEnemytoSpawn = 0;

		EnemySpawnController toSpawn = Instantiate (m_enemySpawner, pos, transform.rotation) as EnemySpawnController;
		toSpawn.SetEnemyToSpawn (enemy[m_currentEnemytoSpawn], prefab, enemy[m_currentEnemytoSpawn].m_spawnCount, enemy[m_currentEnemytoSpawn].m_spawnDelay);

		m_currentEnemytoSpawn++;
		if(m_currentEnemytoSpawn >= MissionContainer.instance.m_listOfMissionEnemies.Length)
			m_currentEnemytoSpawn = 0;
	}

	public void IncrementSpawnCount(){
		m_currentSpawnCount++;
	}

	public void DecreaseSpawnCount(){
		m_currentSpawnCount--;
	}

	public void IncrementKillCount(){
		m_killCount++;
		if (m_missionType == MissionType.bounty && m_killCount >= m_bossSpawnCount) {
			m_spawnStatus = SpawnStatus.waitToSpawnBoss;
		}
	}
}
