using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ShieldController : EquipmentController {
	public float m_shieldArmor = 0;
	public float m_regenerationDelay = 2.0f;
	public float m_timeToFull = 3.0f;

	public override void Init(PlayerController player){
		base.Init (player);
		m_myType = equipmentType.shield;
		m_playerController.m_shieldBar.m_shield = this;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Target == m_Owner) {
			int tempBulletDmg = tempBullet.m_damageType;
			m_playerController.m_shieldBar.SetCurrentValue(DamageCalculators.ShieldHit(tempBullet.m_DamageValue, m_playerController.m_shieldBar.m_currentValue, m_shieldArmor, tempBulletDmg, m_damageType));
			m_playerController.m_shieldBar.CheckShieldHealth ();
			tempBullet.DestroyObjectAndBehaviors();
		}
	}
}
