using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public class State
    {
        public Action startState = DoNothing;
        public Action updateState = DoNothing;
        public Action fixedUpdateState = DoNothing;
        public Action exitState = DoNothing;

        public Enum currentState;
    }

    public State state = new State();
    public Enum currentState
    {
        get => state.currentState;
        set
        {
            if (state.currentState == value)
                return;

            ChangingState();
            state.currentState = value;
            ConfigureCurrentState();
        }
    }

    [HideInInspector] public Enum lastState;
    protected float _timeEnteredState;

    private void ChangingState()
    {
        lastState = currentState;
        _timeEnteredState = Time.time;
    }

    private void ConfigureCurrentState()
    {
        if (state.exitState != null) { state.exitState(); }

        // Now we need to configure all of the methods.
        state.updateState = ConfigureDelegate<Action>("UpdateState", DoNothing);
        state.startState = ConfigureDelegate<Action>("StartState", DoNothing);
        state.exitState = ConfigureDelegate<Action>("ExitState", DoNothing);

        if (state.startState != null) { state.startState(); }
    }

    private Dictionary<Enum, Dictionary<string, Delegate>> _cache = new Dictionary<Enum, Dictionary<string, Delegate>>();
    private T ConfigureDelegate<T>(string methodRoot,T Default) where T : class
    {
        // cache에 키가 없으면 해당 키에 값을 새로 만들어 넣어준다
        if (false == _cache.TryGetValue(state.currentState, out Dictionary<string, Delegate> lookup))
        {
            _cache[state.currentState] = lookup = new Dictionary<string, Delegate>();
        }
        if (false == lookup.TryGetValue(methodRoot, out Delegate returnValue))
        {
            System.Reflection.MethodInfo methodInfo = GetType().GetMethod(state.currentState.ToString() + "_" + methodRoot, System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod);
            if (methodInfo != null)
            {
                returnValue = Delegate.CreateDelegate(typeof(T), this, methodInfo);
            }
            else
                returnValue = Default as Delegate;

            lookup[methodRoot] = returnValue;
        }
        return returnValue as T;
    }

    private void StateUpdate()
    {
        EarlyUpdate();
        if (state.updateState != null)
            state.updateState();
    }

    private void FixedStateUpdate()
    {
        if (state.fixedUpdateState != null)
            state.fixedUpdateState();
    }


    protected virtual void EarlyUpdate() { }

    private static void DoNothing() { }
    
}
