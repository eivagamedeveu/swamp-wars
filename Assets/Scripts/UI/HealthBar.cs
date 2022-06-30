using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : StatusBar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.IsHealthChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _player.IsHealthChanged -= ChangeValue;
    }
}
