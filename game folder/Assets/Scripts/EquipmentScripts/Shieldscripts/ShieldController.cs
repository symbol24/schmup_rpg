using UnityEngine;
using System.Collections;

public class ShieldController : EquipmentController {
	public float m_regenerationDelay = 2.0f;
	public float m_timeToFull = 3.0f;
	public float m_shieldArmor = 0;


	public override void Init(PlayerController player){
		base.Init (player);
		m_playerController.m_shieldBar.m_shield = this;
	}

	private void ShieldHit(ProjectileController bullet){
		m_playerController.m_shieldBar.SetCurrentValue(m_playerController.m_GameManager.Hit(bullet.m_DamageValue, m_playerController.m_shieldBar.m_currentValue, m_shieldArmor));
		m_playerController.m_shieldBar.CheckShieldHealth ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Target == m_Owner) {
			ShieldHit(tempBullet);
			tempBullet.DestroyObjectAndBehaviors();
		}
	}
}
