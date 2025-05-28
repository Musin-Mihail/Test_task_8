using System.Collections.Generic;
using Drones;
using TMPro;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class DroneStateManager : MonoBehaviour
    {
        public static DroneStateManager Instance { get; set; }
        private readonly Dictionary<Drone, TextMeshPro> _droneStateTexts = new();
        private readonly Vector3 _textOffset = new(0, 0, 1.5f);
        public GameObject stateTextPrefab;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddDrone(Drone drone)
        {
            var stateTextGo = Instantiate(stateTextPrefab, drone.transform.position + _textOffset, Quaternion.identity, drone.transform);
            var textMesh = stateTextGo.GetComponent<TextMeshPro>();
            _droneStateTexts.Add(drone, textMesh);
        }

        private void Update()
        {
            foreach (var (drone, textMesh) in _droneStateTexts)
            {
                if (drone && drone.showState && textMesh)
                {
                    textMesh.gameObject.SetActive(true);
                    textMesh.transform.position = drone.transform.position + _textOffset;
                    textMesh.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    textMesh.text = GetTranslatedStatus(drone.State);
                }
                else
                {
                    textMesh.gameObject.SetActive(false);
                }
            }
        }

        private string GetTranslatedStatus(Enums.DroneState state)
        {
            return state switch
            {
                Enums.DroneState.SearchingForResource => "Поиск",
                Enums.DroneState.MovingToResource => "К ресурсу",
                Enums.DroneState.CollectingResource => "Сбор",
                Enums.DroneState.DeliveringResource => "Доставка",
                _ => ""
            };
        }
    }
}