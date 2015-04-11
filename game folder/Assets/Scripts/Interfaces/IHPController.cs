using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Meant to manage hits. How they are handled and others
/// </summary>
public interface IHPController
{
    /// <summary>
    /// Initialization of the HPController. A HP Controller is not capable of working without its initialization being
    /// executed
    /// </summary>
    /// <param name="playerStats"></param>
    /// <param name="shieldController"></param>
    /// <param name="chassisCollider"></param>
    void Init(IPlayerStats playerStats, IShieldController shieldController, DummyCollider chassisCollider);
    /// <summary>
    /// PlayerStats received in the init
    /// </summary>
    IPlayerStats PlayerStats { get; }
    /// <summary>
    /// Hit Method, It can trigger Died or Values Changed Event
    /// </summary>
    /// <param name="projectile">Projectile to be handle</param>
    bool TryHit(IProjectileController projectile);
    /// <summary>
    /// Whether the player has an HP Below zero or is dead
    /// </summary>
    bool IsHPBelowZero { get; }
    float CurrentHP { get; }
    float CurrentShield { get; }
    /// <summary>
    /// Event fired when player dies. When the player dies, ValuesChanged is not fired
    /// </summary>
    event EventHandler<DeathReasonEventArgs> Died;
    /// <summary>
    /// Event fired when player regenerates/loses HP or Shield
    /// </summary>
    event EventHandler ValuesChanged;

}
