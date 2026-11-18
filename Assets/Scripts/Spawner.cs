using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Hazard[] prefabs;

    LevelData level;

    void Start()
    {
        level = LevelController.instance.level;
        InvokeRepeating(nameof(Spawn), level.spawnFirstDelay, level.spawnInterval);
    }

    void Spawn()
    {
        Hazard pick = prefabs[Random.Range(0, prefabs.Length)];

        Instantiate(
            pick,
            new Vector3(Random.Range(level.spawnMinX, level.spawnMaxX), level.spawnY, 0),
            Quaternion.identity
        );
    }
}
