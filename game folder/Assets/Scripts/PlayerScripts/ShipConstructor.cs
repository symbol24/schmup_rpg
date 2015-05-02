using UnityEngine;
using System.Collections;

public class ShipConstructor : MonoBehaviour {
	private PlayerContainer m_playerContainer;
	private PlayerController m_playerController;
	private PrefabContainer m_prefabs;
	private GameManager m_manager;
	private CannonController[] m_cannons = new CannonController[2];
	private EquipmentController[] m_otherEquipment = new EquipmentController[2];
	private ShieldController m_shieldController;

	// Use this for initialization
	void Start () {
		m_playerContainer = (PlayerContainer)PlayerContainer.instance;
        if(m_playerContainer == null){
            print("PlayerContainer missing!");
            Application.LoadLevel("loader");
        }

		m_prefabs = FindObjectOfType<PrefabContainer>();
		m_manager = FindObjectOfType<GameManager>();
		m_playerController = SetPlayerController ();
	}

	PlayerController SetPlayerController ()
	{
		PlayerController ret = Instantiate (m_prefabs.GetPlayerController(), transform.position, transform.rotation) as PlayerController;
		ret.m_experience = m_playerContainer.m_experience;
		ret.m_credits = m_playerContainer.m_credits;
		ret.m_level = m_playerContainer.m_level;
		ret.SetCannons(GetInstancedCannons(m_playerContainer));
		ret.SetOtherEquipmenet (GetInstancedEquipment (m_playerContainer));
        ret.SetShield(GetInstantiatedShield(m_playerContainer));
        ret.SetChassis(GetInstancedChassis(m_playerContainer));
        ret.SetInventory(m_playerContainer.m_inventory);
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

    private ChassisController GetInstancedChassis(PlayerContainer container)
    {
        ChassisController ret;
        string name = container.m_chassis.m_prefabName;
        ret = Instantiate(m_prefabs.GetEquipmentPerName(name), transform.position, transform.rotation) as ChassisController;
        ret.LoadFrom(container.m_chassis);
        return ret;
    }

    private ShieldController GetInstantiatedShield(PlayerContainer container)
    {
        string name = container.m_Shield.m_prefabName;
        ShieldController sTemp = Instantiate(m_prefabs.GetEquipmentPerName(name), transform.position, transform.rotation) as ShieldController;
        sTemp.LoadFrom(container.m_Shield);

        return sTemp;
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
		
		return ret;
	}

    public void RebuildCannon(int id)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        CannonController[] cannons = player.GetCannons();
        string name = PlayerContainer.instance.M_Cannons[id].m_prefabName;
        CannonController newCannon = Instantiate(PrefabContainer.instance.GetEquipmentPerName(name), player.transform.position, player.transform.rotation) as CannonController;
        newCannon.LoadFrom(PlayerContainer.instance.M_Cannons[id]);
        newCannon.Init(player);
        CannonController temp = cannons[id];
        cannons[id] = newCannon;
        Destroy(temp);
        player.SetCannons(cannons);
        player.SetupCannons();
    }
}
