using System.Collections.Generic;
using Drones;
using UnityEngine;

namespace Managers
{
    public class DroneManager : MonoBehaviour
    {
        private readonly List<Drone> _activeDrones = new();
        private GameObject _dronePrefabInternal;

        public void InitializeDronePrefab(GameObject prefab)
        {
            if (prefab)
            {
                _dronePrefabInternal = prefab;
                Debug.Log("DroneManager: Префаб дрона инициализирован.");
            }
            else
            {
                Debug.LogError("DroneManager: Префаб дрона не может быть null!");
            }
        }

        public void SpawnDrone(Vector3 spawnPosition)
        {
            if (!_dronePrefabInternal)
            {
                Debug.LogError("DroneManager: Невозможно создать дрон. Префаб дрона не установлен.");
            }

            var droneGo = Instantiate(_dronePrefabInternal, spawnPosition, Quaternion.identity);
            var newDrone = droneGo.GetComponent<Drone>();

            if (newDrone)
            {
                newDrone.Initialize(_activeDrones.Count + 1);
                _activeDrones.Add(newDrone);
                Debug.Log($"DroneManager: Создан дрон с ID: {newDrone.DroneID} в позиции: {spawnPosition}");
            }
            else
            {
                Debug.LogError("DroneManager: Созданный объект не содержит скрипт Drone!");
                Destroy(droneGo);
            }
        }
    }
}