using UnityEngine;

// Данные босса и его огненного дождя. Create -> Game Data -> Boss Stats
[CreateAssetMenu(fileName = "BossStats", menuName = "Game Data/Boss Stats")]
public class BossStats : ScriptableObject
{
    [Header("Health")]
    public int maxHp = 8;
    public int phase2Hp = 4;            // когда hp <= этого, босс стреляет больше
    public int killScore = 100;         // очки за победу над боссом

    [Header("Shooting")]
    public float shootFirstDelay = 1f;
    public float shootInterval = 2f;
    public int bulletsPhase1 = 2;
    public int bulletsPhase2 = 4;
    public float bulletSpacing = 0.7f;
    public float bulletOffsetX = 1.5f;
    public float bulletSpeed = 6f;
    public int bulletDamage = 1;

    [Header("Fire rain")]
    public float fireWarningTime = 1.5f;
    public float fireInterval = 4f;
    public float fireMinX = -10.3f;
    public float fireMaxX = 22.5f;
    public float fireGroundY = -0.2f;
    public float fireSpawnHeight = 8f;
    public float fireFallSpeed = 7f;
}
