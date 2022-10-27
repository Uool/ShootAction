using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : InstantActionHandler
{
    public override bool CanStartAction(PlayerController controller)
    {
        if (controller.isMoving) { return controller.MoveMagnitude < 0.1f; }

        return true;        
    }

    protected override void _StartAction(PlayerController controller)
    {
        controller.CurrentState = CharacterState.Idle;
    }

    public override bool IsActive(PlayerController controller)
    {
        return controller.CurrentState == CharacterState.Idle;
    }
}
