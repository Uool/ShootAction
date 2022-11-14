using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskAttack : Node
{
    private Transform _transform;
    private EnemyController _enemyController;

    private GameObject _impactVfxPrefab;
    private float _attackCounter = 0f;
    private float _impactVfxLifeTime = 2.5f;

    public TaskAttack(EnemyController enemyController, Transform transform)
    {
        _transform = transform;
        _enemyController = enemyController;
        _attackCounter = _enemyController.attackDelayTime;
        //_impactVfxPrefab = _enemyController.weaponController.impactVfxPrefab;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (_enemyController.attackCounter >= _enemyController.attackDelayTime)
        {
            // Todo : ���� �׾��� �� ���� ó��
            //Todo : Attack Animation (�ϴ� 0������, ���� �ִϸ��̼��� �߰��ȴٸ� 1~���ķε� ����)
            float duration = AnimationData.AttackDuration(_enemyController.enemyType, 0);
            if (false == _enemyController.IsAttacking)
            {
                _enemyController.Attack(0, duration);
            }
        }

        state = NodeState.Running;
        return state;
    }
}
