using UnityEngine;
using System.Collections;

public class PrefabContainer : MonoBehaviour {

	[SerializeField] private EquipmentController[] m_ListofEquipments;
	[SerializeField] private ProjectileController[] m_ListofBullets;
	[SerializeField] private PlayerController m_playerControllerPrefab;

	public EquipmentController GetEquipmentPerName(string name){
		foreach (EquipmentController e in m_ListofEquipments) {
			if(e.gameObject.name == name)
				return e;
		}
		print ("Equipment prefab " + name + " not found");
		return null;
	}

	public ProjectileController GetBulletPerName(string name){
		foreach (ProjectileController p in m_ListofBullets) {
			if(p.gameObject.name == name)
				return p;
		}
		print ("Bullet prefab " + name + " not found");
		return null;
	}

	public PlayerController GetPlayerController(){
		return m_playerControllerPrefab;
	}
}

