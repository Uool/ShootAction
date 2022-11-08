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
        //_impactVfxPrefab = _enemyController.weaponController.impactVfxPrefab;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _enemyController.attackDelayTime)
        {
            // Todo : ���� �׾��� �� ���� ó�� (������ EnemyController�� �Ǿ�������, �μ��� target�� ó���ؾߵ�)
            //if (_enemyController.isDead)
            //{
            //    ClearData("target");
            //}
            //else
            //{
            //    // Todo : Attack Animation (�ϴ� 0������, ���� �ִϸ��̼��� �߰��ȴٸ� 1~���ķε� ����)
            //    // TODO : �� ��ȯ�� ���⿡�� �ؾߵ�.
                _enemyController.Attack(0);
                _attackCounter = 0f;
            //}
        }

        state = NodeState.Running;
        return state;
    }
}
