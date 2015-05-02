using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentButton : MonoBehaviour {

    iPlayerContainer m_playerContainer;
    EquipmentData toDisplay;

    public void Init()
    {
        m_playerContainer = PlayerContainer.instance;

        toDisplay = Inventory.GetEquipment(gameObject, m_playerContainer);
        SetOnClick();
    }

    private void SetOnClick()
    {
        switch (toDisplay.m_myType)
        {
            case EquipmentController.equipmentType.cannon:
                gameObject.GetComponent<Button>().onClick.AddListener(CannonEquip);
                break;
            case EquipmentController.equipmentType.chassis:
                gameObject.GetComponent<Button>().onClick.AddListener(ButtonEquip);
                break;
            case EquipmentController.equipmentType.engine:
                gameObject.GetComponent<Button>().onClick.AddListener(ButtonEquip);
                break;
            case EquipmentController.equipmentType.hull:
                gameObject.GetComponent<Button>().onClick.AddListener(ButtonEquip);
                break;
            case EquipmentController.equipmentType.shield:
                gameObject.GetComponent<Button>().onClick.AddListener(ButtonEquip);
                break;
        }
        
    }

    public void UpdateInfo()
    {

        switch (toDisplay.m_myType)
        {
            case EquipmentController.equipmentType.cannon:
                Inventory.UpdateCannonInfo(toDisplay);
                break;
            case EquipmentController.equipmentType.chassis:
                Inventory.UpdateChassisInfo(toDisplay);
                break;
            case EquipmentController.equipmentType.engine:
                Inventory.UpdateEngineInfo(toDisplay);
                break;
            case EquipmentController.equipmentType.hull:
                Inventory.UpdateHullInfo(toDisplay);
                break;
            case EquipmentController.equipmentType.shield:
                Inventory.UpdateShieldInfo(toDisplay);
                break;
        }
    }

    public void ButtonEquip()
    {
        //Inventory.Equip();
    }

    public void CannonEquip()
    {
        MenuController menu = FindObjectOfType<MenuController>();
        menu.ConfirmScreen(toDisplay);
    }
}
