using Managers;
using Resources;
using UnityEngine;

namespace Drones
{
    public class DroneMovement : MonoBehaviour
    {
        private Drone _drone;
        private const float _moveSpeed = 5f;
        private Resource _targetResource;
        private ResourceManager _resourceManager;

        private void Start()
        {
            _resourceManager = FindAnyObjectByType<ResourceManager>();
            if (!_resourceManager)
            {
                Debug.LogError("DroneMovement: ResourceManager не найден в сцене!");
            }

            _drone = GetComponent<Drone>();
            if (!_drone)
            {
                Debug.LogError("DroneMovement: Скрипт Drone не найден на этом GameObject!");
            }
        }

        private void Update()
        {
            if (!_targetResource)
            {
                FindNearestResource();
            }

            if (_targetResource)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetResource.transform.position, _moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _targetResource.transform.position) < 0.5f)
                {
                    Debug.Log($"Drone {_drone.DroneID} достиг ресурса.");
                    _resourceManager.DestroyResource(_targetResource);
                    _targetResource = null;
                }
            }
        }

        private void FindNearestResource()
        {
            if (!_resourceManager || !_resourceManager.resourcePool)
            {
                return;
            }

            Resource nearestResource = null;
            var minDistance = float.MaxValue;
            foreach (var resource in _resourceManager.GetActiveResources()) // Найти все активные ресурсы в сцене
            {
                var distance = Vector3.Distance(transform.position, resource.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestResource = resource;
                }
            }

            if (nearestResource)
            {
                _targetResource = nearestResource;
                Debug.Log($"Drone {_drone.DroneID} нашел ближайший ресурс с ID {nearestResource.GetInstanceID()} в позиции {_targetResource.transform.position}.");
            }
        }
    }
}