using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInputSystem _playerInputController;
    private InputAction _movement;

    private Vector3 _moveInput;
    private Vector3 _currentAim;

    // Start is called before the first frame update
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerInputController = new PlayerInputSystem();

        _movement = _playerInputController.Player.Movement;
        _movement.Enable();

        _currentAim = Vector3.zero;
    }

    private void OnDisable()
    {
        _movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private bool HasMoveInput() => _moveInput.magnitude > 0.1f;
    private void Moving()
    {
        _moveInput.Set(_movement.ReadValue<Vector2>().x, 0f, _movement.ReadValue<Vector2>().y);
        if (true == HasMoveInput())
            _playerController.SetMoveInput(_moveInput);
        else
            _playerController.SetMoveInput(Vector3.zero);
    }
}
