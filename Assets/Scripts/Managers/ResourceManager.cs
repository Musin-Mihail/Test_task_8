using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        public ResourcePool resourcePool;

        public void Initialize(GameObject prefab)
        {
            if (resourcePool)
            {
                resourcePool.resourcePrefab = prefab;
                resourcePool.InitializePool();
                Debug.Log("ResourceManager: Пул ресурсов инициализирован.");
            }
            else
            {
                Debug.LogError("ResourceManager: Ссылка на ResourcePool не установлена!");
            }
        }

        public void SpawnResources(Vector3 spawnPosition)
        {
            if (!resourcePool)
            {
                Debug.LogError("ResourceManager: ResourcePool не установлен. Невозможно создать ресурс.");
                return;
            }

            var newResource = resourcePool.GetResource(spawnPosition);

            if (newResource)
            {
                Debug.Log($"ResourceManager: Создан ресурс из пула в позиции: {spawnPosition}");
            }
            else
            {
                Debug.LogError("ResourceManager: Не удалось получить ресурс из пула.");
            }
        }

        public void DestroyResource(Resource resource)
        {
            if (resourcePool)
            {
                resourcePool.ReturnResource(resource);
                Debug.Log($"ResourceManager: Ресурс возвращен в пул.");
                SpawnResources(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f));
                Debug.Log("ResourceManager: Создан новый ресурс после уничтожения.");
            }
            else
            {
                Debug.LogError("ResourceManager: ResourcePool не установлен. Невозможно вернуть ресурс.");
            }
        }

        public List<Resource> GetActiveResources()
        {
            if (resourcePool)
            {
                return resourcePool.GetActiveResources();
            }
            else
            {
                Debug.LogError("ResourceManager: ResourcePool не установлен. Невозможно получить активные ресурсы.");
                return new List<Resource>();
            }
        }
    }
}