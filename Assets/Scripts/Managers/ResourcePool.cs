using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace Managers
{
    public class ResourcePool : MonoBehaviour
    {
        public GameObject resourcePrefab;
        private readonly Queue<Resource> _availableResources = new();
        private readonly List<Resource> _activeResources = new();
        private Transform _poolParent;

        public void InitializePool()
        {
            if (!resourcePrefab)
            {
                Debug.LogError("ResourcePool: Префаб ресурса не установлен!");
                return;
            }

            _poolParent = new GameObject("ResourcePool").transform;
        }

        private Resource CreatePooledResource()
        {
            var resourceGo = Instantiate(resourcePrefab, _poolParent);
            var newResource = resourceGo.GetComponent<Resource>();

            if (newResource)
            {
                newResource.gameObject.SetActive(false);
                return newResource;
            }
            else
            {
                Debug.LogError("ResourcePool: Созданный объект не содержит скрипт Resource!");
                Destroy(resourceGo);
                return null;
            }
        }

        public Resource GetResource(Vector3 position)
        {
            Resource resourceToUse;

            if (_availableResources.Count > 0)
            {
                resourceToUse = _availableResources.Dequeue();
            }
            else
            {
                Debug.LogWarning("ResourcePool: Пул ресурсов исчерпан, создаем новый ресурс и добавляем его в пул.");
                resourceToUse = CreatePooledResource();
                if (!resourceToUse)
                {
                    return null;
                }
            }

            if (resourceToUse)
            {
                resourceToUse.transform.position = position;
                resourceToUse.gameObject.SetActive(true);
                _activeResources.Add(resourceToUse);
                return resourceToUse;
            }

            return null;
        }

        public void ReturnResource(Resource resource)
        {
            if (resource && _activeResources.Contains(resource))
            {
                resource.gameObject.SetActive(false);
                resource.transform.SetParent(_poolParent);
                _availableResources.Enqueue(resource);
                _activeResources.Remove(resource);
            }
        }
    }
}