using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// �۾� : Ÿ������ ���ϼ���.
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

        if (target == null)
            target = _enemyController.target;

        float distance = (target.position - _transform.position).sqrMagnitude;
        if(distance > 0.01f && _enemyController.CanMove)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0f, _enemyController.target.position.z);
            _enemyController.navi.SetDestination(targetPosition);
            _transform.rotation = Quaternion.LookRotation(_enemyController.navi.velocity.normalized);

            //_transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _enemyController.speed * Time.deltaTime);
            //_transform.LookAt(target);
            _enemyController.Chase();           
        }
        state = NodeState.Running;
        return state;
    }
}
