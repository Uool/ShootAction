using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    private static Handler s_Instance;
    public static Handler Instance { get { return s_Instance; } }

    private PlayerController controller;
    private Dictionary<string, IActionHandler> _actionHandlers = new Dictionary<string, IActionHandler>();
    public Dictionary<string, IActionHandler> ActionHandlers { get { return _actionHandlers; } }

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        SetupHandler();
    }

    private void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    private void SetupHandler()
    {
        SetHandler(HandlerTypes.Idle, new Idle());
        SetHandler(HandlerTypes.Move, new Move());
       
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

    public bool TryGetHandlerActive (string actionKey) 
    { return HandlerExists(actionKey) && IsActive(actionKey); }
 
    public bool IsActive(string actionKey)
    {
        CheckPlayerController();
        return GetHandler(actionKey).IsActive(controller);
    }

    public bool CanStartAction(string actionKey)
    {
        CheckPlayerController();
        return GetHandler(actionKey).CanStartAction(controller);
    }
    public bool CanEndAction(string action)
    {
        CheckPlayerController();
        return GetHandler(action).CanEndAction(controller);
    }

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
    { GetHandler(action).StartAction(controller); }

    public void EndAction(string action)
    { GetHandler(action).EndAction(controller); }

    private void CheckPlayerController()
    {
        if (controller == null) { Debug.Log("Can't find PlayerController"); controller = FindObjectOfType<PlayerController>(); }
    }
}
