using UnityEngine;
using System;
using System.Collections.Generic;
using Xivol.StateMachine;

public class GameManager : Singleton<GameManager>
{
    Dictionary<Type, GameState> allStates;

    protected override void Initialize()
	{
        allStates = new Dictionary<Type, GameState>();
	}

	protected override void Start()
	{
        base.Start();
        //ChangeState<MovementState>();
	}

	protected GameState _currentState;
    public virtual GameState CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }

    public virtual T GetState<T>() where T : GameState
    {
        if (!allStates.ContainsKey(typeof(T)))
            allStates[typeof(T)] = ScriptableObject.CreateInstance<T>();
        return allStates[typeof(T)] as T;
    }

    public virtual void ChangeState<T>() where T : GameState
    {
        print("State is being Changed");
        CurrentState = GetState<T>();
    }

    public void Rollback()
    {
        Debug.LogWarning("Rollback is Not Implemented in " + this.ScriptName());
    }

    protected bool _inTransition = false;

    protected virtual void Transition(GameState value)
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;

        if (_currentState != null)
            _currentState.Leave();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
}
