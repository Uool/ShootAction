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
        // TODO : 내가 만드는 게임에는 타겟이 모두 정해져있다.
        // Player이냐, Building이냐의 차이긴 한데,
        // Player같은 경우는 수색이 추가적으로 필요가 없고, Building같은경우엔, 최종목적물 or 가장 근처에 있는 Building으로 되어야 한다.
        object target = GetData("target");
        
        // TODO: 임시로 target을 Player로 고정
        // 추후 타겟이 Building인지, Player인지 구별하여 해야됨
        // Building 같은 경우엔, 처음에 target을 주겠지만, 목적지까지 가는 길에 Building을 부셔야 할 일이 있기 때문.
        if (null == _enemyController.target)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _enemyController.fovRange, _enemyController.targetLayerMask);
            if (colliders.Length > 0)
            {
                // Sequence의 부모 Selector 에게 SetData를 한다.
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
