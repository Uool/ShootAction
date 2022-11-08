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
        //object t = GetData("target");
        //if (null == t)
        //{
        //    state = NodeState.Failure;
        //    return state;
        //}
        //Transform target = (Transform)t;
        Transform target = _enemyController.target;
        float distance = Vector3.Distance(_transform.position, target.position);

        // ���� ��Ÿ��� �����ϰų�, �̹� �������� ��Ȳ������ ��� Success����.
        if (distance <= _enemyController.attackRange || true == _enemyController.IsAttacking)
        {
            state = NodeState.Success;
            return state;
        }

        // �Ÿ��� ���� �ʴ� ��� (Player�� ������ ����ų� && ������ �������)
        _enemyController.OutOfAttackRange();
        state = NodeState.Failure;
        return state;
    }
}
