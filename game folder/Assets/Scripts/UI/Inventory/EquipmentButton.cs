using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentButton : MonoBehaviour {

    PlayerController m_playerController;
    EquipmentController.equipmentType myType;
    EquipmentData toDisplay;

    public void Init()
    {
        m_playerController = FindObjectOfType<PlayerController>();
        myType = m_playerController.m_inventory[GetEquipmentID()].m_myType;
        toDisplay = GetEquipment();
        SetOnClick();
    }

    private void UpdateCannonInfo()
    {
        Text txt = GameObject.Find("cannonInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("cannonEnergytype").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_damageType.ToString();

        txt = GameObject.Find("cannonDmg").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[0].ToString();

        txt = GameObject.Find("cannonFR").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[1].ToString();

        txt = GameObject.Find("cannonAlt").GetComponentInChildren<Text>();
        txt.text = "Not implemented yet";

        txt = GameObject.Find("cannonEU").GetComponentInChildren<Text>();
        txt.text = "Not implemented yey";
    }

    private void UpdateChassisInfo()
    {
        Text txt = GameObject.Find("chassisInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("chassisHealth").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[2].ToString();

        txt = GameObject.Find("chassisSpeed").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[4].ToString();

        //needs sprite modif
    }

    private void UpdateHullInfo()
    {
        Text txt = GameObject.Find("hullInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("hullArmor").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[3].ToString();
    }

    private void UpdateEngineInfo()
    {
        Text txt = GameObject.Find("engineInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("engineEnergy").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[5].ToString();
    }

    private void UpdateShieldInfo()
    {
        Text txt = GameObject.Find("shieldInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("shieldType").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_damageType.ToString();

        txt = GameObject.Find("shieldHealth").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[6].ToString();

        //need sprite for shield as well
    }

    private EquipmentData GetEquipment()
    {
        int index = GetEquipmentID();
        return m_playerController.m_inventory[index];
    }

    private int GetEquipmentID()
    {
        for (int i = 0; i < m_playerController.m_inventory.Count; i++)
        {
            if (this.name == m_playerController.m_inventory[i].m_equipmentName) return i;
        }

        return -1;
    }

    private void Equip()
    {
        print("to equip will come here");
    }

    private void SetOnClick()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Equip);
    }

    public void UpdateInfo()
    {

        switch (myType)
        {
            case EquipmentController.equipmentType.cannon:
                UpdateCannonInfo();
                break;
            case EquipmentController.equipmentType.chassis:
                UpdateChassisInfo();
                break;
            case EquipmentController.equipmentType.engine:
                UpdateEngineInfo();
                break;
            case EquipmentController.equipmentType.hull:
                UpdateHullInfo();
                break;
            case EquipmentController.equipmentType.shield:
                UpdateShieldInfo();
                break;
        }
    }
}
