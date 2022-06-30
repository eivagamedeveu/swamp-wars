using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(UnitStateMachine))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private UnitState _exitStateName;
    [SerializeField] private UnitState _name;
    [SerializeField] private bool _isInterrupted;
    [SerializeField] private List<UnitState> _whoCanInterrupt;
    [SerializeField] protected bool _isStaminaNeeded;
    [Header("Animation Settings")] 
    [SerializeField] private bool _isLooped;
    [SerializeField] protected AnimationReferenceAsset _stateAnimation;

    protected Unit Unit;
    protected UnitStateMachine UnitStateMachine;
    protected float RequiredStamina = 0;

    public UnitState Name => _name;
    public bool IsEnable { get; private set; } = false;
    

    protected virtual void Awake()
    {
        UnitStateMachine = GetComponent<UnitStateMachine>();
    }

    public void Init(Unit unit)
    {
        Unit = unit;
    }
    
    public virtual void Enter()
    {
        IsEnable = true;
        
        Unit.UnitAnimationsHandler.SetAnimation(_stateAnimation,_isLooped);
    }

    public virtual void UpdateHandle()
    {
    }

    public virtual void Exit()
    {
        IsEnable = false;
        
        if(_exitStateName != UnitState.None)
            UnitStateMachine.SetState(_exitStateName);
    }

    public bool CheckIsStaminaEnough()
    {
        return !_isStaminaNeeded || Unit.UnitStamina.Check(RequiredStamina);
    }

    public bool CheckInterruption(UnitState state)
    {
        return _isInterrupted && _whoCanInterrupt.Contains(state) || !IsEnable;
    }
}
