using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    public override void Enter()
    {
        base.Enter();
        
        Unit.UnitAnimationsHandler.SetAnimation(_stateAnimation,true);
        UnitStateMachine.IsBlockFinished += Exit;
    }

    public override void UpdateHandle()
    {
        if (!Unit.UnitStamina.Check(LocalDataManager.Instance.Config.BlockStaminaConsume))
        {
            Exit();
        }
    }

    public override void Exit()
    {
        UnitStateMachine.IsBlockFinished -= Exit;
        
        base.Exit();
    }
}
