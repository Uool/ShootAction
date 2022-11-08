using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // 현재 노드의 상태가 성공인지, 실패인지에 대한 여부를 확인하기 위한 Selector
    // 모든 노드가 실패상태라면 Failure을 반환.
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        state = NodeState.Success;
                        return state;
                    case NodeState.Running:
                        state = NodeState.Running;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.Failure;
            return state;
        }

    }
}

