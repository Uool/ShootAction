using UnityEngine;
using BehaviourTree;

public class CheckTargetInFOVRange : Node
{
    private Transform _transform;
    private EnemyController _enemyController;

    public CheckTargetInFOVRange(EnemyController enemyController, Transform transform)
    {
        _transform = transform;
        _enemyController = enemyController;
    }

    public override NodeState Evaluate()
    {
        // TODO : ���� ����� ���ӿ��� Ÿ���� ��� �������ִ�.
        // Player�̳�, Building�̳��� ���̱� �ѵ�,
        // Player���� ���� ������ �߰������� �ʿ䰡 ����, Building������쿣, ���������� or ���� ��ó�� �ִ� Building���� �Ǿ�� �Ѵ�.
        object target = GetData("target");
        if (null == target)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _enemyController.fovRange, _enemyController.targetLayerMask);
            if (colliders.Length > 0)
            {
                // Sequence�� �θ� Selector ���� SetData�� �Ѵ�.
                parent.parent.SetData("target", colliders[0].transform);
                state = NodeState.Success;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }
        state = NodeState.Success;
        return state;
    }
}
