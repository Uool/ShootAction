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
    private CameraController _camController;
    private PlayerMovementController _movementController;

    private Dictionary<string, IActionHandler> actionHandlers = new Dictionary<string, IActionHandler>();

    public float MoveMagnitude { get { return _movementController.PlayerMovement.sqrMagnitude; } }


    private CharacterState _currentState;
    public CharacterState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) { return; }

            // Todo: 해당 상태에 대한 핸들러가 실행되어야 함.
        }
    }

    public void Awake()
    {
        SetupPlayer();
    }

    // Todo: Awake 단계에서 실행되어야 한다.
    public void SetupPlayer()
    {
        _camController = FindObjectOfType<CameraController>();
        _movementController = GetComponent<PlayerMovementController>();

        _camController.SetCamera();
        _movementController.SetupMovement();

        SetupHandler();
    }

    void SetupHandler()
    {

    }

    public void SetHandler(string actionKey, IActionHandler handler)
    {
        actionHandlers[actionKey] = handler;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // 이동이 가능하다면, 이 코드를 실행하는게 맞는듯 한데..
        // Todo: 이동은 상시 가능함 (죽었을 때를 제외하고
        
        transform.position += _movementController.UpdateMovement();
        transform.rotation = _movementController.UpdatePlayerRotation();

        _camController.FollowTarget();
    }

    
}
