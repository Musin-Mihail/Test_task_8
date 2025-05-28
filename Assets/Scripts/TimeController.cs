using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Button normalSpeedButton;
    [SerializeField] private Button doubleSpeedButton;
    [SerializeField] private Button tripleSpeedButton;

    private void Start()
    {
        if (normalSpeedButton)
        {
            normalSpeedButton.onClick.AddListener(() => SetTimeScale(1.0f));
        }
        else
        {
            Debug.LogError("Кнопка 'Обычная скорость' не назначена в инспекторе!");
        }

        if (doubleSpeedButton)
        {
            doubleSpeedButton.onClick.AddListener(() => SetTimeScale(2.0f));
        }
        else
        {
            Debug.LogError("Кнопка 'Двойная скорость' не назначена в инспекторе!");
        }

        if (tripleSpeedButton)
        {
            tripleSpeedButton.onClick.AddListener(() => SetTimeScale(3.0f));
        }
        else
        {
            Debug.LogError("Кнопка 'Тройной скорости' не назначена в инспекторе!");
        }

        SetTimeScale(1.0f);
    }

    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Debug.Log($"Скорость времени изменена на: {scale}");
    }
}