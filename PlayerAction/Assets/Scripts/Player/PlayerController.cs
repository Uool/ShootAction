using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private RangeWeaponController _weaponController;

    #region Move_Parameter
    private Vector3 _moveInput;
    public Vector3 MoveInput => _moveInput;

    // Todo: 나중에 액션이 많아지게 되거나 어떤 고정적인 행동이 생긴다면 can 시리즈의 무조껀 적인 true는 수정되어야 한다.
    public bool canAction => _canAction && !isDead;
    private bool _canAction = true;

    public bool canMove => _canMove && !isDead;
    private bool _canMove = true;

    // 움직이고 있는 중인지 확인 여부
    public bool isMoving => TryGetHandlerActive(HandlerTypes.Move);

    public bool isAttacking => _isAttacking;
    private bool _isAttacking;

    public bool isFacing;
    public Vector3 faceInput => _faceInput;
    private Vector3 _faceInput;

    public bool canFace => _canFace && !isDead;
    private bool _canFace = true;

    // TODO: 추후 애니메이션 추가 시 수정
    public bool isDead = false;
    #endregion

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _weaponController = GetComponentInChildren<RangeWeaponController>();

        // 이건 나중에 Manager을 통해서 실행되게 끔 해야됨.
        SetupHandler();
    }

    // Todo: Awake 단계에서 실행되어야 한다.
    public void SetupPlayer()
    {
        SetupHandler();
    }

    #region Handler

    private Dictionary<string, IActionHandler> _actionHandlers = new Dictionary<string, IActionHandler>();

    private void SetupHandler()
    {
        SetHandler(HandlerTypes.Attack, new RangeAttack());
    }

    public void SetHandler(string actionKey, IActionHandler handler)
    {
        _actionHandlers[actionKey] = handler;
    }

    public IActionHandler GetHandler(string actionKey)
    {
        if (HandlerExists(actionKey)) { return _actionHandlers[actionKey]; }

        Debug.LogError("No handler for action : \"" + actionKey + "\"");
        return _actionHandlers[HandlerTypes.Null];
    }

    public bool HandlerExists(string actionKey)
    { return _actionHandlers.ContainsKey(actionKey); }

    public bool TryGetHandlerActive(string actionKey)
    { return HandlerExists(actionKey) && IsActive(actionKey); }

    public bool IsActive(string actionKey)
    { return GetHandler(actionKey).IsActive(this); }

    public bool CanStartAction(string actionKey)
    { return GetHandler(actionKey).CanStartAction(this); }
    public bool CanEndAction(string action)
    { return GetHandler(action).CanEndAction(this); }

    public bool TryStartAction(string action, object context = null)
    {
        if (!CanStartAction(action)) { return false; }

        StartAction(action);
        return true;
    }

    public bool TryEndAction(string action)
    {
        if (!CanEndAction(action)) { return false; }
        EndAction(action);
        return true;
    }
    public void StartAction(string action)
    { GetHandler(action).StartAction(this); }

    public void EndAction(string action)
    { GetHandler(action).EndAction(this); }
    #endregion

    #region Movement
    public void SetMoveInput(Vector3 moveInput)
    {
        _moveInput = moveInput;
    }

    public void SetFaceInput(Vector3 faceInput)
    {
        _faceInput = faceInput;
    }

    #endregion

    #region Attack

    public void Attack(int attackNumber)
    {
        _isAttacking = true;
        _animator.SetActionTrigger(AnimatorTrigger.AttackTrigger, attackNumber);
        _animator.SetFloat(AnimationParameters.MoveVelocity, _moveInput.magnitude);
        _animator.SetBool(AnimationParameters.MousePressed, true);

        _weaponController.TryAttack();
    }
    public void EndAttack()
    {
        _animator.SetBool(AnimationParameters.MousePressed, false);
        //_weaponController.();
    }

    #endregion
}
