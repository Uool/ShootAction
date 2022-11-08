using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : ActionHandler
{
    public override bool CanStartAction(PlayerController controller)
    {
        return !active && controller.canAction;
    }
    public override bool CanEndAction(PlayerController controller)
    {
        return active;
    }

    // 내 공격은 연속적인 공격밖에 없기 때문에 active가 항상 활성화 되어있어야 한다.
    // canStart나 End가 모두 true인 이유
    protected override void _StartAction(PlayerController controller)
    {
        // TODO: 추후 공격이 늘어난다면 이 Number은 변화해야 한다
        var attackNumber = 0;
        controller.Attack(attackNumber);
    }

    protected override void _EndAction(PlayerController controller)
    {
        controller.EndAttack();
        if (controller.TryStartAction(HandlerTypes.Idle))
            return;
        controller.TryStartAction(HandlerTypes.Move);

    }
}
