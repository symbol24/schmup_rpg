﻿using UnityEngine;
using System.Collections;

[SerializeField]
public class MissionData {
    public string m_missionCoordinates { get; set; }
    public MissionController.MissionType m_MissionType { get; set; }
    public float m_missionLevel { get; set; }
    public bool m_isMissionEmpty { get; set; }
    public EnemyData[] m_listOfMissionEnemies { get; set; }
    public EnemyData[] m_listofBosses { get; set; }
    public EquipmentData[] m_rewardEquipment { get; set; }
    public float m_scavangeTimer { get; set; }
    public float m_creditReward { get; set; }
    public MissionDifficulty m_difficulty { get; set; }
    public float m_experienceValue { get; set; }
    public int m_killsForBoss { get; set; }
}
