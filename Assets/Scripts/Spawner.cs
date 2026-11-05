using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Hazard[] prefabs;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 2f);
    }

    void Spawn()
    {
        Hazard pick = prefabs[Random.Range(0, prefabs.Length)];

        Instantiate(
            pick,
            new Vector3(Random.Range(-9.48f, 16.88f), 6f, 0),
            Quaternion.identity
        );
    }
}
