using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour {

	public GameManager m_GameMgr;
	private PlayerController m_playerController;
	private MissionController m_missionController;
	public float m_LimiterY = -4.9f;
	public float m_EaiHP = 1;
	public float m_CurrentHP;
	public float m_EaiArmor = 0;
	public float m_experienceValue = 100;
	public GameObject m_BlueExplosion;
	public string m_Target = "player";
	public ProjectileController m_ProjectileToShoot;
	public ProjectileController[] m_ListOfProjectilesToShoot;
	public float m_ShootDelay = 1.0f;
	public EAIBehaviors[] m_BehaviorsPrefabs;
	private EAIBehaviors[] m_BehaviorsInstances;
	private EAIBehaviors m_DeathBehavior;
	public CannonReferences[] m_CannonReferances;
	public float m_MouvementSpeed = 1.0f;
	public GameObject m_HealthBar;
	public int m_CannonUpgradeID = 1;
	public bool m_IsDying = false;
    public EnergyType m_damageType;
    public float m_baseDamage;
    public int m_shieldType;
    public float m_baseShield;

	void Start(){
		m_GameMgr = FindObjectOfType<GameManager> ();
		m_missionController = FindObjectOfType<MissionController> ();
		m_playerController = FindObjectOfType<PlayerController> ();

		//Get cannon references
		m_CannonReferances = GetCannonReferences ();

		//instanciate all behaviours attached to EAI
		m_BehaviorsInstances = new EAIBehaviors[m_BehaviorsPrefabs.Length];
		for(int i = 0; i < m_BehaviorsPrefabs.Length; i++){
			m_BehaviorsInstances[i] = Instantiate(m_BehaviorsPrefabs[i], transform.position, transform.rotation) as EAIBehaviors;
			m_BehaviorsInstances[i].Init(this);
			m_BehaviorsInstances[i].transform.SetParent(transform);
		}

		foreach (EAIBehaviors behavior in m_BehaviorsInstances) {
			if(behavior.m_BehaviorName == "death") m_DeathBehavior = behavior;
		}
		//if(m_DeathBehavior == null) print("THERE IS NO DEATH BEHAVIOR!!!!");
		m_CurrentHP = m_EaiHP;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_GameMgr.m_CurrentState == GameManager.gameState.playing){
			foreach(EAIBehaviors behavior in m_BehaviorsInstances){
				if(behavior != null){
					behavior.UpdateBehavior();
				}
			}

			if(transform.position.y < m_LimiterY){
				DestroyObjectAndBehaviors(0);
			}
		}
	}

	private void checkHealth(){
		if (m_CurrentHP <= 0) {
			m_IsDying = true;
			if(m_DeathBehavior != null && m_DeathBehavior.m_BehaviorDeathType == "boss")
				m_DeathBehavior.StartExplosions(50);
			else{
				DestroyObjectAndBehaviors(m_experienceValue);
			}
		}
	}

	public void DestroyObjectAndBehaviors(float score){
		gameObject.SetActive (false);
//		m_GameMgr.UpdateScore(score);
		foreach (EAIBehaviors behavior in m_BehaviorsInstances) {
			if(behavior != null){
				Destroy(behavior.gameObject);
			}
		}
		m_playerController.AddExp(score);
		if(score > 0)
			m_missionController.IncrementKillCount ();

		m_missionController.DecreaseSpawnCount ();
		Destroy (this.gameObject);
	}
	
	public void OnTriggerEnter2D(Collider2D coll) {
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Owner == m_Target) {
			foreach(EAIBehaviors behavior in m_BehaviorsInstances){
				if(behavior != null){
					EAIBehaviorEvade evader = behavior.GetComponentInChildren<EAIBehaviorEvade>();
					if(evader != null){
						evader.EvaderHit();
					}
					EAIBehaviorTeleporter teleporter = behavior.GetComponentInChildren<EAIBehaviorTeleporter>();
					if(teleporter != null){
						teleporter.TeleporterHit();
					}
				}
			}
			Instantiate (m_BlueExplosion, tempBullet.transform.position, tempBullet.transform.rotation);
			m_CurrentHP = DamageCalculators.Hit(tempBullet.m_DamageValue, m_CurrentHP, m_EaiArmor);
			checkHealth();
			tempBullet.DestroyObjectAndBehaviors(false);
		}
	}

	public CannonReferences[] GetCannonReferences(){
		CannonReferences[] cannonRefs = GetComponentsInChildren<CannonReferences>() as CannonReferences[];
		if (cannonRefs == null) {
			print ("NO CHILD CANNON REFS ON " + this);
			cannonRefs = new CannonReferences[0];
		}
		return cannonRefs;
	}

	public void LoadFromInternal(EnemyData data){

        m_damageType = data.m_damageType;
        m_EaiHP = data.m_baseHP;
        m_baseDamage = data.m_baseDamage;
        m_EaiArmor = data.m_baseArmour;
        m_experienceValue = data.m_experienceValue;
        m_shieldType = data.m_shieldType;
        m_baseShield = data.m_baseShield;
	}

    public EnemyData SaveData()
    {
        EnemyData ret = new EnemyData();

        ret.m_damageType = m_damageType;
        ret.m_baseHP = m_EaiHP;
        ret.m_baseDamage = m_baseDamage;
        ret.m_baseArmour = m_EaiArmor;
        ret.m_experienceValue = m_experienceValue;
        ret.m_shieldType = m_shieldType;
        ret.m_baseShield = m_baseShield;
        ret.m_PrefabName = gameObject.name;

        return ret;
    }
}

public class ScoreEventArgs : EventArgs{
	public float score{ get; set;}

}