using System.Collections.Generic;
using Drones;
using UnityEngine;

namespace Managers
{
    public class DroneManager : MonoBehaviour
    {
        private readonly List<Drone> _activeDrones = new();
        public GameObject dronePrefabInternal;

        public void SpawnDrone(Vector3 spawnPosition, GameObject baseObj)
        {
            if (!dronePrefabInternal)
            {
                Debug.LogError("DroneManager: Невозможно создать дрон. Префаб дрона не установлен.");
            }

            var droneGo = Instantiate(dronePrefabInternal, spawnPosition, Quaternion.identity);
            var newDrone = droneGo.GetComponent<Drone>();

            if (newDrone)
            {
                newDrone.Initialize(_activeDrones.Count + 1, baseObj);
                _activeDrones.Add(newDrone);
                DroneStateManager.Instance.AddDrone(newDrone);
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