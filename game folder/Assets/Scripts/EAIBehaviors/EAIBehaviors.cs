using UnityEngine;
using System.Collections;

public class EAIBehaviors : MonoBehaviour{
	protected EnemyController m_Controller;
	public string m_BehaviorName = "base";
	public string m_BehaviorDeathType = "simple";

	public virtual void Init (EnemyController controller) {
		m_Controller = controller;
	}

	public virtual void Start(){
		transform.position = m_Controller.transform.position;
	}

	public virtual void Update(){}

	public virtual void UpdateBehavior(){}
	
	public virtual void StartExplosions(int explosionCount){}

	public void ShootABullet(CannonReferences refereance, ProjectileController bulletTemplate){
		ProjectileController oneBullet = Instantiate(bulletTemplate, refereance.transform.position, refereance.transform.rotation) as ProjectileController;
	}

}
