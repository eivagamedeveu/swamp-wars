using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : MoveState
{
    [SerializeField] private PlayerInput _playerInput;

    protected override void CalculateMoveDirection()
    {
        _moveDirection = _playerInput.PlayerInputActions.Unit.Move.ReadValue<Vector2>();
    }
}
