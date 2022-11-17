using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MoveActionHandler
{
    public Idle(PlayerMovementController movement) : base(movement)
    {

    }
    public override bool CanStartAction(PlayerController controller)
    {
        if (controller.isMoving) { return controller.MoveInput.magnitude < 0.2f; }

        return true;        
    }

    protected override void _StartAction(PlayerController controller)
    {
        _movement.currentState = CharacterState.Idle;
    }

    public override bool IsActive(PlayerController controller)
    { return _movement.currentState != null && (CharacterState)_movement.currentState == CharacterState.Idle; }

}
