using UnityEngine;

// Данные одного уровня. Три штуки: Level_01, Level_02, Level_03
// Create -> Game Data -> Level Data
[CreateAssetMenu(fileName = "Level_01", menuName = "Game Data/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Info")]
    public int levelNumber = 1;
    public string levelName = "Level 1";
    public string sceneName = "Level1";   // имя сцены в Build Profiles

    [Header("Goal")]
    public float survivalTime = 20f;      // сколько секунд надо продержаться (0 - если нет таймера)

    [Header("Player")]
    public float jumpForce = 12f;

    [Header("Falling hazards (Spawner)")]
    public float spawnFirstDelay = 1f;
    public float spawnInterval = 2f;
    public float spawnMinX = -9.48f;
    public float spawnMaxX = 16.88f;
    public float spawnY = 6f;
}
