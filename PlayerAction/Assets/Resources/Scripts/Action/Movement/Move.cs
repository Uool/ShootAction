using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : InstantActionHandler
{
    public override bool CanStartAction(PlayerController controller)
    {
        return controller.MoveMagnitude > 0.2f;
    }

    protected override void _StartAction(PlayerController controller)
    {
        controller.CurrentState = CharacterState.Move;
    }
    public override bool IsActive(PlayerController controller)
    {
        return controller.CurrentState == CharacterState.Move;
    }
}
