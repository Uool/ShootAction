using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveActionHandler : InstantActionHandler
{
    protected PlayerMovementController _movement;
    public MoveActionHandler(PlayerMovementController movement)
    {
        this._movement = movement;
    }
}
