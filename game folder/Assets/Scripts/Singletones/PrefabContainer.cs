using UnityEngine;
using System.Collections;

public class PrefabContainer : MonoBehaviour {

	[SerializeField] private EquipmentController[] m_ListofEquipments;
	[SerializeField] private ProjectileController[] m_ListofBullets;
	[SerializeField] private PlayerController m_playerControllerPrefab;
	[SerializeField] private EnemyController[] m_listofEnemies;
	[SerializeField] private EnemyController[] m_listofBosses;
	[SerializeField] private EnemySpawnController m_enemySpawnController;

	public EquipmentController GetEquipmentPerName(string name){
		foreach (EquipmentController e in m_ListofEquipments) {
			if(e.gameObject.name == name)
				return e;
		}
		print ("Equipment prefab " + name + " not found");
		return null;
	}

	public ProjectileController GetBulletPerName(string name){
		foreach (ProjectileController p in m_ListofBullets) {
			if(p.gameObject.name == name)
				return p;
		}
		print ("Bullet prefab " + name + " not found");
		return null;
	}

	public PlayerController GetPlayerController(){
		return m_playerControllerPrefab;
	}

	public EnemyController GetEnemyPerName(string name){
		foreach (EnemyController e in m_listofEnemies) {
			if(e.gameObject.name == name)
				return e;
		}
        foreach (EnemyController e in m_listofBosses)
        {
            if (e.gameObject.name == name)
                return e;
        }

		print ("Enemy prefab " + name + " not found");
		return null;
	}

	public EnemySpawnController GetEnemySpawner(){
		return m_enemySpawnController;
	}

    public EnemyData GetRandomEnemy(string type)
    {
        EnemyData ret;
        EnemyController[] toUse;


        if (type == "boss") toUse = m_listofBosses;
        else toUse = m_listofEnemies;

        int index = Random.Range(0, toUse.Length);
        ret = toUse[index].SaveData();

        return ret;
    }
}

