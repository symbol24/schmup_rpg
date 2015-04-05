using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EAIBehaviorSimpleShoot : EAIBehaviors {
	public float m_FireRate = 1.0f;
	public float m_NextFire = 0.0F;
	private ProjectileController m_BulletToShoot;
	public float m_Offset;

	// Use this for initialization
	public override void Start(){		
		base.Start ();
		m_FireRate = m_Controller.m_ShootDelay;
		m_NextFire = Time.time + m_FireRate;
		m_BulletToShoot = m_Controller.m_ProjectileToShoot;
	}

	// Update is called once per frame
	public override void UpdateBehavior() {
		if (Time.time > m_NextFire){
			m_NextFire = Time.time + m_FireRate;

			Vector2 newPos = new Vector2(m_Controller.transform.position.x, m_Controller.transform.position.y - m_Offset);

			ProjectileController oneBullet = Instantiate(m_BulletToShoot, newPos, m_BulletToShoot.transform.rotation) as ProjectileController;
		}
	}

}
