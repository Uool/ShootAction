using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInputSystem _playerInputController;
    private InputAction _movement;
    private InputAction _attack;
    private InputAction _face;

    private Vector3 _moveInput;
    private Vector3 _currentAim;

    bool _attackLock;

    // Start is called before the first frame update
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerInputController = new PlayerInputSystem();

        _movement = _playerInputController.Player.Movement;
        _attack = _playerInputController.Player.Attack;
        _face = _playerInputController.Player.Face;

        _movement.Enable();
        _attack.Enable();
        _face.Enable();
        _currentAim = Vector3.zero;
    }

    private void OnDisable()
    {
        _movement.Disable();
        _attack.Disable();
        _face.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Facing();
        Attacking();
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

    private void Attacking()
    {
        if (false == _playerController.HandlerExists(HandlerTypes.Attack))
            return;

        if (_attack.IsPressed())
        {
            _playerController.StartAction(HandlerTypes.Attack);
            _attackLock = true;
        }
        else if (!_attack.IsPressed() && _attackLock)
        {
            _playerController.EndAction(HandlerTypes.Attack);
            _attackLock = false;
        }  
    }

    private void Facing()
    {
        if (false == _playerController.canFace) return;

        if (_face.IsPressed())
        {
            Debug.Log("조준중");
            
            // 화면의 마우스 위치 -> 월드 위치로 변환
            var playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            float hitDist = 0f; // 마우스 지점으로 부터 얼마나 떨어져있는지 거리
            if (playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Vector3 lookTarget = new Vector3(targetPoint.x - transform.position.x, targetPoint.z - transform.position.z, 0f);
                _playerController.SetFaceInput(lookTarget);
            }
            _playerController.isFacing = true;
        }
        else
        {
            _playerController.isFacing = false;
        }
    }
}
