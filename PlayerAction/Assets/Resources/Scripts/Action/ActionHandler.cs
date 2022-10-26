using System;

public interface IActionHandler
{
    bool CanStartAction(PlayerController controller);

    void StartAction(PlayerController controller);

    bool IsActive();

    bool CanEndAction(PlayerController controller);

    void EndAction(PlayerController controller);
}

public abstract class ActionHandler : IActionHandler
{
    public bool active;

    public abstract bool CanStartAction(PlayerController controller);

    public virtual void StartAction(PlayerController controller)
    {
        if (CanStartAction(controller)) { return; }
        active = true;
        _StartAction(controller);
    }

    public virtual bool IsActive() { return active; }

    protected abstract void _StartAction(PlayerController controller);

    public abstract bool CanEndAction(PlayerController controller);

    public virtual void EndAction(PlayerController controller)
    {
        if (!CanEndAction(controller)) { return; }
        active = false;
        _EndAction(controller);
    }

    protected abstract void _EndAction(PlayerController controller);
}
