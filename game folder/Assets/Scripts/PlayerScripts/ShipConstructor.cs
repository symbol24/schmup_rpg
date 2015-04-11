using UnityEngine;
using System.Collections;

public class ShipConstructor : MonoBehaviour {
	private PlayerContainer m_playerContainer;
	private PlayerController m_playerController;
	private PrefabContainer m_prefabs;
	private GameManager m_manager;
	private CannonController[] m_cannons = new CannonController[2];
	private EquipmentController[] m_otherEquipment = new EquipmentController[4];
	private ShieldController m_shieldController;

	// Use this for initialization
	void Start () {
		m_playerContainer = FindObjectOfType(typeof(PlayerContainer)) as PlayerContainer;
		m_prefabs = FindObjectOfType(typeof(PrefabContainer)) as PrefabContainer;
		m_manager = FindObjectOfType (typeof(GameManager)) as GameManager;
		m_playerController = SetPlayerController ();
//		m_manager.SetPlayerShip (m_playerController);
		m_playerController.m_energyBar.SetPlayerShip (m_playerController);
		m_playerController.m_HPBar.SetPlayerShip (m_playerController);
		m_playerController.m_shieldBar.SetPlayerShip (m_playerController);
		m_playerController.m_shieldBar.SetShieldController (m_shieldController);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	PlayerController SetPlayerController ()
	{
		PlayerController ret = Instantiate (m_prefabs.GetPlayerController(), transform.position, transform.rotation) as PlayerController;
		ret.SetCannons(GetInstancedCannons(m_playerContainer));
		ret.SetOtherEquipmenet (GetInstancedEquipment (m_playerContainer));
		ret.SetShield (m_shieldController);
		ret.Init ();
		return ret;
	}

	private CannonController[] GetInstancedCannons( PlayerContainer container){
		CannonController[] ret = new CannonController[2];
		for (int i = 0; i < ret.Length; i++) {
			string name = container.m_Cannons[i].m_prefabName;
			ret[i] = Instantiate(m_prefabs.GetEquipmentPerName(name), transform.position, transform.rotation) as CannonController;
			ret[i].LoadFrom(container.m_Cannons[i]);
		}

		return ret;
	}

	private EquipmentController[] GetInstancedEquipment(PlayerContainer container){
		EquipmentController[] ret = new EquipmentController[4];
		int x = 0;
		string name;
		for (int i = 0; i < container.m_OtherEquipment.Length; i++) {
			name = container.m_OtherEquipment [i].m_prefabName;
			ret[i] = Instantiate (m_prefabs.GetEquipmentPerName (name), transform.position, transform.rotation) as EquipmentController;
			ret[i].LoadFromInternal(container.m_OtherEquipment [i]);
			x = i;
		}
		x += 1;
		name = container.m_Shield.m_prefabName;
		ShieldController sTemp = Instantiate (m_prefabs.GetEquipmentPerName (name), transform.position, transform.rotation) as ShieldController;
		sTemp.LoadFrom (container.m_Shield);
		ret [x] = sTemp;

		m_shieldController = sTemp;
		return ret;
	}

}
