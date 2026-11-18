using UnityEngine;

// Кладётся на пустой объект в КАЖДОЙ игровой сцене (Level1, Level2, Level3_Boss).
// В поле Level перетаскиваешь нужный LevelData. Остальные скрипты берут числа отсюда.
public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public LevelData level;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Новое прохождение начинается с первого уровня - очки с нуля
        if (level.levelNumber == 1)
        {
            SaveManager.instance.runScore = 0;
        }
    }

    // Вызывается, когда уровень пройден (выход на Level1/2, смерть босса)
    public void CompleteLevel(int levelScore)
    {
        SaveManager saveManager = SaveManager.instance;

        saveManager.runScore = saveManager.runScore + levelScore;
        saveManager.SubmitScore(saveManager.runScore);
        saveManager.UnlockLevel(level.levelNumber + 1);
        saveManager.Save();   // сохраняем после уровня
    }
}
