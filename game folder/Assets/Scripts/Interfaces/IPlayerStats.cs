using UnityEngine;
using System.Collections;

public interface IPlayerStats
{
    float Damage { get; }
    float FireRate { get; }
    float MaxHP {get;}
    float Armor { get; }
    float MouvementSpeed { get; }
    float MaxEnergy { get; }
    float MaxShield { get; }
    /// <summary>
    /// How long before the player recharges it shields
    /// </summary>
    float ShieldRechargeTime { get; }
}
