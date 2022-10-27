using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : StateMachine
{
    private PlayerInputController _playerInputController;
    private CameraController _camController;
    private InputAction _movement;
    private Animator _animator;
    private Rigidbody _playerRigid;

    private Vector3 _playerMovement;
    public Vector3 PlayerMovement { get { return _playerMovement; } }

    private Vector3 _currentVelocity;

    // Movement
    private float _moveSpeed = 3f;
    private float _moveAccel = 15f;
    private bool _canMove;
    public bool CanMove { get { return _canMove; } }

    // Rotation
    private Vector3 _prevMovement;
    private float _turnSpeed = 10f;

    public void SetupMovement()
    {
        _playerInputController = new PlayerInputController();
        _camController = FindObjectOfType<CameraController>();
        _camController.SetCamera();
        _playerRigid = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();

        _movement = _playerInputController.Player.Movement;
        _movement.Enable();

    }

    private void OnDisable()
    {
        _movement.Disable();
    }
    

    public bool IsMoved { get { return _movement.ReadValue<Vector2>() != Vector2.zero; } }

    private void FixedUpdate()
    {
        gameObject.SendMessage("StateUpdate", SendMessageOptions.DontRequireReceiver);
    }

    // Idle로 돌아올 때 기초 상태 변화.
    private void Idle_EnterState()
    {
        Debug.Log("Idle_EnterState");
    }

    private void Idle_UpdateState()
    {
        _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, Time.deltaTime);
    }

    private void Idle_ExitState()
    {
        Debug.Log("Idle_EnterState");
    }

    private void Move_EnterState()
    {
        Debug.Log("Move_EnterState");
    }

    private void Move_UpdateState()
    {
        transform.position += UpdateMovement();
        transform.rotation = UpdatePlayerRotation();
    }

    private void Move_ExitState()
    {
        Debug.Log("Move_EnterState");
    }

    private Vector3 UpdateMovement()
    {
        _playerMovement.Set(_movement.ReadValue<Vector2>().x, 0f, _movement.ReadValue<Vector2>().y);
        _currentVelocity = Vector3.MoveTowards(_currentVelocity,
            _playerMovement * _moveSpeed,
            _moveAccel * Time.deltaTime);

        return _currentVelocity * Time.deltaTime;
    }
    private Quaternion UpdatePlayerRotation()
    {
        if (_playerMovement != Vector3.zero)
            _prevMovement = _playerMovement;

        Quaternion lookRotation = Quaternion.LookRotation(_prevMovement);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;

        return Quaternion.Euler(rotation.x, rotation.y, 0f);
    }
}
