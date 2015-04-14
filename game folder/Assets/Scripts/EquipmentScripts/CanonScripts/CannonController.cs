using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 

public class CannonController : EquipmentController, ISavable<CannonData> {
	private GameManager m_GameManager;

	public float m_baseWeaponDamage = 1.0f;
	public float m_baseWeaponFireRate = 0.05F;
	private float m_NextFire = 0.0F;

	public GameObject[] m_ReferencePointForBullet;
	public ProjectileController m_ProjectileToShootPrefab;
	public int m_ProjectileEnergyValue = 1;
	private EnergySystemController m_EnergyBar;
	public bool m_IsAvailable = false;
	public string m_cannonTag = "playerCannonRef";

	public override void Init(PlayerController player){
		base.Init (player);
	}

	void Start(){
		m_GameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
		m_ProjectileEnergyValue = m_ProjectileToShootPrefab.m_EnergyValue;
		m_EnergyBar = GameObject.FindObjectOfType(typeof(EnergySystemController)) as EnergySystemController;
		m_ReferencePointForBullet = GameObject.FindGameObjectsWithTag (m_cannonTag);
	}

	// Update is called once per frame
	void Update () {

	}

	public void FireForEachReference(float damage, float delay){
		if(Time.time > m_NextFire && m_EnergyBar.GetCurrentValue() >= m_ProjectileEnergyValue){
				m_NextFire = Time.time + delay;
				
				foreach(GameObject refer in m_ReferencePointForBullet){
					ShotABullet(refer, m_ProjectileToShootPrefab, damage);
				}
			}


	}

	private void ShotABullet(GameObject refereance, ProjectileController bulletTemplate, float damage){
		ProjectileController oneBullet = Instantiate(m_ProjectileToShootPrefab, refereance.transform.position, refereance.transform.rotation) as ProjectileController;
		oneBullet.m_DamageValue = damage;
		m_EnergyBar.ChangeEnergyTotal ("substract", m_ProjectileEnergyValue);
	}

	#region ISavable implementation

	public CannonData GetSavableObject ()
	{
		var ret = GetSavableObjectInternal<CannonData> ();
		ret.m_baseWeaponDamage = m_baseWeaponDamage;
		ret.m_baseWeaponFireRate = m_baseWeaponFireRate;
		return ret;
	}

	public void LoadFrom (CannonData data)
	{
		LoadFromInternal (data);
		m_baseWeaponDamage = data.m_baseWeaponDamage;
		m_baseWeaponFireRate = data.m_baseWeaponFireRate;
	}

	#endregion
}
