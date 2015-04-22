﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStatusMenu : Menu {

    private string GetEquipmentName(EquipmentController[] equips, EquipmentController.equipmentType type)
    {
        string ret = "";

        foreach (EquipmentController e in equips)
        {
            if (e.m_myType == type)
                ret = e.m_equipmentName;
        }
        print(ret);
        return ret;
    }

    public void UpdateLargePlayerInfo(PlayerController player)
    {

        CannonController[] cannons = player.GetCannons();
        EquipmentController[] equip = player.GetOtherEquipment();

        Text temp = GameObject.Find("largeDmgValue").GetComponent<Text>();
        temp.text = player.m_playerDamage.ToString();
        temp = GameObject.Find("largeFrValue").GetComponent<Text>();
        temp.text = player.m_playerFireRate.ToString();
        temp = GameObject.Find("largeHpValue").GetComponent<Text>();
        temp.text = player.m_maxPlayerHP.ToString();
        temp = GameObject.Find("largeArmorValue").GetComponent<Text>();
        temp.text = player.m_playerArmor.ToString();
        temp = GameObject.Find("largeSpeedValue").GetComponent<Text>();
        temp.text = player.m_playerMouvementSpeed.ToString();
        temp = GameObject.Find("largeEnergyValue").GetComponent<Text>();
        temp.text = player.m_maxPlayerEnergy.ToString();
        temp = GameObject.Find("cannonTxt1 1").GetComponent<Text>();
        temp.text = cannons[0].m_equipmentName;
        temp = GameObject.Find("cannonTxt2 1").GetComponent<Text>();
        temp.text = cannons[1].m_equipmentName;
        temp = GameObject.Find("chassisTxt 1").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.chassis);
        temp = GameObject.Find("engineTxt 1").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.engine);
        temp = GameObject.Find("hullTxt 1").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.hull);
        temp = GameObject.Find("shieldTxt 1").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.shield);

    }
}
