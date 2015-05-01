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
        return new CannonData()
        {
            m_equipmentLevel = (int)level,
            m_baseDamage = StatCalculator.CalculateBaseDamage(level),
            m_baseFireRate = StatCalculator.CalculateBaseFireRate(level),
            m_damageType = StatCalculator.GetRandomValue<EnergyType>(),
        };
    }

    public static ChassisData Chassis(float level, float damageModifier = float.MinValue, float fireRateModifier = float.MinValue, float armorModifier = float.MinValue, float shieldModifier = float.MinValue)
    {
        if (Math.Abs(damageModifier - float.MinValue) < 0.1) damageModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(fireRateModifier - float.MinValue) < 0.1) fireRateModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(armorModifier - float.MinValue) < 0.1) armorModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(shieldModifier - float.MinValue) < 0.1) shieldModifier = Random.Range(0f, 20f)/100f;
        var chassisSize = StatCalculator.GetRandomValue<ChassisController.ChassisSize>();
        return new ChassisData()
        {
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
        return new ShieldData()
        {
            m_baseShield = StatCalculator.CalculateBaseShield(level),
            m_modifierEnergy = energyModifier,
            m_damageType = StatCalculator.GetRandomValue<EnergyType>(),
        };
    }

    public static EquipmentData Hull(float level, float healthModifier = float.MinValue)
    {
        if (Math.Abs(healthModifier - float.MinValue) < 0.1) healthModifier = Random.Range(0f, 20f)/100f;
        return new EquipmentData()
        {
            m_baseArmour = StatCalculator.CalculateBaseArmor(level),
            m_modifierHealth = healthModifier,
        };
    }

    public static EquipmentData Engine(float level, float speedModifier = float.MinValue, float shieldModifier = float.MinValue)
    {
        if (Math.Abs(speedModifier - float.MinValue) < 0.1) speedModifier = Random.Range(0f, 20f)/100f;
        if (Math.Abs(shieldModifier - float.MinValue) < 0.1) shieldModifier = Random.Range(0f, 20f)/100f;
        Debug.Log("need to take a look at m_baseEnergy random value");
        return new EquipmentData
        {
            m_baseEnergy = level*5, //Need to take a look at it
            m_modifierSpeed = speedModifier,
            m_modifierShield = shieldModifier,
        };
    }
    
}
