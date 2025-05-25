using Managers;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuUI : MonoBehaviour
{
    public Button exitToMenuButton;

    private void Start()
    {
        if (exitToMenuButton)
        {
            exitToMenuButton.onClick.AddListener(ExitToMenu);
        }
    }

    private void ExitToMenu()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.ExitToMenu();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null. Невозможно выйти в меню.");
        }
    }
}