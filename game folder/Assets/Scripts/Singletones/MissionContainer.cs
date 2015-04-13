using UnityEngine;
using System.Collections;

public class MissionContainer : MonoBehaviour, iMissionContainer {
	private static iMissionContainer mInstance;
	
	public static iMissionContainer instance{
		get{ return mInstance ?? (mInstance = new MissionContainerDummy());}
	}

	public MissionController.MissionType m_missionType;
	public int m_playerLevel;
	public EnemyData[] m_listOfMissionEnemies;
	public BossData m_bossEnemy;


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

}

public class MissionContainerDummy : iMissionContainer{
	public MissionController.MissionType m_missionType;
	public int m_playerLevel;
}