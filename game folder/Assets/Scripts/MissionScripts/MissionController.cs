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
		spawningEnemies,
		spawningBoss,
		fighting,
	}

	private SpawnStatus m_spawnStatus;

	private float m_spawnDelay = 0.0f;
	private float m_spawnTimer = 0.0f;

	private int m_currentEnemytoSpawn = 0;
	private EnemyController[] m_listofEnemies;
	private EnemyController m_boss;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetInfo(){
		MissionContainer container = FindObjectOfType<MissionContainer> ();
	}
}
