using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UnitAnimationsHandler))]
public abstract class Unit : MonoBehaviour, IDamageable, IAttacker
{
    [SerializeField] private UnitStateMachine _unitStateMachine;
    [SerializeField] private float _attackZoneSize;
    [SerializeField] protected int _health;
    [SerializeField] protected int _damage;
    
    private AttackZone _attackZone;
    private bool _isDied = false;

    public int LookDirection { get; private set; }
    public UnitAnimationsHandler UnitAnimationsHandler { get; protected set; }
    public UnitStamina UnitStamina { get; private set; }
    public UnitStateMachine UnitStateMachine => _unitStateMachine;
    public float AttackZoneSize => _attackZoneSize;
    public event UnityAction<float> IsHealthChanged;

    private void Awake()
    {
        _attackZone = GetComponentInChildren<AttackZone>();
        _attackZone.SetAttackZone(_attackZoneSize);
        UnitAnimationsHandler = GetComponent<UnitAnimationsHandler>();
        UnitStamina = GetComponent<UnitStamina>();
    }

    private void Die()
    {
        _isDied = true;
        UnitStateMachine.SetState(UnitState.Die);
    }
    
    public virtual void TakeDamage(int damage, float attackerYPosition)
    {
        if(_isDied) return;
        
        if(!UnitStateMachine.SetState(UnitState.TakeDamage)) return;
        
        var yDifference = transform.position.y - attackerYPosition;
        
        if(yDifference is > 2f or < -2f) return;
        
        float finalDamage = damage;
        
        if (UnitStateMachine.GetStateByName(UnitState.Block).IsEnable)
        {
            finalDamage *= LocalDataManager.Instance.Config.BlockPercent;

            UnitStamina.Spend(LocalDataManager.Instance.Config.BlockStaminaConsume);
        }
        
        _health -= (int)finalDamage;
        
        IsHealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Die();
        }
    }

    public virtual void Attack()
    {
        foreach (var target in _attackZone.Targets)
        {
            target.TakeDamage(_damage + Random.Range(-3, 3), transform.position.y);
        }
    }

    public void SetLookDirection(int direction)
    {
        LookDirection = direction;

        transform.rotation = Quaternion.Euler(0f, LookDirection > 0 ? 0f : 180f, 0f);
    }
}
