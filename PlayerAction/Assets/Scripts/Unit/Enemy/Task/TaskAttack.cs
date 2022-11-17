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
            // Todo : 적이 죽었을 떄 삭제 처리
            //Todo : Attack Animation (일단 0이지만, 추후 애니메이션이 추가된다면 1~이후로도 가능)
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
