using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class Inventory {

    public static void UpdateCannonInfo(EquipmentData toDisplay)
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

        CannonData data = new CannonData();

        if (toDisplay.GetType() == typeof(CannonData)) data = (CannonData)toDisplay;

        string bulletname = "Player_Base_Bullet";
        if (data.m_bulletPrefabName != null) bulletname = data.m_bulletPrefabName;

        ProjectileController projectile = PrefabContainer.instance.GetBulletPerName(bulletname);
        Sprite bulletSprite = projectile.GetComponent<SpriteRenderer>().sprite;

        Image myImage = GameObject.Find("bulletSprite").GetComponent<Image>();
        myImage.sprite = bulletSprite;
    }

    public static void UpdateChassisInfo(EquipmentData toDisplay)
    {
        Text txt = GameObject.Find("chassisInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("chassisHealth").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[2].ToString();

        txt = GameObject.Find("chassisSpeed").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[4].ToString();

        ChassisData data = new ChassisData();

        if (toDisplay.GetType() == typeof(ChassisData)) data = (ChassisData)toDisplay;

        if (data.m_prefabName != null && data.m_prefabName != "")
        {
            ChassisController controller = (ChassisController)PrefabContainer.instance.GetEquipmentPerName(data.m_prefabName);
            Sprite[] sprites = controller.m_shipSprites;

            Image myImage = GameObject.Find("chassisSprite").GetComponent<Image>();
            myImage.sprite = sprites[1];
        }
    }

    public static void UpdateHullInfo(EquipmentData toDisplay)
    {
        Text txt = GameObject.Find("hullInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("hullArmor").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[3].ToString();
    }

    public static void UpdateEngineInfo(EquipmentData toDisplay)
    {
        Text txt = GameObject.Find("engineInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("engineEnergy").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[5].ToString();
    }

    public static void UpdateShieldInfo(EquipmentData toDisplay)
    {
        Text txt = GameObject.Find("shieldInfoTitle").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("shieldType").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_damageType.ToString();

        txt = GameObject.Find("shieldHealth").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_baseValues[6].ToString();

        //need sprite for shield as well
    }

    public static EquipmentData GetEquipment(string btnname)
    {
        int index = GetEquipmentID(btnname);
        return PlayerContainer.instance.M_inventory[index];
    }

    public static int GetEquipmentID(string name)
    {
        for (int i = 0; i < PlayerContainer.instance.M_inventory.Count; i++)
        {
            if (name == PlayerContainer.instance.M_inventory[i].m_equipmentName) return i;
        }

        return -1;
    }

    public static void Equip(EquipmentData toEquip)
    {
        
    }

    public static void CannonEquip(EquipmentData toEquip, int id)
    {

        CannonData temp = new CannonData();
        CannonData temp2 = PlayerContainer.instance.M_Cannons[id];
        int inventoryId = GetEquipmentID(toEquip.m_equipmentName);

        if (toEquip.GetType() != typeof(CannonData) && toEquip.GetType() == typeof(EquipmentData))
            temp.LoadFromData(toEquip);
        else if (toEquip.GetType() == typeof(CannonData))
            temp = (CannonData)toEquip;

        PlayerContainer.instance.M_Cannons[id] = temp;
        PlayerContainer.instance.M_inventory[inventoryId] = temp2;

        ShipConstructor constructor = GameObject.FindObjectOfType<ShipConstructor>();
        if (constructor != null)
        {
            constructor.RebuildCannon(id);
        }
    }

    
}
