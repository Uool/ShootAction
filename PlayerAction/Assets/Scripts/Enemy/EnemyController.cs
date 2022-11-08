using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// TODO: 몬스터의 종류별로 스크립트를 따로 만드는 것으로.
// 이유: 몬스터마다 행동하는 양식이 다 다르기 때문에 (뭐 일부는 똑같겠지)
public class EnemyController : Enemy
{
    protected override void Init()
    {
        base.Init();
        // TODO: 일단 임시로 플레이어로 (추후 건물도 따로 해야됨)
        targetLayerMask = LayerMask.GetMask("Player");
        target = FindObjectOfType<PlayerController>().transform;
    }
    protected override Node SetupTree()
    {
        // selector -> 큰 흐름의 행동 (적을 찾는다는 큰 흐름)
        // Sequence -> 작은 흐름의 행동 (탐지 후 쫓아간다 or 공격한다)
        // TODO: 위에 시퀸스가 끝나면, 아래쪽의 Sequence는 작동하지 않는가?

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(this, transform),
                new TaskAttack(this, transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckTargetInFOVRange(this, transform),
                new TaskGoToTarget(this, transform),
            }),
        });
        return root;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fovRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
