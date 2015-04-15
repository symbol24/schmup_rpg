using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 

public class ProjectileController : MonoBehaviour, IProjectileController
{
    #region BaseStats
    public float m_DamageValue = 2.0f;
    public int m_damageType = 0;
    public int m_EnergyValue = 1;
    public float m_Speed = 5.0f;
    public string m_Target = "enemy";
    public string m_Owner = "player";
    public string m_Type = "";
    public float m_bonusAtt = 0f;
    public float Damage { get { return m_DamageValue; } }
    public EnergyType DamageType { get { return (EnergyType)m_damageType; } }
    public int EnergyValue { get { return m_EnergyValue; } }
    public float BonusAtt { get { return m_bonusAtt; } }
    public TargetEnum Target { get { return m_Target == "enemy" ? TargetEnum.Enemies : TargetEnum.Player; } }
    #endregion BaseStats
    private GameManager m_GameManager;
	public ProjectileBehavior[] m_ProjectileBehaviorPrefabs;
	public ProjectileBehavior[] m_ProjectileBehaviorInstances;
    public GameObject m_explosion;

	void Start(){
		m_GameManager = GameObject.Find ("GameManagerObj").GetComponent<GameManager> ();
		m_ProjectileBehaviorInstances = new ProjectileBehavior[m_ProjectileBehaviorPrefabs.Length];
		for(int i = 0; i < m_ProjectileBehaviorPrefabs.Length; i++){
			m_ProjectileBehaviorInstances[i] = Instantiate(m_ProjectileBehaviorPrefabs[i], transform.position, transform.rotation) as ProjectileBehavior;
			m_ProjectileBehaviorInstances[i].Init(this);
			m_ProjectileBehaviorInstances[i].transform.SetParent(transform);
		}
	}

	// Update is called once per frame
	void Update () {
		if(m_GameManager.m_CurrentState == GameManager.gameState.playing){
			foreach(ProjectileBehavior behavior in m_ProjectileBehaviorInstances){
				behavior.UpdateBehavior();
			}
			if(m_Type != "beam"){
				//putting the bullets back into their respective STACK
				if(gameObject.activeInHierarchy && !gameObject.GetComponent<Renderer>().isVisible){
					Destroy(this.gameObject);
				}
			}
		}
	}

    

    public void DestroyObjectAndBehaviors(bool executeDestruction){
		gameObject.SetActive (false);
		foreach (ProjectileBehavior behavior in m_ProjectileBehaviorInstances) {
			if(behavior != null){
				Destroy(behavior.gameObject);
			}
		}
        if (executeDestruction) Instantiate(m_explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
