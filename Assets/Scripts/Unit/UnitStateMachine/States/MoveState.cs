using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] protected float _speed = 10f;

    private Rigidbody2D _rigidbody;
    protected Vector2 _moveDirection;
    protected Vector3 position => Unit.transform.position;


    private void Move()
    {
        _rigidbody.velocity = _moveDirection * _speed;

        Unit.transform.position = new Vector3(position.x, position.y, position.y);

        if (_moveDirection.x == 0) return;
        
        Unit.SetLookDirection(_moveDirection.x > 0 ? 1 : -1);
    }
    
    protected virtual void CalculateMoveDirection() { }
    
    public override void Enter()
    {
        base.Enter();

        _rigidbody = Unit.GetComponent<Rigidbody2D>();
    }

    public override void UpdateHandle()
    {
        CalculateMoveDirection();

        Move();

        if (_moveDirection != Vector2.zero) return;
        
        Exit();
    }

    public override void Exit()
    {
        _rigidbody.velocity = Vector2.zero;
        
        base.Exit();
    }
}
