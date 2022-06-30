using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitStateMachine))]
public class EnemyStateTransitions : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _attackDelay = 2f;

    private UnitStateMachine _unitStateMachine;
    private Vector2 _distanceToTarget;
    private float _timer;
    private Unit Unit => _unitStateMachine.Unit;

    private void Awake()
    {
        _unitStateMachine = GetComponent<UnitStateMachine>();
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        
        SetCurrentState();
    }
    
    private void SetCurrentState()
    {
        _distanceToTarget = Unit.transform.position - _target.transform.position;

        if (Math.Abs(_distanceToTarget.y) > 2 || Math.Abs(_distanceToTarget.x) > 2 + Unit.AttackZoneSize * 1.5)
        {
            _unitStateMachine.SetState(UnitState.Move);
        }
        else
        {
            if (_timer > 0) return;
            
            _unitStateMachine.SetState(UnitState.Attack);
            _timer = _attackDelay;
        }
    }
}
