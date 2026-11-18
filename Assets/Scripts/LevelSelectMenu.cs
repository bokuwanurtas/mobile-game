using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Выбор уровня в MainMenu: закрытые уровни нельзя нажать
public class LevelSelectMenu : MonoBehaviour
{
    public LevelData[] levels;            // Level_01, Level_02, Level_03 по порядку
    public Button[] levelButtons;         // три кнопки по порядку
    public GameObject[] lockIcons;        // значки замка (можно оставить пустым)
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool open = SaveManager.instance.IsUnlocked(levels[i].levelNumber);
            levelButtons[i].interactable = open;

            if (lockIcons != null && i < lockIcons.Length && lockIcons[i] != null)
            {
                lockIcons[i].SetActive(open == false);
            }
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = "Best: " + SaveManager.instance.data.bestScore;
        }
    }

    // Кнопке в OnClick передай номер по порядку: 0, 1 или 2
    public void PlayLevel(int index)
    {
        if (SaveManager.instance.IsUnlocked(levels[index].levelNumber) == false)
        {
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(levels[index].sceneName);
    }
}
