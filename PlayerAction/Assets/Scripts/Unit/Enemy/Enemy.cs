using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : BehaviourTree.Tree
{
    #region Component
    protected Animator _animator;
    protected Health _health;

    [HideInInspector] public NavMeshAgent navi;
    [HideInInspector] public Weapon weaponController;
    #endregion

    #region Parameter

    protected bool _canMove = true;
    public bool CanMove { get { return _canMove; } }

    protected bool _canAction = true;
    public bool CanAction { get { return _canAction; } }

    protected bool _isAttacking = false;
    public bool IsAttacking { get { return _isAttacking; } }
    #endregion

    #region Variables
    public EnemyType enemyType;
    public LayerMask targetLayerMask;
    public float speed = 2f;
    public float damage = 10f;
    public float fovRange = 10f;
    public float attackRange = 2f;
    public float attackCounter;
    public float attackDelayTime;
    #endregion

    public Transform target;

    protected override void Init()
    {
        _animator = GetComponentInChildren<Animator>();
        navi = GetComponent<NavMeshAgent>();
        navi.updateRotation = false;

        _health = GetComponent<Health>();
        _health.onDie -= Dead;
        _health.onDie += Dead;

        weaponController = GetComponentInChildren<Weapon>();
        weaponController.SetOwner(gameObject);

        attackDelayTime = AnimationData.AttackDuration(enemyType, 0);
        attackCounter = attackDelayTime;
        weaponController.Init(damage, attackDelayTime);
    }

    protected override void TreeUpdate()
    {
        // 공격 제한 시간을 업데이트로 돌려준다.
        attackCounter += Time.deltaTime;
    }

    public void Chase()
    {
        _animator.SetBool(AnimationParameters.Moving, true);
    }

    public void Attack(int attackNumber, float duration)
    {
        _isAttacking = true;
        attackCounter = 0f;
        Lock(true, true, true, 0, duration);
        _animator.SetActionTrigger(AnimatorTrigger.AttackTrigger, attackNumber);
        _animator.SetBool(AnimationParameters.Moving, false);
        weaponController.TryAttack();
    }

    public void Dead()
    {
        isDead = true;
        _animator.SetAnimatorTrigger(AnimatorTrigger.DeathTrigger);
        _animator.SetBool(AnimationParameters.Moving, false);
        Managers.Resource.Destroy(this.gameObject, AnimationData.DeadDuration(enemyType));
    }

    private void Lock(bool lockMovement, bool lockAction, bool timed, float delayTime, float lockTime)
    {
        StopCoroutine("coLock");
        StartCoroutine(coLock(lockMovement, lockAction, timed, delayTime, lockTime));
    }

    private IEnumerator coLock(bool lockMovement, bool lockAction, bool timed, float delayTime, float lockTime)
    {
        if (delayTime > 0) { yield return new WaitForSeconds(delayTime); }

        if (true == lockMovement)
            _canMove = false;
        if (true == lockAction)
            _canAction = false;

        if (true == timed)
        {
            if (lockTime > 0) { yield return new WaitForSeconds(lockTime); }
            UnLock(lockMovement, lockAction);
        }
    }
    private void UnLock(bool move, bool action)
    {
        if (true == move)
            _canMove = true;
        if (true == action)
        {
            _canAction = true;
            if (true == _isAttacking)
                _isAttacking = false;
        }
    }
}
