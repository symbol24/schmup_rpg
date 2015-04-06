using UnityEngine;
using System.Collections;

public class ShieldHPSystemController : BaseBarSystemController {
	public ShieldController m_shield;
	private float m_regenTimer;

	public override void Start(){
		base.Start ();
		m_maxValue = m_player.m_maxPlayerShield;
		m_currentValue = m_maxValue;
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
