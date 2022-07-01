using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : State
{
    [SerializeField] private float _force;
    
    private Rigidbody2D _rigidbody;

    public override void Init(Unit unit)
    {
        base.Init(unit);
        
        RequiredStamina = LocalDataManager.Instance.Config.RollStaminaConsume;
    }

    public override void Enter()
    {
        base.Enter();
        
        _rigidbody = Unit.GetComponent<Rigidbody2D>();
        
        _rigidbody.AddForce(new Vector2(Unit.LookDirection, 0f) * _force, ForceMode2D.Impulse);

        Unit.UnitStamina.Spend(RequiredStamina);
        
        Unit.UnitAnimationsHandler.IsAnimationCompleted += Exit;
    }

    public override void UpdateHandle()
    {
        
    }

    public override void Exit()
    {
        _rigidbody.velocity = Vector2.zero;
        Unit.UnitAnimationsHandler.IsAnimationCompleted -= Exit;
        
        base.Exit();
    }
}
