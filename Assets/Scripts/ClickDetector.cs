using Drones;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private UnityEngine.Camera _mainCamera;
    private Drone _previousTarget;

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
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        var mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out var hit))
        {
            Debug.Log("Нажат объект: " + hit.collider.name);
            var clickedDrone = hit.collider.GetComponent<Drone>();

            if (_previousTarget)
            {
                _previousTarget.showState = false;
            }

            if (clickedDrone)
            {
                _previousTarget = clickedDrone;
                _previousTarget.showState = true;
            }
            else
            {
                _previousTarget = null;
                Debug.Log("На нажатом объекте нет скрипта Drone.");
            }
        }
        else
        {
            if (_previousTarget)
            {
                _previousTarget.showState = false;
            }

            _previousTarget = null;
        }
    }
}