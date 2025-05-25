using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void DepositResource()
        {
        }

        public void PlayResourceParticles()
        {
            if (_particleSystem)
            {
                _particleSystem.Play();
            }

            Debug.Log($"Воспроизведение анимации сбора для ресурса по адресу {transform.position}");
        }
    }
}