using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class BasicHPController : MonoBehaviour , IHPController
{
    //Fields and Properties
    private IPlayerStats _playerStats;
    private IShieldController _shieldController;
    private DummyCollider _shieldCollider { get { return _shieldController.Collider; } }
    private DummyCollider _chassiCollider;
    public TargetEnum[] _targetsToTakeDamageFrom = {TargetEnum.Anything, TargetEnum.Player,};
    public bool IsHPBelowZero { get; private set; }
    public float CurrentHP { get; private set; }
    private float _currentShield;
    public float CurrentShield
    {
        get { return _currentShield; }
        private set
        {
            _currentShield = value;
            _shieldCollider.theCollider.enabled = _currentShield != 0f;
        }
    }

    //Public Methods
    public void Init(IPlayerStats playerStats, IShieldController shieldController, DummyCollider chassiCollider)
    {
        _playerStats = playerStats;
        _shieldController = shieldController;
        _chassiCollider = chassiCollider;
        CurrentHP = _playerStats.MaxHP;
        CurrentShield = _playerStats.MaxShield;
        chassiCollider.ColliderEntered += chassiCollider_ColliderEntered;
        _shieldCollider.ColliderEntered += ShieldColliderOnColliderEntered;
    }
    public bool TryHit(IProjectileController projectile)
    {
        bool ret = false;
        if (_targetsToTakeDamageFrom.Any(c => c == projectile.Target))
        {
            ret = true;
            if (projectile.EnergyValue > 0)
            {
                HandleElementalDamage(projectile);
            }
            HandleRegularDamage(projectile);
            if (CurrentHP <= 0)
            {
                if (Died != null) Died(this, new DeathReasonEventArgs {});
            }
            else if (ValuesChanged != null) ValuesChanged(this, EventArgs.Empty);
        }
        return ret;
    }
    
    //Events
    public event EventHandler<DeathReasonEventArgs> Died;
    public event EventHandler ValuesChanged;
    private void ShieldColliderOnColliderEntered(object sender, Collider2DEventArgs collider2DEventArgs)
    {
        var test = collider2DEventArgs.Collider.gameObject.GetComponent<IProjectileController>();
        if (test != null)
        {
            TryHit(test);
        }
    }
    private void chassiCollider_ColliderEntered(object sender, Collider2DEventArgs e)
    {
        var test = e.Collider.gameObject.GetComponent<IProjectileController>();
        if (test != null)
        {
            TryHit(test);
        }
    }

    //Helpers
    private void HandleRegularDamage(IProjectileController projectile)
    {
        if (projectile.Damage >= CurrentShield)
        {
            var residue = projectile.Damage - CurrentShield;
            CurrentShield = 0;
            CurrentHP -= residue;
        }
        else CurrentHP -= projectile.Damage;
    }
    private void HandleElementalDamage(IProjectileController projectile)
    {
        var elementalNetDamage = GetElementalDamage(projectile);
        if (elementalNetDamage > CurrentShield)
        {
            var residue = elementalNetDamage - CurrentShield;
            var nonElementalResidue = (residue * (projectile.BonusAtt + _shieldController.BonusAtt)) /
                                      elementalNetDamage;
            CurrentShield = 0;
            CurrentHP -= CurrentHP;
        }
        else
        {
            CurrentShield -= elementalNetDamage;
        }
    }
    private float GetElementalDamage(IProjectileController projectile)
    {
        float ret;
        var bonusAtt = projectile.BonusAtt + _shieldController.BonusAtt;
        if ((projectile.DamageType == EnergyType.proton && _shieldController.EnergyType == EnergyType.photon) ||
            (projectile.DamageType == EnergyType.photon && _shieldController.EnergyType == EnergyType.plasma) ||
            (projectile.DamageType == EnergyType.plasma && _shieldController.EnergyType == EnergyType.proton))
            bonusAtt = bonusAtt;
        else if ((projectile.DamageType == EnergyType.photon && _shieldController.EnergyType == EnergyType.proton) ||
            (projectile.DamageType == EnergyType.plasma && _shieldController.EnergyType == EnergyType.photon) ||
            (projectile.DamageType == EnergyType.proton && _shieldController.EnergyType == EnergyType.plasma))
            bonusAtt = -bonusAtt;
        ret = bonusAtt * projectile.EnergyValue;
        return ret;
    }

}
