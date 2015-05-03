using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public static class ItemGenerator
{
    public static CannonData Cannon(float level)
    {
        ProjectileController bullet = PrefabContainer.instance.GetRandomProjective();
        EquipmentData baseData = BaseInfo(level);
        return new CannonData()
        {
            m_prefabName = PrefabContainer.instance.GetRandomEquipement(EquipmentController.equipmentType.cannon).name,
            m_creditValue = baseData.m_creditValue,
            m_Owner = baseData.m_Owner,
            m_equipmentLevel = baseData.m_equipmentLevel,
            m_myType = EquipmentController.equipmentType.cannon,
            m_equipmentName = "Cannon " + Random.Range(level, (level *level)),
            m_baseDamage = StatCalculator.CalculateBaseDamage(level),
            m_baseFireRate = StatCalculator.CalculateBaseFireRate(level),
            m_damageType = bullet.m_damageType,
            m_bulletPrefabName = bullet.name,
        };
    }

    public static ChassisData Chassis(float level, float damageModifier = float.MinValue, float fireRateModifier = float.MinValue, float armorModifier = float.MinValue, float shieldModifier = float.MinValue)
    {
        if (Math.Abs(damageModifier - float.MinValue) < 0.1) damageModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(fireRateModifier - float.MinValue) < 0.1) fireRateModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(armorModifier - float.MinValue) < 0.1) armorModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(shieldModifier - float.MinValue) < 0.1) shieldModifier = Random.Range(0f, 20f)/100f;
        ChassisController chassis = (ChassisController)PrefabContainer.instance.GetRandomEquipement(EquipmentController.equipmentType.chassis);
        var chassisSize = chassis.m_chassisSize;
        EquipmentData baseData = BaseInfo(level);
        return new ChassisData()
        {
            m_prefabName = PrefabContainer.instance.GetRandomEquipement(EquipmentController.equipmentType.chassis).name,
            m_equipmentName = "Chassis " + Random.Range(level, (level * level)),
            m_creditValue = baseData.m_creditValue,
            m_Owner = baseData.m_Owner,
            m_equipmentLevel = baseData.m_equipmentLevel,
            m_myType = EquipmentController.equipmentType.chassis,
            m_baseHealth = StatCalculator.CalculateBaseHP(level),
            m_baseSpeed = StatCalculator.CalculateBaseSpeed(level,chassisSize),
            m_chassisSize = chassisSize,
            m_modifierDamage = damageModifier,
            m_modifierFireRate = fireRateModifier,
            m_modifierArmour = armorModifier,
            m_modifierShield = shieldModifier,
        };
    }

    public static ShieldData Shield(float level, float energyModifier = float.MinValue)
    {
        if (Math.Abs(energyModifier - float.MinValue) < 0.1) energyModifier = Random.Range(0f, 20f)/100f;

        EquipmentData baseData = BaseInfo(level);
        return new ShieldData()
        {
            m_prefabName = PrefabContainer.instance.GetRandomEquipement(EquipmentController.equipmentType.shield).name,
            m_equipmentName = "Shield " + Random.Range(level, (level * level)),
            m_creditValue = baseData.m_creditValue,
            m_Owner = baseData.m_Owner,
            m_equipmentLevel = baseData.m_equipmentLevel,
            m_myType = EquipmentController.equipmentType.shield,
            m_baseShield = StatCalculator.CalculateBaseShield(level),
            m_modifierEnergy = energyModifier,
            m_damageType = StatCalculator.GetRandomValue<EnergyType>(),
        };
    }

    public static EquipmentData Hull(float level, float healthModifier = float.MinValue)
    {
        if (Math.Abs(healthModifier - float.MinValue) < 0.1) healthModifier = Random.Range(0f, 20f) / 100f;
        EquipmentData baseData = BaseInfo(level);
        return new EquipmentData()
        {
            m_prefabName = PrefabContainer.instance.GetRandomEquipement(EquipmentController.equipmentType.hull).name,
            m_equipmentName = "Hull " + Random.Range(level, (level * level)),
            m_creditValue = baseData.m_creditValue,
            m_Owner = baseData.m_Owner,
            m_equipmentLevel = baseData.m_equipmentLevel,
            m_myType = EquipmentController.equipmentType.hull,
            m_baseArmour = StatCalculator.CalculateBaseArmor(level),
            m_modifierHealth = healthModifier,
        };
    }

    public static EquipmentData Engine(float level, float speedModifier = float.MinValue, float shieldModifier = float.MinValue)
    {
        if (Math.Abs(speedModifier - float.MinValue) < 0.1) speedModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(shieldModifier - float.MinValue) < 0.1) shieldModifier = Random.Range(0f, 20f)/100f;
        EquipmentData baseData = BaseInfo(level);
        return new EquipmentData
        {
            m_prefabName = PrefabContainer.instance.GetRandomEquipement(EquipmentController.equipmentType.engine).name,
            m_equipmentName = "Engine " + Random.Range(level, (level * level)),
            m_creditValue = baseData.m_creditValue,
            m_Owner = baseData.m_Owner,
            m_equipmentLevel = baseData.m_equipmentLevel,
            m_myType = EquipmentController.equipmentType.hull,
            m_baseEnergy = StatCalculator.CalculateBaseEnergy(level),
            m_modifierSpeed = speedModifier,
            m_modifierShield = shieldModifier,
        };
    }

    public static EquipmentData BaseInfo(float level)
    {
        return new EquipmentData()
        {
            m_creditValue = StatCalculator.GetCreditValue(level),
            m_Owner = "player",
            m_equipmentLevel = level,

        };
    }
    
}
