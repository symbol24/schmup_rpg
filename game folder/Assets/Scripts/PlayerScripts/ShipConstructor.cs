using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipConstructor : MonoBehaviour {
	private PlayerController m_playerController;
	private PrefabContainer m_prefabs;
	private GameManager m_manager;

	// Use this for initialization
	void Start () {
        if(FindObjectOfType<PlayerContainer>() == null){
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
        ret.m_experience = PlayerContainer.instance.M_experience;
        ret.m_credits = PlayerContainer.instance.M_credits;
        ret.m_level = PlayerContainer.instance.M_level;
        ret.SetCannons(GetInstancedCannons());
        ret.SetOtherEquipmenet(GetInstancedEquipment());
        ret.SetShield(GetInstantiatedShield());
        ret.SetChassis(GetInstancedChassis());
		ret.Init ();
		return ret;
	}

	private CannonController[] GetInstancedCannons(){
		CannonController[] ret = new CannonController[2];
		for (int i = 0; i < ret.Length; i++) {
            string name = PlayerContainer.instance.M_Cannons[i].m_prefabName;
			ret[i] = Instantiate(m_prefabs.GetEquipmentPerName(name), transform.position, transform.rotation) as CannonController;
            ret[i].LoadFrom(PlayerContainer.instance.M_Cannons[i]);
		}

		return ret;
	}

    private ChassisController GetInstancedChassis()
    {
        ChassisController ret;
        string name = PlayerContainer.instance.M_chassis.m_prefabName;
        ret = Instantiate(m_prefabs.GetEquipmentPerName(name), transform.position, transform.rotation) as ChassisController;
        ret.LoadFrom(PlayerContainer.instance.M_chassis);
        return ret;
    }

    private ShieldController GetInstantiatedShield()
    {
        string name = PlayerContainer.instance.M_Shield.m_prefabName;
        ShieldController sTemp = Instantiate(m_prefabs.GetEquipmentPerName(name), transform.position, transform.rotation) as ShieldController;
        sTemp.LoadFrom(PlayerContainer.instance.M_Shield);

        return sTemp;
    }

	private EquipmentController[] GetInstancedEquipment(){
        EquipmentController[] ret = new EquipmentController[PlayerContainer.instance.M_OtherEquipment.Length];
		int x = 0;
		string name;
        for (int i = 0; i < PlayerContainer.instance.M_OtherEquipment.Length; i++)
        {
            name = PlayerContainer.instance.M_OtherEquipment[i].m_prefabName;
			ret[i] = Instantiate (m_prefabs.GetEquipmentPerName (name), transform.position, transform.rotation) as EquipmentController;
            ret[i].LoadFromInternal(PlayerContainer.instance.M_OtherEquipment[i]);
			x = i;
		}
		
		return ret;
	}

    public void RebuildCannon(int id)
    {
        CannonController[] cannons = m_playerController.GetCannons();
        string name = PlayerContainer.instance.M_Cannons[id].m_prefabName;
        CannonController newCannon = Instantiate(PrefabContainer.instance.GetEquipmentPerName(name), m_playerController.transform.position, m_playerController.transform.rotation) as CannonController;
        newCannon.LoadFrom(PlayerContainer.instance.M_Cannons[id]);
        CannonController temp = cannons[id];
        cannons[id] = newCannon;
        Destroy(temp.gameObject);
        m_playerController.SetCannons(cannons);
        m_playerController.SetupCannons();
        m_playerController.UpdatePlayerInfo();
    }

    public void RebuildEquipment(EquipmentController.equipmentType type)
    {
        switch (type)
        {
            case EquipmentController.equipmentType.chassis:
                ChassisController current = m_playerController.GetChassis();
                string name = PlayerContainer.instance.M_chassis.m_prefabName;
                ChassisController prefab = (ChassisController)PrefabContainer.instance.GetEquipmentPerName(name);
                ChassisController newChassis = Instantiate(prefab, m_playerController.transform.position, m_playerController.transform.rotation) as ChassisController;
                newChassis.LoadFrom(PlayerContainer.instance.M_chassis);
                ChassisController temp = current;
                current = newChassis;
                Destroy(temp.gameObject);
                m_playerController.SetChassis(current);
                m_playerController.SetupEquipment();
                m_playerController.UpdateCannonRefs();
                break;
            case EquipmentController.equipmentType.shield:
                ShieldController cShield = m_playerController.GetShield();
                name = PlayerContainer.instance.M_Shield.m_prefabName;
                ShieldController preShield = (ShieldController)PrefabContainer.instance.GetEquipmentPerName(name);
                ShieldController newShield = Instantiate(preShield, m_playerController.transform.position, m_playerController.transform.rotation) as ShieldController;
                newShield.LoadFrom(PlayerContainer.instance.M_Shield);
                ShieldController tempShield = cShield;
                cShield = newShield;
                Destroy(tempShield.gameObject);
                m_playerController.SetShield(cShield);
                m_playerController.SetupEquipment();
                break;
            default:
                int id = m_playerController.GetPositionInOtherEquips(type);
                print("equipment id " + id);
                EquipmentController[] cEquip = m_playerController.GetOtherEquipment();
                EquipmentData toEquipData = PlayerContainer.instance.GetOneEquipment(type);
                name = toEquipData.m_prefabName;
                EquipmentController newPrefab = PrefabContainer.instance.GetEquipmentPerName(name);
                EquipmentController newEquip = Instantiate<EquipmentController>(newPrefab);
                newEquip.LoadFromInternal(toEquipData);
                EquipmentController tempE = cEquip[id];
                cEquip[id] = newEquip;
                Destroy(tempE.gameObject);
                m_playerController.SetOtherEquipmenet(cEquip);
                m_playerController.SetupEquipment();
                break;
        }
        m_playerController.UpdatePlayerInfo();
    }
}
