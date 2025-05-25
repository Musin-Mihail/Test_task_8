using System.Collections;
using Managers;
using Resources;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Drones
{
    public class DroneMovement : MonoBehaviour
    {
        private Drone _drone;
        private NavMeshAgent _navMeshAgent;
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

            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (!_navMeshAgent)
            {
                Debug.LogError("DroneMovement: NavMeshAgent не найден на этом GameObject!");
            }
        }

        private void Update()
        {
            if (!_targetResource && _drone.State == Enums.DroneState.SearchingForResource)
            {
                FindNearestResource();
            }

            if (_drone.State == Enums.DroneState.DeliveringResource)
            {
                if (_navMeshAgent.destination != _drone.BaseObj.transform.position)
                {
                    _navMeshAgent.SetDestination(_drone.BaseObj.transform.position);
                }

                if (Vector3.Distance(transform.position, _drone.BaseObj.transform.position) < 3.0f)
                {
                    Debug.Log($"Drone {_drone.DroneID} достиг базы.");
                    DepositResourceAtBase();
                }
            }
            else if (_targetResource && _navMeshAgent.enabled)
            {
                if (_navMeshAgent.destination != _targetResource.transform.position)
                {
                    _navMeshAgent.SetDestination(_targetResource.transform.position);
                }

                if (Vector3.Distance(transform.position, _targetResource.transform.position) < 0.8f)
                {
                    Debug.Log($"Drone {_drone.DroneID} достиг ресурса. Запуск таймера перед сбором.");
                    _navMeshAgent.isStopped = true;
                    _navMeshAgent.enabled = false;
                    StartCoroutine(CollectResourceWithDelay(_targetResource));
                    _targetResource = null;
                }
            }
        }

        /// <summary>
        /// Корутина для ожидания 2 секунд, воспроизведения анимации, а затем сбора ресурса.
        /// </summary>
        /// <param name="resourceToCollect">Ресурс для сбора.</param>
        private IEnumerator CollectResourceWithDelay(Resource resourceToCollect)
        {
            resourceToCollect.PlayCollectionAnimation();
            _drone.State = Enums.DroneState.CollectingResource;
            yield return new WaitForSeconds(2f);
            Debug.Log($"Drone {_drone.DroneID} завершил ожидание. Передача на сбор ресурса.");
            _resourceCollector.CollectResource(resourceToCollect);

            if (_navMeshAgent)
            {
                _navMeshAgent.enabled = true;
                _navMeshAgent.ResetPath();
                _navMeshAgent.isStopped = false;
                _drone.State = Enums.DroneState.DeliveringResource;
            }
        }

        /// <summary>
        /// Выгружает ресурс на базу и запускает частицы из базы.
        /// </summary>
        private void DepositResourceAtBase()
        {
            Debug.Log($"Drone {_drone.DroneID} выгружает ресурс на базу");
            _drone.Base.DepositResource();
            _drone.Base.PlayResourceParticles();
            _drone.State = Enums.DroneState.SearchingForResource;
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
                if (resource.isAvailable)
                {
                    var distance = Vector3.Distance(transform.position, resource.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestResource = resource;
                    }
                }
            }

            if (nearestResource)
            {
                nearestResource.isAvailable = false;
                _targetResource = nearestResource;
                _drone.State = Enums.DroneState.MovingToResource;
                Debug.Log($"Drone {_drone.DroneID} нашел ближайший ресурс в позиции {_targetResource.transform.position}.");
            }
            else
            {
                Debug.Log($"Drone {_drone.DroneID}: Ресурсы не найдены.");
            }
        }
    }
}