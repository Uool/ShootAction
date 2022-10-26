using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantActionHandler : ActionHandler
{
    public override void StartAction(PlayerController controller)
    {
        base.StartAction(controller);
        base.EndAction(controller);
    }

    public override bool IsActive()
    {
        return false;
    }

    public override bool CanEndAction(PlayerController controller)
    {
        return true;
    }

    protected override void _EndAction(PlayerController controller)
    {
        
    }
}
