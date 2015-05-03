using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    //the managers
	private GameManager m_gameManager;
    private MenuController m_menuController;

    //scavange timer
    private GameObject m_scavangeTimerPanel;
    private Text m_scavangeText;
    private float m_scavangeTimer;

    //spawn delay timer stuff
	[SerializeField] private float m_minSpawnDelay = 0.0f;
	[SerializeField] private float m_maxSpawnDelay = 0.0f;
	private float m_Timer = 0.0f;
    private bool m_survived = false;

    //intro delay
    [SerializeField] private float m_introDelay = 5.0f;
    private bool m_isIntroDisplayed = false;
    private bool m_canStart = false;

    //outro
    private bool m_isOutroDisplayed = false;

	private int m_currentEnemytoSpawn = 0;
	private EnemyController[] m_listofEnemies;
	private EnemyController[] m_listofBosses;
	private EnemySpawnController m_enemySpawner;

	private EquipmentData[] m_rewardEquipment;

	public float m_spawnerX, m_spawnerY;

	private int m_killCount;
	private int m_currentSpawnCount;
	public int m_delaySpawnCount;
	public int m_bossSpawnCount;

    private bool m_isBossSpawned = false;
    private bool m_isMissionSuccesful = false;
    private bool m_isRewardGiven = false;

	// Use this for initialization
	void Start () {
		m_gameManager = FindObjectOfType<GameManager> ();
        m_menuController = FindObjectOfType<MenuController>();
		m_spawnerX = m_gameManager.m_limiterX;
		GetMissionInfo ();
        
        m_Timer = Time.time + m_introDelay;
		m_spawnStatus = SpawnStatus.intro;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_gameManager.m_CurrentState == GameManager.gameState.playing) {
		    switch (m_spawnStatus) {
		    case SpawnStatus.intro:
                    if (m_scavangeTimerPanel == null) GetScavangerTimerPanel();

                    if (!m_isIntroDisplayed) m_isIntroDisplayed = DisplayIntro();

                    if (m_canStart) { 
                        if (m_Timer <= Time.time) {

                            nextStatus = SpawnStatus.spawningEnemies;

                            //empty exploration goes to outro
                            if (MissionContainer.instance.m_isMissionEmpty)
                                nextStatus = SpawnStatus.outro;

				            m_spawnStatus = nextStatus;
                        }
                    }
			    break;
		    case SpawnStatus.waiting:
                    //do nothing

                if (m_missionType == MissionType.scavange && m_survived && m_currentSpawnCount <= 0)
                {
                    m_spawnStatus = SpawnStatus.outro;
                    m_isMissionSuccesful = true;
                }
			    break;
		    case SpawnStatus.spawningEnemies:
			    if(m_Timer <= Time.time && m_currentSpawnCount < m_delaySpawnCount){
				    SpawnEnemySpawnController (MissionContainer.instance.m_listOfMissionEnemies);
				    float delay = Random.Range(m_minSpawnDelay, m_maxSpawnDelay);
				    m_Timer = Time.time + delay;

			    }

                if (m_missionType == MissionType.scavange && m_scavangeTimer > 0.0f)
                {
                    m_scavangeTimer -= Time.deltaTime;
                    m_scavangeText.text = "" + (int)m_scavangeTimer;
                }
                else if (m_missionType == MissionType.scavange && m_scavangeTimer <= 0.0f)
                {
                    m_survived = true;
                    m_spawnStatus = SpawnStatus.waiting;
                }
                    break;
		    case SpawnStatus.waitToSpawnBoss:
			    if(m_currentSpawnCount <=0)
				    m_spawnStatus = SpawnStatus.spawningBoss;
			    break;
		    case SpawnStatus.spawningBoss:
                m_currentEnemytoSpawn = 0;
			    SpawnEnemySpawnController (MissionContainer.instance.m_listofBosses);
			    m_spawnStatus = SpawnStatus.waiting;
                m_isBossSpawned = true;
			    break;
            case SpawnStatus.outro:
                if (!m_isOutroDisplayed) m_isOutroDisplayed = DisplayOutro();
                if (!m_isRewardGiven) m_isRewardGiven = GiveRewardsToPlayer();

                break;
		    }
        }
        else if (m_gameManager.m_CurrentState == GameManager.gameState.gameover)
        {
            if (!m_isOutroDisplayed) m_isOutroDisplayed = DisplayOutro();
        }
	}

    private void GetScavangerTimerPanel()
    {
        m_scavangeTimerPanel = GameObject.Find("ScavangeTimerPanel");
        m_scavangeText = m_scavangeTimerPanel.GetComponentInChildren<Text>();
        m_scavangeTimerPanel.SetActive(false);
    }

    private bool DisplayIntro(){
        if (m_scavangeTimerPanel != null)
        {

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

                    m_scavangeTimerPanel.SetActive(true);
                    m_scavangeTimer = MissionContainer.instance.m_scavangeTimer;
                    m_scavangeText.text = m_scavangeTimer.ToString();
                    break;
            }

            m_menuController.DisplayIntroPanel(m_missionType.ToString());

            return true;
        }

        return false;
    }

	private void GetMissionInfo(){
		//enemyspawner prefab
		m_enemySpawner = PrefabContainer.instance.GetEnemySpawner();

		//mission data from container
		m_missionType = MissionContainer.instance.m_MissionType;

		m_listofEnemies = new EnemyController[MissionContainer.instance.m_listOfMissionEnemies.Length];
		for(int i = 0; i < MissionContainer.instance.m_listOfMissionEnemies.Length; i++){
			m_listofEnemies[i] = PrefabContainer.instance.GetEnemyPerName(MissionContainer.instance.m_listOfMissionEnemies[i].m_PrefabName);
		}
		if (m_listofEnemies.Length <= 0)
			print ("No enemies in enemy data");


		m_listofBosses = new EnemyController[MissionContainer.instance.m_listofBosses.Length];
		for(int i = 0; i < MissionContainer.instance.m_listofBosses.Length; i++){
            if (m_listofBosses[i] != null)
            {
			    m_listofBosses[i] = PrefabContainer.instance.GetEnemyPerName(MissionContainer.instance.m_listofBosses[i].m_PrefabName);
            }

		}
		if (m_listofBosses.Length <= 0)
			print ("No bosses in boss data");

	}

	private void SpawnEnemySpawnController(EnemyData[] enemy){
		float x = Random.Range (-m_spawnerX, m_spawnerX); 
		Vector3 pos = new Vector3 (x, m_spawnerY, 0);
        //print("m_currentEnemytoSpawn " + m_currentEnemytoSpawn);
		EnemyController prefab = PrefabContainer.instance.GetEnemyPerName (enemy[m_currentEnemytoSpawn].m_PrefabName);
        

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
        if (m_missionType == MissionType.bounty && m_killCount >= m_bossSpawnCount && !m_isBossSpawned)
        {
			m_spawnStatus = SpawnStatus.waitToSpawnBoss;
        }
        else if (m_isBossSpawned)
        {
            m_spawnStatus = SpawnStatus.outro;
            m_isMissionSuccesful = true;
        }
	}

    public void StartMission(Menu menu)
    {
        m_canStart = true;
        m_menuController.HideMenu(menu);
    }

    private bool DisplayOutro(){
        m_menuController.DisplayOutroPanel(m_isMissionSuccesful);
        return true;

    }

    private bool GiveRewardsToPlayer()
    {
        PlayerContainer.instance.M_experience += MissionContainer.instance.m_experienceValue;
        PlayerContainer.instance.M_credits += MissionContainer.instance.m_creditValue;

        if (MissionContainer.instance.m_rewardEquipment.Length > 0) PlayerContainer.instance.M_inventory.Add(MissionContainer.instance.m_rewardEquipment[0]);

        return true;
    }
}
