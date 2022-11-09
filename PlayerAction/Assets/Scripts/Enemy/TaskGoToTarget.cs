using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// 작업 : 타겟으로 향하세요.
public class TaskGoToTarget : Node
{
    private Transform _transform;
    private EnemyController _enemyController;
    public TaskGoToTarget(EnemyController enemyController,Transform transform)
    {
        _transform = transform;
        _enemyController = enemyController;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        // TODO: 임시로 타겟지정
        float distance = (_enemyController.target.position - _transform.position).sqrMagnitude;
        if(distance > 0.01f)
        {
            Vector3 targetPosition = new Vector3(_enemyController.target.position.x, 0f, _enemyController.target.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _enemyController.speed * Time.deltaTime);
            _transform.LookAt(target);
            state = NodeState.Running;
        }
        // Todo : 적이 이동하면서 때리러 온다. 이게 계속 작동되서 그럼
        //else if (_enemyController.)
        return state;
    }
}
