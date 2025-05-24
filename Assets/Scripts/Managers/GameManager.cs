using System.Collections;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public DroneManager droneManager;
        public GameObject dronePrefab;
        private const int NumberOfDronesToSpawn = 5;
        private const float SpawnInterval = 0.5f;

        public ResourceManager resourceManager;
        public GameObject resourcePrefab;
        private const int NumberOfResourcesToSpawn = 10;

        private void Start()
        {
            droneManager.InitializeDronePrefab(dronePrefab);
            StartCoroutine(SpawnDronesRoutine());

            resourceManager.Initialize(resourcePrefab);
            StartCoroutine(SpawnResourcesRoutine());
        }

        private IEnumerator SpawnDronesRoutine()
        {
            Debug.Log("GameManager: Запускаем спавн дронов...");
            for (var i = 0; i < NumberOfDronesToSpawn; i++)
            {
                droneManager.SpawnDrone(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f));
                yield return new WaitForSeconds(SpawnInterval);
            }

            Debug.Log("GameManager: Все дроны запрошены.");
        }

        private IEnumerator SpawnResourcesRoutine()
        {
            Debug.Log("GameManager: Запускаем спавн ресурсов..."); // Изменил сообщение для ясности
            for (var i = 0; i < NumberOfResourcesToSpawn; i++)
            {
                resourceManager.SpawnResources(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f));
                yield return new WaitForSeconds(SpawnInterval);
            }

            Debug.Log("GameManager: Все ресурсы запрошены."); // Изменил сообщение для ясности
        }
    }
}