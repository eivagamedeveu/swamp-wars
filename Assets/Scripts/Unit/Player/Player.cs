using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private HealthBar _healthBar;

    protected override void Start()
    {
        base.Start();
        
        _healthBar.Init(_health, _health);
    }
}
