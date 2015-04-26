using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentButton : MonoBehaviour {

    PlayerController m_playerController;
    EquipmentController.equipmentType myType;

    public void Init()
    {
        m_playerController = FindObjectOfType<PlayerController>();
        myType = m_playerController.m_inventory[GetEquipmentID()].m_myType;
        SetOnClick();
    }

    private void UpdateCannonInfo()
    {
        int index = GetEquipmentID();
        EquipmentData toDisplay = m_playerController.m_inventory[index];

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

    private int GetEquipmentID()
    {
        for (int i = 0; i < m_playerController.m_inventory.Length; i++)
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
                break;
            case EquipmentController.equipmentType.engine:
                break;
            case EquipmentController.equipmentType.hull:
                break;
            case EquipmentController.equipmentType.shield:
                break;
        }
    }
}
