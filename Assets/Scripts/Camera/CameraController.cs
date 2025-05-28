using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        private const float MovementSpeed = 10f;
        private const float MaxCoordX = 10f;
        private const float MinCoordX = -10f;
        private const float MaxCoordZ = 10f;
        private const float MinCoordZ = -10f;

        private const float ZoomSpeed = 10f;
        private const float MinOrthographicSize = 5f;
        private const float MaxOrthographicSize = 12f;

        private UnityEngine.Camera _mainCamera;

        private void Start()
        {
            _mainCamera = UnityEngine.Camera.main;
            if (!_mainCamera)
            {
                Debug.LogError("На сцене нет главной камеры (с тегом 'MainCamera').");
                enabled = false;
                return;
            }

            if (!_mainCamera.orthographic)
            {
                Debug.LogWarning("Камера не ортографическая. Скрипт масштабирования будет работать только с ортографическими камерами.");
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleZoom();
        }

        private void HandleMovement()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            var moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            var newPosition = transform.position + moveDirection * (MovementSpeed * Time.deltaTime);

            newPosition.x = Mathf.Clamp(newPosition.x, MinCoordX, MaxCoordX);
            newPosition.y = transform.position.y;
            newPosition.z = Mathf.Clamp(newPosition.z, MinCoordZ, MaxCoordZ);

            transform.position = newPosition;
        }

        private void HandleZoom()
        {
            if (!_mainCamera || !_mainCamera.orthographic) return;

            var scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput != 0)
            {
                _mainCamera.orthographicSize -= scrollInput * ZoomSpeed * Time.deltaTime * 100;
                _mainCamera.orthographicSize = Mathf.Clamp(_mainCamera.orthographicSize, MinOrthographicSize, MaxOrthographicSize);
            }
        }
    }
}