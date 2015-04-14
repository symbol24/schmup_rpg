using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MissionContainer : MonoBehaviour, iMissionContainer {
	private static iMissionContainer mInstance;
	
	public static iMissionContainer instance{
		get{ return mInstance ?? (mInstance = new MissionContainerDummy());}
	}
	public MissionController.MissionType MissionType;
	public MissionController.MissionType m_MissionType{ get{ return MissionType; } set{ MissionType = value; } }

	public int playerLevel;
	public int m_playerLevel{ get{ return playerLevel; } set{ playerLevel = value; } }

	public EnemyData[] listofEnemies;
	public EnemyData[] m_listOfMissionEnemies{ get{ return listofEnemies; } set{ listofEnemies = value; } }

	public BossData[] listofBosses;
	public BossData[] m_listofBosses{ get{ return listofBosses; } set{ listofBosses = value; } }

	public EquipmentData[] rewardEquipment;
	public EquipmentData[] m_rewardEquipment{ get{ return rewardEquipment; } set{ rewardEquipment = value; } }


	void Awake(){
		if (mInstance == null || mInstance is MissionContainerDummy) {
			mInstance = this;
			
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}
}


public interface iMissionContainer{
	MissionController.MissionType m_MissionType { get; set; }
	int m_playerLevel { get; set; }
	EnemyData[] m_listOfMissionEnemies { get; set; }
	BossData[] m_listofBosses { get; set; }
	EquipmentData[] m_rewardEquipment { get; set; }
}

public class MissionContainerDummy : iMissionContainer{
	public MissionController.MissionType m_MissionType{ get; set; }
	public int m_playerLevel{ get; set; }
	public EnemyData[] m_listOfMissionEnemies{ get; set; }
	public BossData[] m_listofBosses{ get; set; }
	public EquipmentData[] m_rewardEquipment{ get; set; }
}