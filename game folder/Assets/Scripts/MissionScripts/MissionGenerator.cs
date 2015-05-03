using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MissionGenerator : MonoBehaviour {
    [SerializeField] private string leveltoLoad = "loader";
    [SerializeField] private string gameplayLevel = "phil_test";
    [SerializeField] public MissionData[] m_missions;
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
        if (FindObjectOfType<PlayerContainer>() == null)
        {
            print("No player found!");
            Application.LoadLevel(leveltoLoad);
        }

        m_missions = new MissionData[m_amountOfMissions];
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

        ret.m_missionLevel = StatCalculator.GetMissionLevel(PlayerContainer.instance.M_level);

        bool isEmpty = false;
        EnemyData[] enemyData = new EnemyData[enemyAmount];
        EnemyData[] bossData = new EnemyData[bossAmount];
        EquipmentData[] reward = new EquipmentData[rewardAmount];
        
        switch (type)
        {
            case MissionController.MissionType.bounty:
                isEmpty = false;
                GetEnemyList(enemyData, ret.m_missionLevel, "enemy");
                GetEnemyList(bossData, ret.m_missionLevel, "boss");
                ret.m_creditReward = GenerateCreditReward(ret.m_missionLevel);
                rewardAmount = 0;
                reward = new EquipmentData[rewardAmount];
                break;
            case MissionController.MissionType.exploration:
                isEmpty = Extensions.randomBoolean();
                switch (isEmpty)
                {
                    case true:
                        enemyData = new EnemyData[0];
                        bossData = new EnemyData[0];
                        reward = new EquipmentData[0];
                        break;
                    case false:
                        GetEnemyList(enemyData, ret.m_missionLevel, "enemy");
                        ret.m_creditReward = GenerateCreditReward(ret.m_missionLevel);
                        rewardAmount = 1;
                        reward = new EquipmentData[rewardAmount];
                        reward = GenerateRewardList(reward);
                        break;
                }
                break;
            case MissionController.MissionType.scavange:
                isEmpty = false;
                ret.m_scavangeTimer = (float)Random.Range(45, 60);
                GetEnemyList(enemyData, ret.m_missionLevel, "enemy");
                bossData = new EnemyData[0];
                rewardAmount = 1;
                reward = new EquipmentData[rewardAmount];
                reward = GenerateRewardList(reward);
                break;
        }

        ret.m_difficulty = StatCalculator.GetMissionDifficulty(ret.m_missionLevel, PlayerContainer.instance.M_level);
        ret.m_experienceValue = StatCalculator.GetMissionExperience(ret.m_difficulty, PlayerContainer.instance.M_level, ret.m_missionLevel);
        ret.m_missionCoordinates = Random.Range(0, 360) + ". " + Random.Range(-50, 50) + "' " + Random.Range(-180, 180);
        ret.m_MissionType = type;
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
            enemies[i] = PrefabContainer.instance.GetRandomEnemy(type);
            if (type == "enemy")
                enemies[i] = GenerateStatsForEnemy(enemies[i]);
            else
                enemies[i] = GenerateStatsForBoss(enemies[i]);
        }
    }

    private EnemyData GenerateStatsForEnemy(EnemyData ret)
    {
        int lvl = PlayerContainer.instance.M_level;
        ret.m_experienceValue = StatCalculator.GetExpValue(EnemyController.EnemyType.grunt);
        ret.m_baseDamage = StatCalculator.CalculateBaseDamage(lvl) * 0.5f;
        ret.m_damageType = StatCalculator.GetRandomValue<EnergyType>(0, 0);
        ret.m_baseHP = StatCalculator.CalculateEAIBaseHP(lvl);
        ret.m_baseArmour = StatCalculator.CalculateBaseArmor(lvl) * 0.01f ;
        ret.m_baseShield = 0.0f;
        ret.m_spawnCount = Random.Range(3, 5);
        ret.m_spawnDelay = (float)System.Math.Round(Random.Range(0.75f, 1.5f), 2);
        return ret;
    }

    private EnemyData GenerateStatsForBoss(EnemyData ret)
    {
        int lvl = PlayerContainer.instance.M_level;
        ret.m_experienceValue = StatCalculator.GetExpValue(EnemyController.EnemyType.boss);
        ret.m_baseDamage = StatCalculator.CalculateBaseDamage(lvl) * 0.5f;
        ret.m_damageType = StatCalculator.GetRandomValue<EnergyType>(0, 0);
        ret.m_baseHP = StatCalculator.CalculateBaseHP(lvl);
        ret.m_baseArmour = StatCalculator.CalculateBaseArmor(lvl) * 0.01f;
        ret.m_baseShield = StatCalculator.CalculateBaseShield(lvl);
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
        MissionContainer.instance.m_listOfMissionEnemies = mission.m_listOfMissionEnemies;
        MissionContainer.instance.m_isMissionEmpty = mission.m_isMissionEmpty;
        MissionContainer.instance.m_listofBosses = mission.m_listofBosses;
        MissionContainer.instance.m_missionCoordinates = mission.m_missionCoordinates;
        MissionContainer.instance.m_MissionType = mission.m_MissionType;
        MissionContainer.instance.m_playerLevel = mission.m_missionLevel;
        MissionContainer.instance.m_rewardEquipment = mission.m_rewardEquipment;
        MissionContainer.instance.m_scavangeTimer = mission.m_scavangeTimer;
        MissionContainer.instance.m_creditValue = mission.m_creditReward;
        MissionContainer.instance.m_difficulty = mission.m_difficulty;
        MissionContainer.instance.m_experienceValue = mission.m_experienceValue;
    }

    private EquipmentData[] GenerateRewardList(EquipmentData[] list)
    {

        if (rewardAmount > 0)
        {
            for (int i = 0; i < rewardAmount; i++)
            {
                list[i] = GenerateRandomReward();
            }
        }

        return list;
    }

    private EquipmentData GenerateRandomReward()
    {
        EquipmentController.equipmentType type = StatCalculator.GetRandomValue<EquipmentController.equipmentType>();

        EquipmentData ret = new EquipmentData();

        switch (type)
        {
            case EquipmentController.equipmentType.chassis:
                ret = ItemGenerator.Chassis(PlayerContainer.instance.M_level);
                break;
            case EquipmentController.equipmentType.cannon:
                ret = ItemGenerator.Cannon(PlayerContainer.instance.M_level);
                break;
            case EquipmentController.equipmentType.engine:
                ret = ItemGenerator.Engine(PlayerContainer.instance.M_level);
                break;
            case EquipmentController.equipmentType.hull:
                ret = ItemGenerator.Hull(PlayerContainer.instance.M_level);
                break;
            case EquipmentController.equipmentType.shield:
                ret = ItemGenerator.Shield(PlayerContainer.instance.M_level);
                break;

        }

        return ret;
    }

    private float GenerateCreditReward(float missionLevel)
    {
        return missionLevel * 1000;
    }
}
