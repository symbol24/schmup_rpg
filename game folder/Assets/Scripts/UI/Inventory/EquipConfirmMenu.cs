using UnityEngine;
using System.Collections;

public class EquipConfirmMenu : Menu {

    EquipmentData m_toEquip;
    GameManager m_manager;
    MenuController m_controller;
    GameObject m_button;

    public void Init(EquipmentData toEquip, GameObject button)
    {
        m_toEquip = toEquip;
        m_manager = FindObjectOfType<GameManager>();
        m_controller = FindObjectOfType<MenuController>();
        m_button = button;
    }

    void Update()
    {
        if (m_manager.m_altFireButton > 0 && m_manager.m_CurrentState != GameManager.gameState.playing)
        {
            Close();
        }
    }

    public void CannonEquip(int cannonId){
        Inventory.CannonEquip(m_toEquip, cannonId);
        Close();
        m_controller.SwitchMenu(m_controller.GetCurrentMenuTypeInt());
    }

    public void DefaultEquip()
    {
        Close();
    }

    private void Close()
    {
        m_controller.CloseConfirmScreens();
        m_controller.SetSelectedButton(m_button);
    }

}
