using UnityEngine;

namespace Resources
{
    public class Resource : MonoBehaviour
    {
        public bool isAvailable;
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void PlayCollectionAnimation()
        {
            if (_particleSystem)
            {
                _particleSystem.Play();
            }

            Debug.Log($"Воспроизведение анимации сбора для ресурса по адресу {transform.position}");
        }
    }
}