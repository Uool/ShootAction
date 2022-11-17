using System;

public class Move : MoveActionHandler
{
    public Move(PlayerMovementController movement) : base(movement)
    {

    }
    public override bool CanStartAction(PlayerController controller)
    {
        // Todo: 추후 움직임이 잠겨야 하는 상황에서 controller.canMove 추가
        return controller.MoveInput.sqrMagnitude > 0.1f;
    }

    protected override void _StartAction(PlayerController controller)
    {
        _movement.currentState = CharacterState.Move;
    }
    public override bool IsActive(PlayerController controller)
    { return _movement.currentState != null && (CharacterState)_movement.currentState == CharacterState.Move; }
}
