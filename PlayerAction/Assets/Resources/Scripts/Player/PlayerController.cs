using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Idle = 0,
    Move = 1,
    Jump = 2,
    DoubleJump = 3,
    Fall = 4,
    Swim = 5,
    Block = 6,
    ClimbLadder = 7,
    Roll = 8,
    Knockback = 9,
    Knockdown = 10,
    DiveRoll = 11,
    Crawl = 12
}

public class PlayerController : MonoBehaviour
{
    #region Reference
    private PlayerMovementController _movementController;

    #endregion

    #region Parameter
    public bool isMoving;
    public float MoveMagnitude { get { return _movementController.PlayerMovement.sqrMagnitude; } }

    #endregion

    #region StateParameter

    // State가 변경됨에 따라 실행될 Action들
    private Action enterState;
    private Action updateState;
    private Action exitState;

    private float _timeEnteredState; 
    private CharacterState _currentState;
    private CharacterState _lastState;
    public CharacterState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) { return; }

            // Todo: 해당 상태에 대한 핸들러가 실행되어야 함.
            ChangingState();
            _currentState = value;
            ConfigueCurretState();
        }
    }
    public CharacterState LastState { get { return _lastState; } }

    #endregion

    public void Awake()
    {
        // 이건 나중에 Manager을 통해서 실행되게 끔 해야됨.
        SetupPlayer();
    }

    // Todo: Awake 단계에서 실행되어야 한다.
    public void SetupPlayer()
    {
        _movementController = GetComponent<PlayerMovementController>();
        _movementController.SetupMovement();

        SetupHandler();

        CurrentState = CharacterState.Idle;
    }

    #region Handler

    private Dictionary<string, IActionHandler> actionHandlers = new Dictionary<string, IActionHandler>();
    private void SetupHandler()
    {
        
    }

    public void SetHandler(string actionKey, IActionHandler handler)
    {
        actionHandlers[actionKey] = handler;
    }

    #endregion

    private void Update()
    {
        if (updateState != null)
            updateState.Invoke();
    }

    private void FixedUpdate()
    {
        // 이동이 가능하다면, 이 코드를 실행하는게 맞는듯 한데..
        // Todo: 이동은 상시 가능함 (죽었을 때를 제외하고
        
        //transform.position += _movementController.UpdateMovement();
        //transform.rotation = _movementController.UpdatePlayerRotation();

        //_camController.FollowTarget();
    }



    #region StateChange

    private Dictionary<Enum, Dictionary<string, Delegate>> _cache = new Dictionary<Enum, Dictionary<string, Delegate>>();

    private void ChangingState()
    {
        _lastState = _currentState;
        _timeEnteredState = Time.time;
    }

    private void ConfigueCurretState()
    {
        updateState = ConfigueDelegate<Action>(typeof(PlayerMovementController), "UpdateState", DoNothing);
        enterState = ConfigueDelegate<Action>(typeof(PlayerMovementController), "EnterState", DoNothing);
        exitState = ConfigueDelegate<Action>(typeof(PlayerMovementController), "ExitState", DoNothing);
    }
    
    private T ConfigueDelegate<T> (Type classType, string methodRoot, Action Default) where T : class
    {
        // _cache 에 해당 state에 대한 값이 없으면 새로 만들어준다.
        if (false == _cache.TryGetValue(_currentState, out Dictionary<string, Delegate> lookup))
        {
            _cache[_currentState] = lookup = new Dictionary<string, Delegate>();
        }

        if (false == lookup.TryGetValue(methodRoot, out Delegate returnValue))
        {
            // 해당 Type에서 methodRoot에 해당하는 method를 가져온다.
            System.Reflection.MethodInfo methodInfo = classType.GetMethod(CurrentState.ToString() + "_" + methodRoot, System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod);

            if (methodInfo != null) { returnValue = Delegate.CreateDelegate(typeof(T), _movementController, methodInfo); }
            else { returnValue = Default as Delegate; }
            lookup[methodRoot] = returnValue;
        }

        return returnValue as T;
    }

    private static void DoNothing() { }
    #endregion
}
