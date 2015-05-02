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
        EquipmentController.equipmentType type = toEquip.m_myType;
        EquipmentData old;

        int inventoryId = GetEquipmentID(toEquip.m_equipmentName);

        switch(type){
            case EquipmentController.equipmentType.chassis:
                old = PlayerContainer.instance.M_chassis;
                ChassisData newChassis = (ChassisData)ConvertEquipment(toEquip);
                PlayerContainer.instance.M_chassis = newChassis;
                break;
            case EquipmentController.equipmentType.shield:
                old = PlayerContainer.instance.M_Shield;
                ShieldData newShield = (ShieldData)ConvertEquipment(toEquip);
                PlayerContainer.instance.M_Shield = newShield;
                break;
            default:
                old = PlayerContainer.instance.GetOneEquipment(type);
                PlayerContainer.instance.SetOneEquipment(toEquip);
                break;
        }


        PlayerContainer.instance.M_inventory[inventoryId] = old;

        EquipOnPlayerController(type);
    }

    public static void CannonEquip(EquipmentData toEquip, int id)
    {

        CannonData temp = (CannonData)ConvertEquipment(toEquip);
        CannonData temp2 = PlayerContainer.instance.M_Cannons[id];
        int inventoryId = GetEquipmentID(toEquip.m_equipmentName);

        PlayerContainer.instance.M_Cannons[id] = temp;
        PlayerContainer.instance.M_inventory[inventoryId] = temp2;

        EquipCannonOnPlayerController(id);
    }

    private static void EquipCannonOnPlayerController(int pos)
    {
        ShipConstructor constructor = GameObject.FindObjectOfType<ShipConstructor>();
        if (constructor != null)
        {
            constructor.RebuildCannon(pos);
        }
    }

    private static void EquipOnPlayerController(EquipmentController.equipmentType toRebuild)
    {
        ShipConstructor constructor = GameObject.FindObjectOfType<ShipConstructor>();
        if (constructor != null)
        {
            constructor.RebuildCEquipment(toRebuild);
        }
    }


    private static EquipmentData ConvertEquipment(EquipmentData equip)
    {
        EquipmentData ret = new EquipmentData();

        switch (equip.m_myType)
        {
            case EquipmentController.equipmentType.cannon:
                if (equip.GetType() != typeof(CannonData) && equip.GetType() == typeof(EquipmentData))
                {
                    CannonData temp = new CannonData();
                    temp.LoadFromData(equip);
                    ret = temp;
                }
                else ret = equip;
                break;
            case EquipmentController.equipmentType.chassis:
                if (equip.GetType() != typeof(ChassisData) && equip.GetType() == typeof(EquipmentData))
                {
                    ChassisData temp = new ChassisData();
                    temp.LoadFromData(equip);
                    ret = temp;
                }
                else ret = equip;
                break;
            case EquipmentController.equipmentType.shield:
                if (equip.GetType() != typeof(ShieldData) && equip.GetType() == typeof(EquipmentData))
                {
                    ShieldData temp = new ShieldData();
                    temp.LoadFromData(equip);
                    ret = temp;
                }
                else ret = equip;
                break;
            default:
                ret = equip;
                break;
        }

        return ret;

    }
    
}
