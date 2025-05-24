using Managers;
using Resources;
using UnityEngine;

namespace Drones
{
    public class ResourceCollector : MonoBehaviour
    {
        private ResourceManager _resourceManager;
        private Drone _drone;

        /// <summary>
        /// Инициализирует сборщик ресурсов.
        /// </summary>
        /// <param name="resourceManager">Менеджер ресурсов для взаимодействия.</param>
        /// <param name="drone">Дрон, к которому прикреплен этот компонент.</param>
        public void Initialize(ResourceManager resourceManager, Drone drone)
        {
            _resourceManager = resourceManager;
            _drone = drone;
            if (_resourceManager && _drone)
            {
                Debug.Log($"ResourceCollector: Инициализирован для дрона ID {_drone.DroneID}.");
            }
            else
            {
                Debug.LogError("ResourceCollector: Не удалось инициализировать. ResourceManager или Drone отсутствуют.");
            }
        }

        /// <summary>
        /// Выполняет сбор указанного ресурса.
        /// </summary>
        /// <param name="resourceToCollect">Ресурс, который необходимо собрать.</param>
        public void CollectResource(Resource resourceToCollect)
        {
            if (_resourceManager && resourceToCollect)
            {
                Debug.Log($"Drone {_drone.DroneID} собирает ресурс.");
                _resourceManager.DestroyResource(resourceToCollect);
            }
            else
            {
                Debug.LogError("ResourceCollector: Невозможно собрать ресурс. ResourceManager или ресурс отсутствуют.");
            }
        }
    }
}