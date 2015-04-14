using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class EnemyData {
	public string m_PrefabName;
	public int m_damageType;
	public float m_baseHP;
	public float m_baseDamage;
	public float m_baseArmour;
	public float m_spawnDelay;
	public int m_spawnCount;
	public float m_experienceValue;
}
