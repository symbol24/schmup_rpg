using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class EquipmentData {
	public EquipmentController.equipmentType m_myType;

    public string m_equipmentName = "";
	public string m_prefabName = "";
	public int m_equipmentLevel = 1;
	public float m_creditValue = 1.0f;
	public EnergyType m_damageType = EnergyType.proton;
	public string m_Owner = "player";
	
	public float[] m_baseValues = new float[7];
    public float m_baseDamage { get { return m_baseValues[0]; } set { m_baseValues[0] = value; } }
    public float m_baseFireRate { get { return m_baseValues[1]; } set { m_baseValues[1] = value; } }
    public float m_baseHealth { get { return m_baseValues[2]; } set { m_baseValues[2] = value; } }
    public float m_baseArmour { get { return m_baseValues[3]; } set { m_baseValues[3] = value; } }
    public float m_baseSpeed { get { return m_baseValues[4]; } set { m_baseValues[4] = value; } }
    public float m_baseEnergy { get { return m_baseValues[5]; } set { m_baseValues[5] = value; } }
    public float m_baseShield { get { return m_baseValues[6]; } set { m_baseValues[6] = value; } }

	public float[] m_ValueModifiers = new float[7];
    public float m_modifierDamage {get { return m_ValueModifiers[0]; } set { m_ValueModifiers[0] = value; }}

    public float m_modifierFireRateModifier { get { return m_ValueModifiers[1]; } set { m_ValueModifiers[1] = value; } }
    public float m_modifierSpeedModifier { get { return m_ValueModifiers[2]; } set { m_ValueModifiers[2] = value; } }
    public float m_modifierArmourModifier { get { return m_ValueModifiers[3]; } set { m_ValueModifiers[3] = value; } }
    public float m_modifierEnergyModifier { get { return m_ValueModifiers[4]; } set { m_ValueModifiers[4] = value; } }
    public float m_modifierShieldModifier { get { return m_ValueModifiers[5]; } set { m_ValueModifiers[5] = value; } }
    public float m_modifierHealthModifier { get { return m_ValueModifiers[6]; } set { m_ValueModifiers[6] = value; } }

    public float m_modifierFireRate { get { return m_ValueModifiers[1]; } set { m_ValueModifiers[1] = value; } }
    public float m_modifierSpeed { get { return m_ValueModifiers[2]; } set { m_ValueModifiers[2] = value; } }
    public float m_modifierArmour { get { return m_ValueModifiers[3]; } set { m_ValueModifiers[3] = value; } }
    public float m_modifierEnergy { get { return m_ValueModifiers[4]; } set { m_ValueModifiers[4] = value; } }
    public float m_modifierShield { get { return m_ValueModifiers[5]; } set { m_ValueModifiers[5] = value; } }
    public float m_modifierHealth { get { return m_ValueModifiers[6]; } set { m_ValueModifiers[6] = value; } }

}
