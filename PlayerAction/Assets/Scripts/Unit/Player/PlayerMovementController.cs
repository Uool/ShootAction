using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : StateMachine
{
    private PlayerController _playerController;
    private CameraController _camController;
    private CharacterController _charController;
    private Animator _animator;

    // Movement
    private Vector3 _currentVelocity;
    private Quaternion _currentRotation;
    private float _moveSpeed = 3f;
    private float _moveAccel = 15f;
       
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
        _charController = GetComponent<CharacterController>();
        //_camController = FindObjectOfType<CameraController>();
        //_camController.SetCamera();
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
        //gameObject.SendMessage("StateUpdate", SendMessageOptions.DontRequireReceiver);
        //transform.position += _currentVelocity * Time.deltaTime;
        _charController.Move(_currentVelocity * Time.deltaTime);
        if (_playerController.isFacing)
            transform.rotation = FacingPlayerRotation(_playerController.faceInput);
        else
            transform.rotation = UpdatePlayerRotation();

        if (false == _playerController.isDead && _playerController.canMove)
        {
            if (_currentVelocity.magnitude > 0f)
            {
                _animator.SetBool(AnimationParameters.Moving, true);
            }
            else
            {
                _animator.SetBool(AnimationParameters.Moving, false);
            }
        }
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

        Vector3 capsuleBottomBeforeMove = GetCapsuleBottomHemisphere();
        Vector3 capsuleTopBeforeMove = GetCapsuleTopHemisphere(_charController.height);
        // 전방 벽 확인
        if (Physics.CapsuleCast(capsuleBottomBeforeMove, capsuleTopBeforeMove, _charController.radius,
            _currentVelocity.normalized, out RaycastHit hit, _currentVelocity.magnitude * Time.deltaTime, -1,
            QueryTriggerInteraction.Ignore))
        {
            _currentVelocity = Vector3.ProjectOnPlane(_currentVelocity, hit.normal);
            Debug.Log($"Velocity : {_currentVelocity}, hitMesh : {hit.collider.gameObject.name}");
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

        Quaternion lookRotation = Quaternion.LookRotation(_prevMovement, Vector3.up);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        return Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    private Quaternion FacingPlayerRotation(Vector3 targetPos)
    {
        Vector3 lookDir = new Vector3(targetPos.x, 0f, targetPos.y);
        _prevMovement = lookDir;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        return Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    Vector3 GetCapsuleBottomHemisphere()
    {
        return transform.position + (transform.up * _charController.radius);
    }

    Vector3 GetCapsuleTopHemisphere(float height)
    {
        return transform.position + (transform.up * (height - _charController.radius));
    }
}
