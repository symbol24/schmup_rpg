using UnityEngine;
using System.Collections;

public interface IShieldController 
{
    /// <summary>
    /// ColliderContainer to root the ontrigger event
    /// </summary>
    IDummyCollider Collider { get; }
    /// <summary>
    /// Type of the shield
    /// </summary>
    EnergyType EnergyType { get; }
    /// <summary>
    /// Bonus muliplier or "how" elemental your type is. This value varies from 0 to 1
    /// </summary>
    float BonusAtt { get; }
}
