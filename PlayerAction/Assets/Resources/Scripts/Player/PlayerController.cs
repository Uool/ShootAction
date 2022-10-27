using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    #region Reference
    private PlayerMovementController _movementController;

    #endregion

    #region Parameter
    public bool isMoving;
    public float MoveMagnitude { get { return _movementController.PlayerMovement.sqrMagnitude; } }

    #endregion


    public void Awake()
    {
        // 이건 나중에 Manager을 통해서 실행되게 끔 해야됨.
        SetupPlayer();
    }

    // Todo: Awake 단계에서 실행되어야 한다.
    public void SetupPlayer()
    {
        _movementController = GetComponent<PlayerMovementController>();
        _movementController.SetupMovement();

        SetupHandler();
    }

    #region Handler

    private Dictionary<string, IActionHandler> actionHandlers = new Dictionary<string, IActionHandler>();
    private void SetupHandler()
    {
        
    }

    public void SetHandler(string actionKey, IActionHandler handler)
    {
        actionHandlers[actionKey] = handler;
    }

    #endregion

}
