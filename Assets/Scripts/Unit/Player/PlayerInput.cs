using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private Player _player;

    public PlayerInputActions PlayerInputActions => _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Unit.Attack.performed += ctx => ToAttackState();
        _playerInputActions.Unit.Block.started += ctx => ToBlockState();
        _playerInputActions.Unit.Block.performed += ctx => FinishBlock();
        _playerInputActions.Unit.Move.performed += ctx => ToMoveState();
        _playerInputActions.Unit.Roll.performed += ctx => ToRollState();
        _playerInputActions.Enable();

        _player = GetComponent<Player>();
    }

    private void ToMoveState()
    {
        _player.UnitStateMachine.SetState(UnitState.Move);
    }

    private void ToAttackState()
    {
        _player.UnitStateMachine.SetState(UnitState.Attack);
    }

    private void ToBlockState()
    {
        _player.UnitStateMachine.SetState(UnitState.Block);
    }

    private void FinishBlock()
    {
        _player.UnitStateMachine.FinishBlock();
    }

    private void ToRollState()
    {
        _player.UnitStateMachine.SetState(UnitState.Roll);
    }
}
