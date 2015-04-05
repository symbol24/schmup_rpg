using UnityEngine;
using System.Collections;

public class ShieldController : EquipmentController {
	public string m_Owner = "player";
	public float m_shieldHealth = 10.0f;

	void Start(){

	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Target == m_Owner) {
			tempBullet.DestroyObjectAndBehaviors();

		}
	}
}
