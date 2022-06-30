using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class UnitStateMachine : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private UnitState _firstStateName;

    private State _currentState;
    private List<State> _states;

    public Unit Unit => _unit;
    public event UnityAction IsBlockFinished;

    private void Awake()
    {
        InitStates();
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if(_currentState != null)
            _currentState.UpdateHandle();
    }

    private void InitStates()
    {
        _states = GetComponents<State>().ToList();
        
        foreach (var state in _states)
        {
            state.Init(Unit);
        }
    }

    public State GetStateByName(UnitState state)
    {
        return _states[(int) state];
    }

    public void Reset()
    {
        SetState(_firstStateName);
    }

    public bool SetState(UnitState stateName)
    {
        var targetState = GetStateByName(stateName);
        
        if (!targetState.CheckIsStaminaEnough())
        {
            return false;
        }
        
        if(_currentState != null)
        {
            if (!_currentState.CheckInterruption(stateName)) return false;
            
            if(_currentState.IsEnable)
                _currentState.Exit();
        }
        
        _currentState = targetState;
        _currentState.Enter();

        return true;
    }

    public void FinishBlock()
    {
        IsBlockFinished?.Invoke();
    }
}

[Serializable]
public enum UnitState
{
    Attack,
    Block,
    Die,
    Idle,
    MoveAndAttack,
    MoveAndBlock,
    Move,
    Roll,
    TakeDamageInBlock,
    TakeDamage,
    None
}

