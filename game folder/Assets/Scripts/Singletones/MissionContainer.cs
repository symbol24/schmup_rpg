using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MissionContainer : MonoBehaviour, iMissionContainer {
	private static iMissionContainer mInstance;
	
	public static iMissionContainer instance{
		get{ return mInstance ?? (mInstance = new MissionContainerDummy());}
	}

    string MissionCoordinates;
    public string m_missionCoordinates { get{ return MissionCoordinates; } set{MissionCoordinates = value;} }

	MissionController.MissionType MissionType;
	public MissionController.MissionType m_MissionType{ get{ return MissionType; } set{ MissionType = value; } }

	float playerLevel;
	public float m_playerLevel{ get{ return playerLevel; } set{ playerLevel = value; } }

    bool isMissionEmpty;
    public bool m_isMissionEmpty { get { return isMissionEmpty; } set { isMissionEmpty = value; } }

	EnemyData[] listofEnemies;
	public EnemyData[] m_listOfMissionEnemies{ get{ return listofEnemies; } set{ listofEnemies = value; } }

    EnemyData[] listofBosses;
    public EnemyData[] m_listofBosses { get { return listofBosses; } set { listofBosses = value; } }

	EquipmentData[] rewardEquipment;
	public EquipmentData[] m_rewardEquipment{ get{ return rewardEquipment; } set{ rewardEquipment = value; } }

    float scavangeTimer;
    public float m_scavangeTimer { get { return scavangeTimer; } set { scavangeTimer = value; } }

    MissionDifficulty difficulty;
    public MissionDifficulty m_difficulty { get { return difficulty; } set { difficulty = value; } }

    float creditValue;
    public float m_creditValue { get { return creditValue; } set { creditValue = value; } }

    float experieneValue;
    public float m_experienceValue { get { return experieneValue; } set { experieneValue = value; } }

	void Awake(){
		if (mInstance == null || mInstance is MissionContainerDummy) {
			mInstance = this;
			
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}
}


public interface iMissionContainer
{
    string m_missionCoordinates { get; set; }
	MissionController.MissionType m_MissionType { get; set; }
	float m_playerLevel { get; set; }
    bool m_isMissionEmpty { get; set; }
	EnemyData[] m_listOfMissionEnemies { get; set; }
    EnemyData[] m_listofBosses { get; set; }
	EquipmentData[] m_rewardEquipment { get; set; }
    float m_scavangeTimer { get; set; }
    MissionDifficulty m_difficulty { get; set; }
    float m_creditValue { get; set; }
    float m_experienceValue { get; set; }
}

public class MissionContainerDummy : iMissionContainer
{
    public string m_missionCoordinates { get; set; }
	public MissionController.MissionType m_MissionType{ get; set; }
    public float m_playerLevel { get; set; }
    public bool m_isMissionEmpty { get; set; }
	public EnemyData[] m_listOfMissionEnemies{ get; set; }
    public EnemyData[] m_listofBosses { get; set; }
	public EquipmentData[] m_rewardEquipment{ get; set; }
    public float m_scavangeTimer { get; set; }
    public MissionDifficulty m_difficulty { get; set; }
    public float m_creditValue { get; set; }
    public float m_experienceValue { get; set; }
}