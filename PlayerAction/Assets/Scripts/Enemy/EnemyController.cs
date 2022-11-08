using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// TODO: ������ �������� ��ũ��Ʈ�� ���� ����� ������.
// ����: ���͸��� �ൿ�ϴ� ����� �� �ٸ��� ������ (�� �Ϻδ� �Ȱ�����)
public class EnemyController : Enemy
{
    protected override void Init()
    {
        base.Init();
        // TODO: �ϴ� �ӽ÷� �÷��̾�� (���� �ǹ��� ���� �ؾߵ�)
        targetLayerMask = LayerMask.GetMask("Player");
        target = FindObjectOfType<PlayerController>().transform;
    }
    protected override Node SetupTree()
    {
        // selector -> ū �帧�� �ൿ (���� ã�´ٴ� ū �帧)
        // Sequence -> ���� �帧�� �ൿ (Ž�� �� �Ѿư��� or �����Ѵ�)
        // TODO: ���� �������� ������, �Ʒ����� Sequence�� �۵����� �ʴ°�?

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
