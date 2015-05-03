using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {
    public void UpdateStats(PlayerController player){
        Text temp = GameObject.Find("dmgValue").GetComponent<Text>();
        temp.text = player.m_playerDamage.ToString();

        temp = GameObject.Find("frValue").GetComponent<Text>();
        temp.text = player.m_playerFireRate.ToString();

        temp = GameObject.Find("hpValue").GetComponent<Text>();
        temp.text = player.m_maxPlayerHP.ToString();

        temp = GameObject.Find("armorValue").GetComponent<Text>();
        temp.text = player.m_playerArmor.ToString();

        temp = GameObject.Find("speedValue").GetComponent<Text>();
        temp.text = player.m_playerMouvementSpeed.ToString();

        temp = GameObject.Find("energyValue").GetComponent<Text>();
        temp.text = player.m_maxPlayerEnergy.ToString();

        UpdateLevel(player.m_level);

        temp = GameObject.Find("creditValue").GetComponent<Text>();
        temp.text = player.m_credits.ToString();
    }

    public void UpdateLevel(float lvl)
    {
        Text temp = GameObject.Find("levelValue").GetComponent<Text>();
        temp.text = lvl.ToString();
    }

    public void UpdateEquipmentNames(List<EquipmentController> equipList)
    {
        Text temp = GameObject.Find("cannonTxt1").GetComponent<Text>();
        temp.text = equipList[0].m_equipmentName;

        temp = GameObject.Find("cannonTxt2").GetComponent<Text>();
        temp.text = equipList[1].m_equipmentName;

        temp = GameObject.Find("chassisTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equipList, EquipmentController.equipmentType.chassis);

        temp = GameObject.Find("hullTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equipList, EquipmentController.equipmentType.hull);

        temp = GameObject.Find("engineTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equipList, EquipmentController.equipmentType.engine);

        temp = GameObject.Find("shieldTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equipList, EquipmentController.equipmentType.shield);

    }

    private string GetEquipmentName(List<EquipmentController> equips, EquipmentController.equipmentType type){
        string ret = "";

        foreach (EquipmentController e in equips){
            if (e.m_myType == type)
                ret = e.m_equipmentName;
        }
        print(ret);
        return ret;
    }
}
