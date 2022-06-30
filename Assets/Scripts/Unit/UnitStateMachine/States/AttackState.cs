using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private void DealDamage()
    {
        
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Unit.UnitAnimationsHandler.IsAnimationCompleted += Exit;
    }

    public override void UpdateHandle()
    {
        
    }

    public override void Exit()
    {
        Unit.UnitAnimationsHandler.IsAnimationCompleted -= Exit;
        Unit.Attack();

        base.Exit();
    }
}
