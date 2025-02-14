using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TestTaskIMAGO.Scripts
{
    public class CharacterRayCast : MonoBehaviour
    {
        private PlayerInput _playerInput;
        [SerializeField] private float _maxDistance;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Gameplay.Click.performed += RaycastHandler;
        }

        private void RaycastHandler(InputAction.CallbackContext obj)
        {
            Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit, _maxDistance);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out RaycastCheck check))
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        private void OnDestroy()
        {
            _playerInput.Gameplay.Click.performed -= RaycastHandler;
            _playerInput.Disable();
        }
    }
}