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

        // 공격 사거리에 진입하거나, 이미 공격중인 상황에서는 계속 Success판정.
        if (distance <= _enemyController.attackRange || true == _enemyController.IsAttacking)
        {
            state = NodeState.Success;
            return state;
        }

        // 거리에 닫지 않는 경우 (Player가 범위를 벗어났거나 && 공격이 끝난경우)
        _enemyController.OutOfAttackRange();
        state = NodeState.Failure;
        return state;
    }
}
