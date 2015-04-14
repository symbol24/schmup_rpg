﻿using UnityEngine;
using System.Collections;

public class HPGUIBar : SpriteScaleBarBase
{
    private IHPController _hpController;
    public void Init(IHPController hpcontroller)
    {
        Init();
        if (_hpController != null)
        {
            _hpController.ValuesChanged -= hpcontroller_ValuesChanged;
            _hpController.Died -= hpcontroller_Died;
        }
        _hpController = hpcontroller;
        hpcontroller.ValuesChanged += hpcontroller_ValuesChanged;
        hpcontroller.Died += hpcontroller_Died;
    }

    void hpcontroller_Died(object sender, DeathReasonEventArgs e)
    {
        UpdateBar(0, _hpController.PlayerStats.MaxHP);
    }

    void hpcontroller_ValuesChanged(object sender, System.EventArgs e)
    {
        UpdateBar(_hpController.CurrentHP, _hpController.PlayerStats.MaxHP);
    }
}
