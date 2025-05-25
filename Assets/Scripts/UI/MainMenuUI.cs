using Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Слайдеры")] public Slider dronesPerFactionSlider;
    public Text dronesPerFactionValueText;
    public Slider droneSpeedSlider;
    public Text droneSpeedValueText;

    [Header("Поля ввода")] public InputField resourceSpawnFrequencyInputField;

    [Header("Переключатели")] public Toggle drawDronePathToggle;

    [Header("Кнопки")] public Button startGameButton;
    public Button quitGameButton;

    private void Start()
    {
        if (GameManager.Instance)
        {
            dronesPerFactionSlider.value = GameManager.Instance.NumberOfDronesPerFaction;
            droneSpeedSlider.value = GameManager.Instance.DroneSpeed;
            resourceSpawnFrequencyInputField.text = GameManager.Instance.ResourceSpawnFrequency.ToString();
            drawDronePathToggle.isOn = GameManager.Instance.DrawDronePath;
        }
        else
        {
            dronesPerFactionSlider.value = 5;
            droneSpeedSlider.value = 3;
            resourceSpawnFrequencyInputField.text = "0.5";
            drawDronePathToggle.isOn = true;
        }

        UpdateDronePerFactionText();
        UpdateDroneSpeedText();

        dronesPerFactionSlider.onValueChanged.AddListener(delegate { UpdateDronePerFactionText(); });
        dronesPerFactionSlider.onValueChanged.AddListener(delegate { SetDronesPerFaction(dronesPerFactionSlider.value); });

        droneSpeedSlider.onValueChanged.AddListener(delegate { UpdateDroneSpeedText(); });
        droneSpeedSlider.onValueChanged.AddListener(delegate { SetDroneSpeed(droneSpeedSlider.value); });

        resourceSpawnFrequencyInputField.onEndEdit.AddListener(delegate { SetResourceSpawnFrequency(resourceSpawnFrequencyInputField.text); });

        drawDronePathToggle.onValueChanged.AddListener(delegate { SetDrawDronePath(drawDronePathToggle.isOn); });

        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    private void UpdateDronePerFactionText()
    {
        dronesPerFactionValueText.text = dronesPerFactionSlider.value.ToString();
    }

    private void UpdateDroneSpeedText()
    {
        droneSpeedValueText.text = droneSpeedSlider.value.ToString("F1");
    }

    private void SetDronesPerFaction(float value)
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.NumberOfDronesPerFaction = (int)value;
        }
    }

    private void SetDroneSpeed(float value)
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.DroneSpeed = value;
        }
    }

    private void SetResourceSpawnFrequency(string value)
    {
        if (GameManager.Instance)
        {
            if (float.TryParse(value, out var frequency))
            {
                GameManager.Instance.ResourceSpawnFrequency = frequency;
            }
            else
            {
                Debug.LogWarning("Недопустимый ввод для частоты спавна ресурсов.");
            }
        }
    }

    private void SetDrawDronePath(bool isOn)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DrawDronePath = isOn;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}