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
        // �̰� ���߿� Manager�� ���ؼ� ����ǰ� �� �ؾߵ�.
        SetupPlayer();
    }

    // Todo: Awake �ܰ迡�� ����Ǿ�� �Ѵ�.
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
