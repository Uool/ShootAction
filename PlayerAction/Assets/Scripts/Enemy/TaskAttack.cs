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
            // Todo : 적이 죽었을 떄 삭제 처리 (지금은 EnemyController로 되어있지만, 부수는 target을 처리해야됨)
            //if (_enemyController.isDead)
            //{
            //    ClearData("target");
            //}
            //else
            //{
            //    // Todo : Attack Animation (일단 0이지만, 추후 애니메이션이 추가된다면 1~이후로도 가능)
            //    // TODO : 딜 교환은 무기에서 해야됨.
                _enemyController.Attack(0);
                _attackCounter = 0f;
            //}
        }

        state = NodeState.Running;
        return state;
    }
}
