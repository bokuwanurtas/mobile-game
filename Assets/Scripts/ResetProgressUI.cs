using UnityEngine;

// Кнопка "Reset progress" и панель подтверждения "Are you sure?"
public class ResetProgressUI : MonoBehaviour
{
    public GameObject confirmPanel;
    public LevelSelectMenu levelSelectMenu;   // чтобы после сброса снова закрылись уровни

    void Start()
    {
        confirmPanel.SetActive(false);
    }

    // Кнопка "Reset progress"
    public void OpenConfirm()
    {
        confirmPanel.SetActive(true);
    }

    // Кнопка "Yes"
    public void ConfirmReset()
    {
        SaveManager.instance.ResetProgress();
        confirmPanel.SetActive(false);
        levelSelectMenu.Refresh();
    }

    // Кнопка "No"
    public void CancelReset()
    {
        confirmPanel.SetActive(false);
    }
}
