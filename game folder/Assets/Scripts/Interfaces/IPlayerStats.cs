using System;
using UnityEngine;
using System.Collections;

public interface IPlayerStats
{
    float Damage { get; }
    event EventHandler<floatEventArgs> DamageChanged;
    float FireRate { get; }
    event EventHandler<floatEventArgs> FireRateChanged;
    float MaxHP {get;}
    event EventHandler<floatEventArgs> MaxHPChanged;
    float Armor { get; }
    event EventHandler<floatEventArgs> ArmorChanged;
    float MouvementSpeed { get; }
    event EventHandler<floatEventArgs> MouvementSpeedChanged;
    float MaxEnergy { get; }
    event EventHandler<floatEventArgs> MaxEnergyChanged;
    float MaxShield { get; }
    event EventHandler<floatEventArgs> MaxShieldChanged;
    /// <summary>
    /// How long before the player recharges it shields
    /// </summary>
    float ShieldRechargeTime { get; }
    event EventHandler<floatEventArgs> ShieldRechargeTimeChanged;
    float ShieldRechargeDelay { get; }
    event EventHandler<floatEventArgs> ShieldRechargeDelayChanged;
}
