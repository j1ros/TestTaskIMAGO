using UnityEngine;

namespace TestTaskIMAGO.Scripts
{
    public class CharacterCameraController : MonoBehaviour
    {
        [SerializeField] private Vector2 _yClamp;
        [SerializeField] private Vector2 _xClamp;
        [SerializeField] private float _sensitivity;
        private PlayerInput _playerInput;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraParent;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            Vector2 frameInput = _playerInput.Gameplay.Mouse.ReadValue<Vector2>();
            frameInput *= _sensitivity;
            Quaternion localRotationY = _camera.transform.localRotation;
            Quaternion localRotationX = _cameraParent.localRotation;
            
            Quaternion rotationYaw = Quaternion.Euler(0.0f, frameInput.x, 0.0f);
            Quaternion rotationPitch = Quaternion.Euler(-frameInput.y, 0.0f, 0.0f);

            localRotationX *= rotationYaw;
            localRotationX = ClampX(localRotationX);
            
            localRotationY *= rotationPitch;
            localRotationY = ClampY(localRotationY);
            
            _cameraParent.localRotation = localRotationX;
            _camera.transform.localRotation = localRotationY;
        }
        
        private Quaternion ClampY(Quaternion rotation)
        {
            rotation.x /= rotation.w;
            rotation.y /= rotation.w;
            rotation.z /= rotation.w;
            rotation.w = 1.0f;

            float pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.x);
            pitch = Mathf.Clamp(pitch, _yClamp.x, _yClamp.y);
            rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

            return rotation;
        }

        private Quaternion ClampX(Quaternion rotation)
        {
            rotation.x /= rotation.w;
            rotation.y /= rotation.w;
            rotation.z /= rotation.w;
            rotation.w = 1.0f;
            
            float pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.y);
            pitch = Mathf.Clamp(pitch, _xClamp.x, _xClamp.y);
            rotation.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

            return rotation;
        }
        
        private void OnDestroy()
        {
            _playerInput.Disable();
        }
    }
}