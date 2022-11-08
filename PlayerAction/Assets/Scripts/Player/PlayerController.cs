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

    // Todo: ���߿� �׼��� �������� �ǰų� � �������� �ൿ�� ����ٸ� can �ø����� ������ ���� true�� �����Ǿ�� �Ѵ�.
    public bool canAction => _canAction && !isDead;
    private bool _canAction = true;

    public bool canMove => _canMove && !isDead;
    private bool _canMove = true;

    // �����̰� �ִ� ������ Ȯ�� ����
    public bool isMoving => TryGetHandlerActive(HandlerTypes.Move);

    public bool isAttacking => _isAttacking;
    private bool _isAttacking;

    public bool isFacing;
    public Vector3 faceInput => _faceInput;
    private Vector3 _faceInput;

    public bool canFace => _canFace && !isDead;
    private bool _canFace = true;

    // TODO: ���� �ִϸ��̼� �߰� �� ����
    public bool isDead = false;
    #endregion

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _weaponController = GetComponentInChildren<RangeWeaponController>();

        // �̰� ���߿� Manager�� ���ؼ� ����ǰ� �� �ؾߵ�.
        SetupHandler();
    }

    // Todo: Awake �ܰ迡�� ����Ǿ�� �Ѵ�.
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
