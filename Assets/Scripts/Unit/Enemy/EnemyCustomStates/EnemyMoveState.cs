using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : MoveState
{
    [SerializeField] private Player _target;

    protected override void CalculateMoveDirection()
    {
        _moveDirection = (_target.transform.position - position).normalized;
    }
}
