using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private DroneManager _droneManager;
        private ResourceManager _resourceManager;

        public GameObject dronePrefab;
        public GameObject resourcePrefab;

        private GameObject _baseRed;
        private GameObject _baseBlue;

        public int NumberOfDronesPerFaction { get; set; } = 3;
        public float DroneSpeed { get; set; } = 5f;
        public float ResourceSpawnFrequency { get; set; } = 0.5f;
        public bool DrawDronePath { get; set; } = true;

        private const int NumberOfResourcesToSpawn = 10;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GameScene")
            {
                FindGameSceneDependencies();
                StartGame();
            }
            else if (scene.name == "MainMenuScene")
            {
                Debug.Log("Вернулись в главное меню.");
            }
        }

        private void FindGameSceneDependencies()
        {
            _droneManager = FindAnyObjectByType<DroneManager>();
            if (!_droneManager)
            {
                Debug.LogError("GameManager: DroneManager не найден в GameScene!");
                var dmGo = new GameObject("DroneManager");
                _droneManager = dmGo.AddComponent<DroneManager>();
            }

            _resourceManager = FindAnyObjectByType<ResourceManager>();
            if (!_resourceManager)
            {
                Debug.LogError("GameManager: ResourceManager не найден в GameScene!");
                var rmGo = new GameObject("ResourceManager");
                _resourceManager = rmGo.AddComponent<ResourceManager>();
            }

            _baseRed = GameObject.FindWithTag("BaseRed");
            if (!_baseRed)
            {
                Debug.LogError("GameManager: Объект с тегом 'BaseRed' не найден в GameScene!");
            }

            _baseBlue = GameObject.FindWithTag("BaseBlue");
            if (!_baseBlue)
            {
                Debug.LogError("GameManager: Объект с тегом 'BaseBlue' не найден в GameScene!");
            }
        }

        private void StartGame()
        {
            if (!_droneManager || !_resourceManager || !dronePrefab || !resourcePrefab || !_baseRed || !_baseBlue)
            {
                Debug.LogError("GameManager: Не все зависимости для запуска игры найдены. Проверьте настройки сцены.");
                return;
            }

            Debug.Log($"Игра запущена с настройками: Дронов на фракцию: {NumberOfDronesPerFaction}, Скорость дронов: {DroneSpeed}, Частота спавна ресурсов: {ResourceSpawnFrequency}, Отрисовка пути: {DrawDronePath}");

            _droneManager.InitializeDronePrefab(dronePrefab);
            StartCoroutine(SpawnDronesRoutine());

            _resourceManager.Initialize(resourcePrefab);
            StartCoroutine(SpawnResourcesRoutine());
        }

        private IEnumerator SpawnDronesRoutine()
        {
            Debug.Log("GameManager: Запускаем спавн дронов...");
            for (var i = 0; i < NumberOfDronesPerFaction; i++)
            {
                _droneManager.SpawnDrone(new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)), _baseBlue);
                yield return new WaitForSeconds(0.5f);
                _droneManager.SpawnDrone(new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)), _baseRed);
                yield return new WaitForSeconds(0.5f);
            }

            Debug.Log("GameManager: Все дроны запрошены.");
        }

        private IEnumerator SpawnResourcesRoutine()
        {
            Debug.Log("GameManager: Запускаем спавн ресурсов...");
            for (var i = 0; i < NumberOfResourcesToSpawn; i++)
            {
                _resourceManager.SpawnResources(new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)));
                yield return new WaitForSeconds(ResourceSpawnFrequency);
            }

            Debug.Log("GameManager: Все ресурсы запрошены.");
        }

        public void ExitToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}