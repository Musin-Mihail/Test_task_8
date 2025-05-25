using Bases;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResourceCounterDisplay : MonoBehaviour
    {
        public TextMeshProUGUI resourceCountText;
        public string baseID;

        private void OnEnable()
        {
            Base.OnResourceDeposited += UpdateResourceCountDisplay;
        }

        private void OnDisable()
        {
            Base.OnResourceDeposited -= UpdateResourceCountDisplay;
        }

        private void Start()
        {
            if (!resourceCountText)
            {
                Debug.LogError($"ResourceCounterDisplay для BaseID: {baseID}: TextMeshProUGUI компонент не назначен!");
            }

            UpdateResourceCountDisplay(baseID, 0);
        }

        private void UpdateResourceCountDisplay(string id, int count)
        {
            if (id == baseID && resourceCountText != null)
            {
                resourceCountText.text = $"Ресурсов {baseID}: {count}";
            }
        }
    }
}