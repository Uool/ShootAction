using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    #region Move_Parameter
    private Vector3 _moveInput;
    public Vector3 MoveInput => _moveInput;

    // 움직이고 있는 중인지 확인 여부
    public bool isMoving => TryGetHandlerActive(HandlerTypes.Move);

    #endregion


    public void Awake()
    {
        // 이건 나중에 Manager을 통해서 실행되게 끔 해야됨.
        SetupHandler();
    }

    // Todo: Awake 단계에서 실행되어야 한다.
    public void SetupPlayer()
    {
        SetupHandler();
    }

    #region Handler

    private Dictionary<string, IActionHandler> _actionHandlers = new Dictionary<string, IActionHandler>();

    private void SetupHandler()
    {

    }

    public void SetHandler(string actionKey, IActionHandler handler)
    {
        _actionHandlers[actionKey] = handler;
    }

    public IActionHandler GetHandler(string actionKey)
    {
        if (HandlerExists(actionKey)) { return _actionHandlers[actionKey]; }

        Debug.LogError("No handler for action : \"" + actionKey + "\"");
        return _actionHandlers[HandlerTypes.Null];
    }

    public bool HandlerExists(string actionKey)
    { return _actionHandlers.ContainsKey(actionKey); }

    public bool TryGetHandlerActive(string actionKey)
    { return HandlerExists(actionKey) && IsActive(actionKey); }

    public bool IsActive(string actionKey)
    { return GetHandler(actionKey).IsActive(this); }

    public bool CanStartAction(string actionKey)
    { return GetHandler(actionKey).CanStartAction(this); }
    public bool CanEndAction(string action)
    { return GetHandler(action).CanEndAction(this); }

    public bool TryStartAction(string action, object context = null)
    {
        if (!CanStartAction(action)) { return false; }

        StartAction(action);
        return true;
    }

    public bool TryEndAction(string action)
    {
        if (!CanEndAction(action)) { return false; }
        EndAction(action);
        return true;
    }
    public void StartAction(string action)
    { GetHandler(action).StartAction(this); }

    public void EndAction(string action)
    { GetHandler(action).EndAction(this); }
    #endregion

    #region Movement

    public void SetMoveInput(Vector3 moveInput)
    {
        _moveInput = moveInput;
    }
    #endregion
}
