using System;
using UnityEngine;

namespace TestTaskIMAGO.Scripts
{
    public class CharacterMovement2D : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private bool _isRight = true;
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 moveDir = _playerInput.Gameplay.WASD.ReadValue<Vector2>();
            if (_isRight && moveDir.x < 0)
            {
                _isRight = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);
            }
            else if (!_isRight && moveDir.x > 0)
            {
                _isRight = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);
            }
            
            Vector3 movement = new Vector3(moveDir.x, moveDir.y, 0) * _speed * Time.fixedDeltaTime;
            
            _rigidbody2D.linearVelocity = movement;
        }

        private void OnDestroy()
        {
            _playerInput.Disable();
        }
    }
}