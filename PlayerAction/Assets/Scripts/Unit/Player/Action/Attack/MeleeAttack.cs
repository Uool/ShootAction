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

    // �� ������ �������� ���ݹۿ� ���� ������ active�� �׻� Ȱ��ȭ �Ǿ��־�� �Ѵ�.
    // canStart�� End�� ��� true�� ����
    protected override void _StartAction(PlayerController controller)
    {
        // TODO: ���� ������ �þ�ٸ� �� Number�� ��ȭ�ؾ� �Ѵ�
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
