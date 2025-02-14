using UnityEngine;

namespace TestTaskIMAGO.Scripts
{
    public class CharacterMovementController : MonoBehaviour
    {
        private PlayerInput _playerInput;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed;
        [SerializeField] private float _steerSpeed;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        private void Update()
        {
            Vector2 inputDirection = _playerInput.Gameplay.WASD.ReadValue<Vector2>();
            Vector3 movement = new Vector3(inputDirection.x, 0, inputDirection.y);
            
            transform.Translate(Vector3.forward * _speed * movement.z * Time.deltaTime, Space.Self);

            if (movement.z != 0)
            {
                transform.Rotate(Vector3.up, (movement.z > 0 ? 1f : -1f) * _steerSpeed * movement.x * Time.deltaTime);
            }
            _animator.SetBool("IsMoving", movement.z > 0);
        }
        
        private void OnDestroy()
        {
            _playerInput.Disable();
        }
    }
}
