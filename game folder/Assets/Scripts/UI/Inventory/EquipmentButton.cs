using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentButton : MonoBehaviour {
    EquipmentData toDisplay;
    MenuController menu;

    public void Init()
    {
        menu = FindObjectOfType<MenuController>();
        toDisplay = Inventory.GetEquipment(gameObject.name);
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
                gameObject.GetComponent<Button>().onClick.AddListener(OtherEquip);
                break;
            case EquipmentController.equipmentType.engine:
                gameObject.GetComponent<Button>().onClick.AddListener(OtherEquip);
                break;
            case EquipmentController.equipmentType.hull:
                gameObject.GetComponent<Button>().onClick.AddListener(OtherEquip);
                break;
            case EquipmentController.equipmentType.shield:
                gameObject.GetComponent<Button>().onClick.AddListener(OtherEquip);
                break;
        }
        
    }

    public void UpdateInfo()
    {
        menu.CloseConfirmScreens();

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

    public void OtherEquip()
    {
        menu.ConfirmScreen(toDisplay, gameObject);
    }

    public void CannonEquip()
    {
        menu.ConfirmScreen(toDisplay, gameObject);
    }
}
