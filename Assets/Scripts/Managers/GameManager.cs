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

        private void Start()
        {
            droneManager.InitializeDronePrefab(dronePrefab);
            StartCoroutine(SpawnDronesRoutine());
        }

        IEnumerator SpawnDronesRoutine()
        {
            Debug.Log("GameManager: Запускаем спавн дронов...");
            for (int i = 0; i < NumberOfDronesToSpawn; i++)
            {
                droneManager.SpawnDrone(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f));
                yield return new WaitForSeconds(SpawnInterval);
            }

            Debug.Log("GameManager: Все дроны запрошены.");
        }
    }
}