using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabContainer : MonoBehaviour, iPrefabContainer
{
    private static iPrefabContainer prefabInstance;

    public static iPrefabContainer instance
    {
        get { return prefabInstance ?? (prefabInstance = new PrefabContainerDummy()); }

    }

    void Awake()
    {
        if (prefabInstance == null || prefabInstance is PrefabContainerDummy)
        {
            prefabInstance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	[SerializeField] private EquipmentController[] m_ListofEquipments;
    public EquipmentController[] M_ListofEquipments { get { return m_ListofEquipments; } }
    [SerializeField] private ProjectileController[] m_ListofBullets;
    public ProjectileController[] M_ListofBullets { get { return m_ListofBullets; } }
    [SerializeField] private PlayerController m_playerControllerPrefab;
    public PlayerController M_playerControllerPrefab { get { return m_playerControllerPrefab; } }
    [SerializeField] private EnemyController[] m_listofEnemies;
    public EnemyController[] M_listofEnemies { get { return m_listofEnemies; } }
    [SerializeField] private EnemyController[] m_listofBosses;
    public EnemyController[] M_listofBosses { get { return m_listofBosses; } }
    [SerializeField] private EnemySpawnController m_enemySpawnController;
    public EnemySpawnController M_enemySpawnController { get { return m_enemySpawnController; } }

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

    public ProjectileController GetRandomProjective()
    {
        List<ProjectileController> list = new List<ProjectileController>();

        foreach (ProjectileController p in M_ListofBullets)
        {
            if (p.m_Owner == "player") list.Add(p);
        }

        int ran = Random.Range(0, list.Count);
        return list[ran];
    }

    public EquipmentController GetRandomEquipement(EquipmentController.equipmentType type)
    {
        List<EquipmentController> list = new List<EquipmentController>();
        foreach (EquipmentController e in M_ListofEquipments)
        {
            if(e.m_myType == type)
                list.Add(e);
        }
        int ran = Random.Range(0, list.Count);
        return list[ran];
    }
}



public interface iPrefabContainer
{
    EquipmentController[] M_ListofEquipments { get; }
    ProjectileController[] M_ListofBullets { get; }
    PlayerController M_playerControllerPrefab { get; }
    EnemyController[] M_listofEnemies { get; }
    EnemyController[] M_listofBosses { get; }
    EnemySpawnController M_enemySpawnController { get; }
    EquipmentController GetEquipmentPerName(string name);
    ProjectileController GetBulletPerName(string name);
    PlayerController GetPlayerController();
    EnemyController GetEnemyPerName(string name);
    EnemySpawnController GetEnemySpawner();
    EnemyData GetRandomEnemy(string type);
    ProjectileController GetRandomProjective();
    EquipmentController GetRandomEquipement(EquipmentController.equipmentType type);
}

public class PrefabContainerDummy : iPrefabContainer
{
    [SerializeField]
    private EquipmentController[] m_ListofEquipments;
    public EquipmentController[] M_ListofEquipments { get { return m_ListofEquipments; } }
    [SerializeField]
    private ProjectileController[] m_ListofBullets;
    public ProjectileController[] M_ListofBullets { get { return m_ListofBullets; } }
    [SerializeField]
    private PlayerController m_playerControllerPrefab;
    public PlayerController M_playerControllerPrefab { get { return m_playerControllerPrefab; } }
    [SerializeField]
    private EnemyController[] m_listofEnemies;
    public EnemyController[] M_listofEnemies { get { return m_listofEnemies; } }
    [SerializeField]
    private EnemyController[] m_listofBosses;
    public EnemyController[] M_listofBosses { get { return m_listofBosses; } }
    [SerializeField]
    private EnemySpawnController m_enemySpawnController;
    public EnemySpawnController M_enemySpawnController { get { return m_enemySpawnController; } }




    ProjectileController[] iPrefabContainer.M_ListofBullets
    {
        get { throw new System.NotImplementedException(); }
    }

    PlayerController iPrefabContainer.M_playerControllerPrefab
    {
        get { throw new System.NotImplementedException(); }
    }

    EnemyController[] iPrefabContainer.M_listofEnemies
    {
        get { throw new System.NotImplementedException(); }
    }

    EnemyController[] iPrefabContainer.M_listofBosses
    {
        get { throw new System.NotImplementedException(); }
    }

    EnemySpawnController iPrefabContainer.M_enemySpawnController
    {
        get { throw new System.NotImplementedException(); }
    }

    public EquipmentController GetEquipmentPerName(string name)
    {
        throw new System.NotImplementedException();
    }

    public ProjectileController GetBulletPerName(string name)
    {
        throw new System.NotImplementedException();
    }

    public PlayerController GetPlayerController()
    {
        throw new System.NotImplementedException();
    }

    public EnemyController GetEnemyPerName(string name)
    {
        throw new System.NotImplementedException();
    }

    public EnemySpawnController GetEnemySpawner()
    {
        throw new System.NotImplementedException();
    }

    public EnemyData GetRandomEnemy(string type)
    {
        throw new System.NotImplementedException();
    }

    public ProjectileController GetRandomProjective()
    {
        throw new System.NotImplementedException();
    }


    public EquipmentController GetRandomEquipement(EquipmentController.equipmentType type)
    {
        throw new System.NotImplementedException();
    }
}