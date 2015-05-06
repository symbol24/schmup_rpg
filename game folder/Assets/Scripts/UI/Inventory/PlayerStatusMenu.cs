using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerStatusMenu : Menu {

    private string GetEquipmentName(IEnumerable<EquipmentController> equips, EquipmentController.equipmentType type)
    {
        string ret = "";

        foreach (EquipmentController e in equips)
        {
            if (e.m_myType == type)
                ret = e.m_equipmentName;
        }
        //print(ret);
        return ret;
    }

    public void UpdateLargePlayerInfo(PlayerController player)
    {
        
        Text temp = GameObject.Find("largeDmgValue").GetComponent<Text>();
        List<EquipmentController> allEquips;
        if (player != null)
        {
            allEquips = player.GetAllEquips();

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

        }


        temp = GameObject.Find("largeSizeValue").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_chassis.m_chassisSize.ToString();

        temp = GameObject.Find("cannonTxt1 1").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_Cannons[0].m_equipmentName;
        temp = GameObject.Find("cannonTxt2 1").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_Cannons[1].m_equipmentName;
        temp = GameObject.Find("chassisTxt 1").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_chassis.m_equipmentName;
        temp = GameObject.Find("engineTxt 1").GetComponent<Text>();
        temp.text = PlayerContainer.instance.GetOneEquipment(EquipmentController.equipmentType.engine).m_equipmentName;
        temp = GameObject.Find("hullTxt 1").GetComponent<Text>();
        temp.text = PlayerContainer.instance.GetOneEquipment(EquipmentController.equipmentType.hull).m_equipmentName;
        temp = GameObject.Find("shieldTxt 1").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_Shield.m_equipmentName;

        temp = GameObject.Find("largeLvlValue").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_level.ToString();
        temp = GameObject.Find("largeExpValue").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_experience.ToString();
        temp = GameObject.Find("largeCreditValue").GetComponent<Text>();
        temp.text = PlayerContainer.instance.M_credits.ToString();
    }
}
