using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : StatusBar
{
    [SerializeField] private UnitStamina _unitStamina;

    private void OnEnable()
    {
        _unitStamina.IsStaminaChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _unitStamina.IsStaminaChanged -= ChangeValue;
    }
}
