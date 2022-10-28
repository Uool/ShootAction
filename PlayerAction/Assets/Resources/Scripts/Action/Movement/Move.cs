using System;

public class Move : MoveActionHandler
{
    public Move(PlayerMovementController movement) : base(movement)
    {

    }
    public override bool CanStartAction(PlayerController controller)
    {
        // Todo: ���� �������� ��ܾ� �ϴ� ��Ȳ���� controller.canMove �߰�
        return controller.MoveInput.sqrMagnitude > 0.1f;
    }

    protected override void _StartAction(PlayerController controller)
    {
        _movement.currentState = CharacterState.Move;
    }
    public override bool IsActive(PlayerController controller)
    { return _movement.currentState != null && (CharacterState)_movement.currentState == CharacterState.Move; }
}
