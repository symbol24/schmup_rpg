using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentMenu : Menu {
    PlayerController m_playerController;
    [SerializeField] GameObject m_buttonPrefab;

    public void Init(MenuController.MenuType type, PlayerController player)
    {
        m_menuType = type;
        m_playerController = player;
        print("Buttons for " + m_menuType);
        CreateButtons();
    }

    private void CreateButtons() //can have 12 buttons before making height larger by 45
    {
        GameObject scRect = GameObject.Find("equipmentScroll");
        RectTransform scRectTransform = scRect.GetComponent<RectTransform>();
        EquipmentController.equipmentType equipType = GetType(m_menuType);
        int amount = CountEquipmentAmount(m_playerController.m_inventory, equipType);

        if(amount > 12) {
            float height = amount * 45;
            Vector2 newRect = new Vector2(scRectTransform.rect.width, height);
            scRectTransform.sizeDelta = newRect;
        }

        //DestroyChildren(scRect.transform);

        foreach (EquipmentData e in m_playerController.m_inventory)
        {
            if (e.m_myType == equipType)
            {
                GameObject button = Instantiate(m_buttonPrefab);
                button.name = e.m_equipmentName;
                RectTransform temp = button.GetComponent<RectTransform>();
                temp.SetParent(scRectTransform, false);
                Text txt = temp.transform.GetComponentInChildren<Text>();
                txt.text = e.m_equipmentName;
            }
        }

    }

    private void DestroyChildren(Transform fromThis)
    {
        GameObject[] list = fromThis.GetComponentsInChildren<GameObject>();

        if (list != null && list.Length > 0)
        {
            foreach (GameObject g in list) Destroy(g);
        }
    }

    private EquipmentController.equipmentType GetType(MenuController.MenuType type)
    {
        EquipmentController.equipmentType ret;

        switch (type)
        {
            case MenuController.MenuType.cannons:
                ret = EquipmentController.equipmentType.cannon;
                break;
            case MenuController.MenuType.chassis:
                ret = EquipmentController.equipmentType.chassis;
                break;
            case MenuController.MenuType.engine:
                ret = EquipmentController.equipmentType.engine;
                break;
            case MenuController.MenuType.hull:
                ret = EquipmentController.equipmentType.hull;
                break;
            case MenuController.MenuType.shield:
                ret = EquipmentController.equipmentType.shield;
                break;
            default:
                ret = EquipmentController.equipmentType.cannon;
                break;
        }

        return ret;
    }

    private int CountEquipmentAmount(EquipmentData[] list, EquipmentController.equipmentType type)
    {
        int count = 0;

        foreach (EquipmentData e in list)
        {
            if (e.m_myType == type) count++;
        }

        return count;

    }
}
