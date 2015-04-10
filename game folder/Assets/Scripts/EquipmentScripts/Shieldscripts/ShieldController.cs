using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ShieldController : EquipmentController, ISavable<ShieldData> {
	public float m_shieldArmor = 0;
	public float m_regenerationDelay = 2.0f;
	public float m_timeToFull = 3.0f;

	public Collider2D m_collider;

	public override void Init(PlayerController player){
		base.Init (player);
		player.m_shieldBar.m_shield = this;
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

	#region ISavable implementation

	public ShieldData GetSavableObject ()
	{
		var ret = GetSavableObjectInternal <ShieldData>();
		ret.m_regenerationDelay = m_regenerationDelay;
		ret.m_timeToFull = m_timeToFull;
		return ret;
	}

	public void LoadFrom (ShieldData data)
	{
		LoadFromInternal (data);
		m_regenerationDelay = data.m_regenerationDelay;
		m_timeToFull = data.m_timeToFull;
	}

	#endregion
}
