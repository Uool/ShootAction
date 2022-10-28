using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : StateMachine
{
    private PlayerController _playerController;
    private CameraController _camController;
    private Animator _animator;
    private Rigidbody _playerRigid;

    // Movement
    private Vector3 _currentVelocity;
    private Quaternion _currentRotation;
    private float _moveSpeed = 3f;
    private float _moveAccel = 15f;
        
    // Todo: 추후 움직임이 잠겨야 하는 행동이 생길 때 다시 만들자.
    /*private bool _canMove;
    public bool canMove => _canMove && !isDead;*/
    

    // Rotation
    private Vector3 _prevMovement;
    private float _turnSpeed = 10f;

    private void Awake()
    {
        SetupMovement();
    }

    public void SetupMovement()
    {
        _playerController = GetComponent<PlayerController>();
        _camController = FindObjectOfType<CameraController>();
        _camController.SetCamera();
        _playerRigid = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();

        SetupHandler();
    }

    private void SetupHandler()
    {
        _playerController.SetHandler(HandlerTypes.Idle, new Idle(this));
        _playerController.SetHandler(HandlerTypes.Move, new Move(this));
    }

    private void Update()
    {
        gameObject.SendMessage("StateUpdate", SendMessageOptions.DontRequireReceiver);
    }

    private void FixedUpdate()
    {
        transform.position += _currentVelocity * Time.deltaTime;
        transform.rotation = UpdatePlayerRotation();
    }

    private void LateUpdate()
    {
        if (currentState == null && _playerController.CanStartAction(HandlerTypes.Idle))
            _playerController.StartAction(HandlerTypes.Idle);
    }

    // Idle로 돌아올 때 기초 상태 변화.
    private void Idle_EnterState()
    {
        Debug.Log("Idle_EnterState");
    }

    private void Idle_UpdateState()
    {
        _currentVelocity = Vector3.zero;
        //_currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, Time.deltaTime);
        _playerController.TryStartAction(HandlerTypes.Move);
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
        if (_playerController.MoveInput.sqrMagnitude > 0.1f)
        {
            _currentVelocity = Vector3.MoveTowards(_currentVelocity,
                _playerController.MoveInput * _moveSpeed,
                _moveAccel * Time.deltaTime);
        }
        else
        {
            _playerController.TryStartAction(HandlerTypes.Idle);
        }
    }

    private void Move_ExitState()
    {
        Debug.Log("Move_EnterState");
    }

    private Quaternion UpdatePlayerRotation()
    {
        if (_playerController.MoveInput != Vector3.zero)
            _prevMovement = _playerController.MoveInput;

        Quaternion lookRotation = Quaternion.LookRotation(_prevMovement);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        return Quaternion.Euler(rotation.x, rotation.y, 0f);
    }
}
