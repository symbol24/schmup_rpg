using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

public interface IProjectileController 
{
    /// <summary>
    /// Raw damage that the projectile does
    /// </summary>
    float Damage { get; }
    /// <summary>
    /// Damage type that will play with other types
    /// </summary>
    EnergyType DamageType { get; }
    /// <summary>
    /// Elemental damage. This adds to the Damage
    /// Ex. Damage = 40, energyValue = 10 is a total of 50 damage where 10 are elemental damage
    /// </summary>
    int EnergyValue { get; }
    /// <summary>
    /// How elemental the elemental damage is. This value varies between 0 and 1
    /// </summary>
    float BonusAtt { get; }
    /// <summary>
    /// Who can take damage from this bullet
    /// </summary>
    TargetEnum Target { get; }
    /// <summary>
    /// Destroy bullet if it was used
    /// </summary>
    void DestroyObjectAndBehaviors();
}
