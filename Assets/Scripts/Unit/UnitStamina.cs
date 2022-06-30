using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitStamina : MonoBehaviour
{
    [SerializeField] private StaminaBar _staminaBar;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _regenerationPerSecond;

    private float _currentValue;
    private float _timer = 0;

    public float RegenerationSpeed => _regenerationPerSecond * Time.deltaTime;
    public event UnityAction<float> IsStaminaChanged;

    private void Start()
    {
        if(_staminaBar != null)
            _staminaBar.Init(_maxValue, _maxValue);
        
        _currentValue = _maxValue;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            Regenerate();
        }
    }

    private void Regenerate()
    {
        if(Math.Abs(_currentValue - _maxValue) == 0) return;

        if (_currentValue + RegenerationSpeed >= _maxValue)
        {
            _currentValue = _maxValue;
        }
        else
        {
            _currentValue += RegenerationSpeed;
        }
        
        IsStaminaChanged?.Invoke(_currentValue);
    }
    
    public bool Spend(float value)
    {
        if (_currentValue < value) return false;
        
        _currentValue -= value;
        
        IsStaminaChanged?.Invoke((int)_currentValue);

        _timer = LocalDataManager.Instance.Config.StaminaStunlock;

        return true;
    }

    public bool Check(float value)
    {
        return _currentValue >= value;
    }
}
