using System;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private int _totalResourcesDeposited = 0;
        public string baseID;

        public static event Action<string, int> OnResourceDeposited;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void DepositResource()
        {
            _totalResourcesDeposited++;
            Debug.Log($"База {baseID}: Общее количество ресурсов: {_totalResourcesDeposited}");
            OnResourceDeposited?.Invoke(baseID, _totalResourcesDeposited);
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