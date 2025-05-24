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
            }
            else
            {
                Debug.LogError("ResourceManager: ResourcePool не установлен. Невозможно вернуть ресурс.");
            }
        }
    }
}