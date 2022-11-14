using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private EnemyController _enemyController;

    public CheckEnemyInAttackRange(EnemyController enemyController, Transform transform)
    {
        _enemyController = enemyController;
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        Transform target;
        if (null == t)
            target = _enemyController.target;
        else
            target = (Transform)t;

        float distance = Vector3.Distance(_transform.position, target.position);

        // ���� ��Ÿ��� �����ϰų�, �̹� �������� ��Ȳ������ ��� Success����.
        if (distance <= _enemyController.attackRange || true == _enemyController.IsAttacking)
        {
            _enemyController.navi.isStopped = true;
            _enemyController.navi.velocity = Vector3.zero;
            state = NodeState.Success;
            return state;
        }
        _enemyController.navi.isStopped = false;

        state = NodeState.Failure;
        return state;
    }
}
