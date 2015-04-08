using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerData {
	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;

	public EquipmentData m_cannon1;
	public EquipmentData m_cannon2;
	public EquipmentData m_usedChassis;
	public EquipmentData m_usedHull;
	public EquipmentData m_usedEngine;
	public EquipmentData m_usedShield;
}
