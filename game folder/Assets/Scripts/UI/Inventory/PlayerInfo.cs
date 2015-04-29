using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    }

    public void UpdateEquipmentNames(PlayerController player)
    {
        CannonController[] cannons = player.GetCannons();
        EquipmentController[] equip = player.GetOtherEquipment();

        Text temp = GameObject.Find("cannonTxt1").GetComponent<Text>();
        temp.text = cannons[0].m_equipmentName;

        temp = GameObject.Find("cannonTxt2").GetComponent<Text>();
        temp.text = cannons[1].m_equipmentName;

        temp = GameObject.Find("chassisTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.chassis);

        temp = GameObject.Find("engineTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.engine);

        temp = GameObject.Find("hullTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.hull);

        temp = GameObject.Find("shieldTxt").GetComponent<Text>();
        temp.text = GetEquipmentName(equip, EquipmentController.equipmentType.shield);

    }

    private string GetEquipmentName(EquipmentController[] equips, EquipmentController.equipmentType type){
        string ret = "";

        foreach (EquipmentController e in equips){
            if (e.m_myType == type)
                ret = e.m_equipmentName;
        }
        //print(ret);
        return ret;
    }
}
