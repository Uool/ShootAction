using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public abstract class Enemy : BehaviourTree.Tree
{
    #region Component
    protected Animator _animator;
    protected Health _health;
    [HideInInspector] public Weapon weaponController;
    #endregion

    #region Parameter

    protected bool _canMove = true;
    public bool CanMove { get { return _canMove; } }

    protected bool _isAttacking = false;
    public bool IsAttacking { get { return _isAttacking; } }
    #endregion

    #region Variables
    public LayerMask targetLayerMask;
    public float speed = 2f;
    public float damage = 10f;
    public float fovRange = 10f;
    public float attackRange = 2f;
    public float attackDelayTime;
    #endregion

    public Transform target;

    protected override void Init()
    {
        _animator = GetComponentInChildren<Animator>();

        _health = GetComponent<Health>();
        _health.onDie -= Dead;
        _health.onDie += Dead;

        weaponController = GetComponentInChildren<Weapon>();
        weaponController.Init(damage, attackDelayTime);
    }

    public void Attack(int attackNumber)
    {
        _canMove = false;
        _isAttacking = true;

        _animator.SetActionTrigger(AnimatorTrigger.AttackTrigger, attackNumber);
        _animator.SetBool(AnimationParameters.Moving, false);
    }

    public void OutOfAttackRange()
    {
        _animator.SetBool(AnimationParameters.Moving, true);
        _canMove = true;
    }

    public void Dead()
    {
        isDead = true;
        _animator.SetAnimatorTrigger(AnimatorTrigger.DeathTrigger);
        _animator.SetBool(AnimationParameters.Moving, false);
    }
}
