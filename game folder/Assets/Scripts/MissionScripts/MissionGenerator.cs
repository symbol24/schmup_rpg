using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MissionGenerator : MonoBehaviour {
    [SerializeField] private string leveltoLoad = "loader";
    [SerializeField] private string gameplayLevel = "phil_test";
    [SerializeField] public MissionData[] m_missions;
    private PlayerContainer m_playerController;
    private PrefabContainer m_prefabs;
    private MissionContainer m_missionContainer;
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private GameObject rightPanel;
    [SerializeField] private GameObject btnScavange;
    [SerializeField] private GameObject btnExploration;
    [SerializeField] private GameObject btnBounty;
    private GameObject[] m_buttonList;
    [SerializeField] int m_amountOfMissions = 10;
    [SerializeField] int enemyAmount = 3;
    [SerializeField] int bossAmount = 1;
    [SerializeField] int rewardAmount = 0;

	// Use this for initialization
	void Start () {
        m_playerController = FindObjectOfType<PlayerContainer>();
        if (m_playerController == null)
        {
            print("No player found!");
            Application.LoadLevel(leveltoLoad);
        }

        m_missions = new MissionData[m_amountOfMissions];
        m_prefabs = FindObjectOfType<PrefabContainer>();
        m_missionContainer = FindObjectOfType<MissionContainer>();
        GenerateMissionsRandomly();
        DisplayMissionButtons();
	}

    private void GenerateMissionsRandomly()
    {
        MissionController.MissionType missionType;

        for (int i = 0; i < m_missions.Length; i++)
        {
            missionType = StatCalculator.GetRandomValue<MissionController.MissionType>(0, 0);
            m_missions[i] = GenerateMission(missionType);
        }
    }

    private MissionData GenerateMission(MissionController.MissionType type)
    {
        MissionData ret = new MissionData();
        ret.m_missionCoordinates = Random.Range(0, 360) + ". " + Random.Range(-50, 50) + "' " + Random.Range(-180, 180);
        ret.m_MissionType = type;
        ret.m_playerLevel = m_playerController.m_level;

        bool isEmpty = false;
        EnemyData[] enemyData = new EnemyData[enemyAmount];
        EnemyData[] bossData = new EnemyData[bossAmount];
        EquipmentData[] reward = new EquipmentData[rewardAmount];
        
        switch (type)
        {
            case MissionController.MissionType.bounty:
                isEmpty = false;
                GetEnemyList(enemyData, ret.m_playerLevel, "enemy");
                GetEnemyList(bossData, ret.m_playerLevel, "boss");
                break;
            case MissionController.MissionType.exploration:
                isEmpty = Extensions.randomBoolean();
                switch (isEmpty)
                {
                    case true:
                        enemyData = new EnemyData[0];
                        bossData = new EnemyData[0];
                        break;
                    case false:
                        GetEnemyList(enemyData, ret.m_playerLevel, "enemy");
                        break;
                }
                break;
            case MissionController.MissionType.scavange:
                isEmpty = false;
                ret.m_scavangeTimer = (float)Random.Range(45, 60);
                GetEnemyList(enemyData, ret.m_playerLevel, "enemy");
                bossData = new EnemyData[0];
                break;
        }

        ret.m_isMissionEmpty = isEmpty;
        ret.m_listOfMissionEnemies = enemyData;
        ret.m_listofBosses = bossData;
        ret.m_rewardEquipment = reward;

        return ret;
    }

    

    private void GetEnemyList(EnemyData[] enemies, float playerLevel, string type)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = m_prefabs.GetRandomEnemy(type);
            if (type == "enemy")
                enemies[i] = GenerateStatsForEnemy(enemies[i]);
            else
                enemies[i] = GenerateStatsForBoss(enemies[i]);
        }
    }

    private EnemyData GenerateStatsForEnemy(EnemyData ret)
    {
        int lvl = m_playerController.m_level;
        ret.m_baseDamage = StatCalculator.CalculateBaseDamage(lvl);
        ret.m_damageType = StatCalculator.GetRandomValue<EnergyType>(0, 0);
        ret.m_baseHP = StatCalculator.CalculateEAIBaseHP(lvl);
        ret.m_baseArmour = StatCalculator.CalculateBaseArmor(lvl);
        ret.m_spawnCount = Random.Range(3, 5);
        ret.m_spawnDelay = (float)System.Math.Round(Random.Range(0.35f, 1.5f), 2);
        ret.m_experienceValue = StatCalculator.GetExpValue(EnemyController.EnemyType.grunt);
        return ret;
    }

    private EnemyData GenerateStatsForBoss(EnemyData ret)
    {
        int lvl = m_playerController.m_level;
        ret.m_experienceValue = StatCalculator.GetExpValue(EnemyController.EnemyType.boss);
        ret = GenerateStatsForEnemy(ret);
        ret.m_baseShield = StatCalculator.CalculateBaseShield(lvl);
        ret.m_shieldType = StatCalculator.GetRandomValue<EnergyType>(0, 0);
        ret.m_spawnCount = 1;
        ret.m_spawnDelay = 0;
        return ret;
    }

    private void DisplayMissionButtons()
    {
        GameObject toUse;
        if (m_buttonList != null)
            m_buttonList.DestroyChildren();

        m_buttonList = new GameObject[m_missions.Length];

        int i = 0;
        foreach (MissionData md in m_missions)
        {

            if (i < m_amountOfMissions/2)
                toUse = leftPanel;
            else
                toUse = rightPanel;

            GameObject prefabToUse = btnBounty;
            switch (md.m_MissionType)
            {
                case MissionController.MissionType.bounty:
                    prefabToUse = btnBounty;
                    break;
                case MissionController.MissionType.exploration:
                    prefabToUse = btnExploration;
                    break;
                case MissionController.MissionType.scavange:
                    prefabToUse = btnScavange;
                    break;
            }

            RectTransform panel = toUse.GetComponent<RectTransform>();

            m_buttonList[i] = Instantiate(prefabToUse);
            m_buttonList[i].name = md.m_missionCoordinates;
            RectTransform temp = m_buttonList[i].GetComponent<RectTransform>();
            temp.SetParent(panel, false);
            m_buttonList[i].GetComponent<NavigationButton>().Init();
            i++;
        }

        EventSystem Es = FindObjectOfType<EventSystem>();
        Es.SetSelectedGameObject(m_buttonList[0]);
    }

    public void SetSelectedMission(MissionData mission)
    {
        m_missionContainer.m_listOfMissionEnemies = mission.m_listOfMissionEnemies;
        m_missionContainer.m_isMissionEmpty = mission.m_isMissionEmpty;
        m_missionContainer.m_listofBosses = mission.m_listofBosses;
        m_missionContainer.m_missionCoordinates = mission.m_missionCoordinates;
        m_missionContainer.m_MissionType = mission.m_MissionType;
        m_missionContainer.m_playerLevel = mission.m_playerLevel;
        m_missionContainer.m_rewardEquipment = mission.m_rewardEquipment;
        m_missionContainer.m_scavangeTimer = mission.m_scavangeTimer;
    }
}
