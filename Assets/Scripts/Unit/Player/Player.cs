using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private HealthBar _healthBar;

    private void Start()
    {
        _healthBar.Init(_health, _health);
    }
}
