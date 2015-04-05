using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonController : EquipmentController {
	private GameManager m_GameManager;

	public float m_baseWeaponDamage = 1.0f;
	public float m_baseWeaponFireRate = 0.05F;
	private float m_NextFire = 0.0F;

	public GameObject[] m_ReferencePointForBullet;
	public ProjectileController m_ProjectileToShootPrefab;
	private Stack<ProjectileController> m_StackToUse;
	private ProjectileController m_BeamInstance;
	private bool m_FiringBeam = false;
	public int m_ProjectileEnergyValue = 1;
	private EnergySystemController m_EnergyBar;
	public bool m_IsAvailable = false;

	void Start(){
		m_GameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
		m_ProjectileEnergyValue = m_ProjectileToShootPrefab.m_EnergyValue;
		m_EnergyBar = GameObject.FindObjectOfType(typeof(EnergySystemController)) as EnergySystemController;
	}

	// Update is called once per frame
	void Update () {
		if(m_GameManager.m_CurrentState == GameManager.gameState.playing){
			if (((Input.GetKey(KeyCode.Space) || Input.GetKey(m_GameManager.m_ShootButton))) && m_EnergyBar.GetCurrentEnergy() >= m_ProjectileEnergyValue){
				if(!m_GameManager.m_isShooting){
					m_GameManager.SwitchShieldStatus(true);
				}
				if(m_ProjectileToShootPrefab.m_Type == "beam"){
					if(!m_FiringBeam){
						m_BeamInstance = Instantiate(m_ProjectileToShootPrefab, transform.position, transform.rotation) as ProjectileController;
						m_BeamInstance.gameObject.SetActive(true);
						m_FiringBeam = true;
					}else{
						m_BeamInstance.gameObject.SetActive(true);

					}
				}else{
					if(Time.time > m_NextFire){
					m_NextFire = Time.time + m_baseWeaponFireRate;
					foreach(GameObject refer in m_ReferencePointForBullet){
							ShotABullet(refer, m_ProjectileToShootPrefab);
						}
					}
				}
			}else{
				if(m_GameManager.m_isShooting){
					m_GameManager.SwitchShieldStatus(false);
					m_FiringBeam = false;
				}
			}
		}
	}

	private void ShotABullet(GameObject refereance, ProjectileController bulletTemplate){
		ProjectileController oneBullet = Instantiate(m_ProjectileToShootPrefab, refereance.transform.position, refereance.transform.rotation) as ProjectileController;
		m_EnergyBar.ChangeEnergyTotal ("substract", m_ProjectileEnergyValue);
	}
}
