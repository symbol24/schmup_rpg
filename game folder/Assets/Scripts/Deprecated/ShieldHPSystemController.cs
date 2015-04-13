using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ShieldHPSystemController : BaseBarSystemController {
	public ShieldController m_shield;
	private float m_regenTimer;

	public override void Start(){
		base.Start ();
	}

	public override void Update(){
		base.Update ();
		if (IsShieldDead () && m_regenTimer <= Time.time) {
			StartRegenartion();
		}

		if (!m_isRegenarating && m_shield.m_shieldArmor > 0) {
			m_shield.m_shieldArmor = 0; //reset armor
		}
	}

	public override void SetPlayerShip(PlayerController playerShip){
		base.SetPlayerShip (playerShip);
		m_maxValue = playerShip.m_maxPlayerShield;
		m_currentValue = m_maxValue;

	}

	public virtual void SetShieldController(ShieldController passing)
	{
		m_shield = passing;
	}

	public void CheckShieldHealth(){
		if (IsShieldDead ()) {
			SwitchShieldStatus(false);
			m_regenTimer = Time.time + m_shield.m_regenerationDelay;
		}
	}

	public bool IsShieldDead(){
		if (m_currentValue > 0) {
			return false;
		}
		return true;
	}

	public void SwitchShieldStatus(bool status){
		m_shield.gameObject.SetActive(status);
		m_player.UpdateCollider (!status);//inverse of shield
	}

	private void StartRegenartion(){
		SwitchShieldStatus (true);
		m_shield.m_shieldArmor = 100000; //temp super armor
		float increment = m_maxValue / (m_shield.m_timeToFull / Time.deltaTime);
		m_isRegenarating = Regenration (increment);
	}
}
