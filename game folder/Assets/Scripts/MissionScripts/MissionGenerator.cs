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
        EnemyData[] enemyData = new EnemyData[0];
        EnemyData[] bossData = new EnemyData[0];
        EquipmentData[] reward = new EquipmentData[0];
        
        switch (type)
        {
            case MissionController.MissionType.bounty:
                isEmpty = false;
                enemyData = GetEnemyList(ret.m_playerLevel);
                break;
            case MissionController.MissionType.exploration:
                isEmpty = Extensions.randomBoolean();
                switch (isEmpty)
                {
                    case true:
                        enemyData = new EnemyData[0];
                        break;
                    case false:
                        enemyData = GetEnemyList(ret.m_playerLevel);
                        break;
                }
                break;
            case MissionController.MissionType.scavange:
                isEmpty = false;
                enemyData = GetEnemyList(ret.m_playerLevel);
                break;
        }

        ret.m_isMissionEmpty = isEmpty;
        ret.m_listOfMissionEnemies = enemyData;
        ret.m_listofBosses = bossData;
        ret.m_rewardEquipment = reward;

        return ret;
    }

    

    private EnemyData[] GetEnemyList(float playerLevel)
    {
        EnemyData[] ret = new EnemyData[3];
        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = m_prefabs.GetRandomEnemy("enemy");
            ret[i] = GenerateStatsForEnemy(ret[i]);

        }

        return ret;
    }

    private EnemyData GenerateStatsForEnemy(EnemyData ret)
    {
        int lvl = m_playerController.m_level;
        ret.m_baseDamage = StatCalculator.CalculateBaseDamage(lvl);
        ret.m_damageType = StatCalculator.GetRandomValue<EnergyType>(0, 0);
        ret.m_baseHP = StatCalculator.CalculateBaseHP(lvl);
        ret.m_baseArmour = StatCalculator.CalculateBaseArmor(lvl);
        return ret;
    }

    private void DisplayMissionButtons()
    {
        GameObject toUse;
        if (m_buttonList != null)
            Extensions.DestroyChildren(m_buttonList);

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
    }
}
