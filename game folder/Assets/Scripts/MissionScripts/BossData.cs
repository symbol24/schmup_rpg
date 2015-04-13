using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BossData {
	public string m_PrefabName;
	public PrefabContainer[] m_listofPrefabs;
	public int m_damageType;
	public int m_shieldType;
}
