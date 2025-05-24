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
        private ResourceCollector _resourceCollector;

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

            _resourceCollector = GetComponent<ResourceCollector>();
            if (!_resourceCollector)
            {
                Debug.LogError("DroneMovement: Скрипт ResourceCollector не найден на этом GameObject!");
            }
            else
            {
                _resourceCollector.Initialize(_resourceManager, _drone);
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
                    Debug.Log($"Drone {_drone.DroneID} достиг ресурса. Передача на сбор.");
                    _resourceCollector.CollectResource(_targetResource);
                    _targetResource = null;
                }
            }
        }

        /// <summary>
        /// Находит ближайший активный ресурс в сцене.
        /// </summary>
        private void FindNearestResource()
        {
            if (!_resourceManager || !_resourceManager.resourcePool)
            {
                Debug.LogWarning("DroneMovement: ResourceManager или ResourcePool не установлены. Невозможно найти ресурсы.");
                return;
            }

            Resource nearestResource = null;
            var minDistance = float.MaxValue;

            foreach (var resource in _resourceManager.GetActiveResources())
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
                Debug.Log($"Drone {_drone.DroneID} нашел ближайший ресурс в позиции {_targetResource.transform.position}.");
            }
            else
            {
                Debug.Log($"Drone {_drone.DroneID}: Ресурсы не найдены.");
            }
        }
    }
}