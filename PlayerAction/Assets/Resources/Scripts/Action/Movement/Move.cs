using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : InstantActionHandler
{
    public override bool CanStartAction(PlayerController controller)
    {
        return controller.MoveMagnitude > 0.1f;
    }

    protected override void _StartAction(PlayerController controller)
    {
        controller.currentState = CharacterState.Move;
    }
    public override bool IsActive()
    {
        return true;
    }
}
