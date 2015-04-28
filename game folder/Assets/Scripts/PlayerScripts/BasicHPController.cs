using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using System.Collections;

public class BasicHPController : MonoBehaviour , IHPController
{
    //Fields and Properties
    private IPlayerStats _playerStats;
    private IShieldController _shieldController;
    private IChassisController _chassisController;
    private IDummyCollider _shieldCollider { get { return _shieldController.Collider; } }
    private IDummyCollider _chassisCollider { get { return _chassisController.Collider; } }
    public TargetEnum[] _targetsToTakeDamageFrom = {TargetEnum.Anything, TargetEnum.Player,};
    public bool IsHPBelowZero { get { return CurrentHP <= 0; } }
    public float CurrentHP { get; private set; }
    private float _currentShield;
    public float CurrentShield
    {
        get { return _currentShield; }
        private set
        {
            _currentShield = value;
            bool shieldAboveZero = _currentShield > 0f;
            if (_shieldCollider.theCollider.enabled != shieldAboveZero)
            {
                _shieldCollider.theCollider.enabled = shieldAboveZero;
                foreach (var visualComponent in _shieldController.VisualComponents)
                {
                    visualComponent.enabled = shieldAboveZero;
                }
            }
        }
    }
    private float currentTimeToRechargeShield;
    public bool showDebugCalculations = false;
    public bool mitigateShieldOverflow = false;
    public bool invincibleWhileRecharging = true;
    public IPlayerStats PlayerStats { get { return _playerStats; } }

    //Public Methods
    public void Init(IPlayerStats playerStats, IShieldController shieldController, IChassisController chassisController)
    {
        _playerStats = playerStats;
        _shieldController = shieldController;
        _chassisController = chassisController;
        CurrentHP = _playerStats.MaxHP;
        CurrentShield = _playerStats.MaxShield;
        _chassisCollider.ColliderEntered += chassiCollider_ColliderEntered;
        _shieldCollider.ColliderEntered += ShieldColliderOnColliderEntered;
        var hpGUIBars = FindObjectsOfType<HPGUIBar>();
        foreach (var hpGuiBar in hpGUIBars)
        {
            hpGuiBar.Init(this);
        }
        var shieldGUIBars = FindObjectsOfType<ShieldGUIBar>();
        foreach (var shieldGuiBar in shieldGUIBars)
        {
            shieldGuiBar.Init(this);
        }
        var otherUIBars = FindObjectsOfType<RectTransWidthBar>();
        foreach (var rectTransWidthBar in otherUIBars)
        {
            rectTransWidthBar.Init(this);
        }

    }
    public bool TryHit(IProjectileController projectile)
    {
        bool ret = false;
        if (_targetsToTakeDamageFrom.Any(c => c == projectile.Target))
        {
            ret = true;
            if (_currentRechargingRoutine == null || !invincibleWhileRecharging)
            {
                ResetRechargeTimer();
                if (projectile.Damage != 0f && projectile.EnergyValue != 0f)
                {
                    if (projectile.EnergyValue > 0)
                    {
                        HandleElementalDamage(projectile);
                    }
                    HandleRegularDamage(projectile);
                    if (showDebugCalculations)
                    {
                        Debug.Log(string.Format("After being hit, CurrentShields: {0}, CurrentHP is: {1}", CurrentShield,
                            CurrentHP));
                    }
                    if (CurrentHP <= 0)
                    {
                        if (Died != null) Died(this, new DeathReasonEventArgs {});
                    }
                    else if (ValuesChanged != null) ValuesChanged(this, EventArgs.Empty);
                }
            }
            if(CurrentShield > 0) projectile.DestroyObjectAndBehaviors(false);
            else projectile.DestroyObjectAndBehaviors(true);
        }
        return ret; //Only explosions with the ship.
    }

    //Events
    public event EventHandler<DeathReasonEventArgs> Died;
    public event EventHandler ValuesChanged;
    public void Reset()
    {
        CurrentHP = _playerStats.MaxHP;
        CurrentShield = _playerStats.MaxShield;
    }

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

    //UpdateMethod
    void Update()
    {
        if (currentTimeToRechargeShield >= _playerStats.ShieldRechargeDelay && 
            CurrentShield < _playerStats.MaxShield && _currentRechargingRoutine == null &&
            !IsHPBelowZero)
        {
            _currentRechargingRoutine = StartRecharging();
            StartCoroutine(_currentRechargingRoutine);
        }
        currentTimeToRechargeShield += Time.deltaTime;
    }

    //Helpers
    private void HandleRegularDamage(IProjectileController projectile)
    {
        if (projectile.Damage >= CurrentShield)
        {

            var residue = projectile.Damage - CurrentShield;
            CurrentShield = 0;
            CurrentHP -= residue;
            if (showDebugCalculations)
            {
                Debug.Log(string.Format("Shield Overflow for normal damage. Residue for HP:{0}", residue));
            }
        }
        else
        {
            if (showDebugCalculations)
                Debug.Log(string.Format("Applying all damage to shield. Damage{0}", projectile.Damage));
            CurrentShield -= projectile.Damage;
        }
    }
    private void HandleElementalDamage(IProjectileController projectile)
    {
        var elementalNetDamage = GetElementalDamage(projectile);
        if (elementalNetDamage > CurrentShield)
        {
            if (showDebugCalculations) Debug.Log("Detected shield damage overflow");
            var residue = elementalNetDamage - CurrentShield;
            var nonElementalResidue = (residue*(projectile.BonusAtt + _shieldController.BonusAtt))/
                                      elementalNetDamage;
            CurrentShield = 0;
            if (showDebugCalculations)
            {
                Debug.Log(
                    string.Format(
                        "Elemental residue from elemental damage: {0}{1}Elemental corrected to nonElemental: {2}",
                        residue,
                        Environment.NewLine, nonElementalResidue));
            }
            if (!mitigateShieldOverflow)
            {
                if (showDebugCalculations) Debug.Log("Applying damageToHP");
                CurrentHP -= residue;
            }
        }
        else if (CurrentShield >= 0)
        {
            if (showDebugCalculations) Debug.Log("Detected enough shield for elemental damage");
            CurrentShield -= elementalNetDamage;
        }
        else
        {
            if(showDebugCalculations) Debug.Log("Detected no shield, damage going to HP");
            CurrentHP -= projectile.EnergyValue;
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
        if (showDebugCalculations)
        {
            Debug.Log(
                string.Format(
                    "ProjectileType is:{3}{4}ShieldType is:{5}Bonus% for Elemental is: {0}{1}Projectile Energy Value is:{2}",
                    bonusAtt, Environment.NewLine, ret, Enum.GetName(typeof (EnergyType), projectile.DamageType),
                    Environment.NewLine, Enum.GetName(typeof (EnergyType), _shieldController)));
        }
        return ret;
    }
    private void ResetRechargeTimer()
    {
        currentTimeToRechargeShield = 0f;
        if (_currentRechargingRoutine != null)
        {
            StopCoroutine(_currentRechargingRoutine);
            _currentRechargingRoutine = null;
        }
    }
    private IEnumerator _currentRechargingRoutine;
    private IEnumerator StartRecharging()
    {
        while (CurrentShield < _playerStats.MaxShield)
        {
            var increment = _playerStats.MaxShield/(_playerStats.ShieldRechargeTime/Time.deltaTime);
            CurrentShield = increment + CurrentShield >= _playerStats.MaxShield
                ? _playerStats.MaxShield
                : increment + CurrentShield;
            if (ValuesChanged != null) ValuesChanged(this, EventArgs.Empty);
            yield return null;
        }
        _currentRechargingRoutine = null;
    }

}
