using UnityEngine;

// Данные игрока. Create -> Game Data -> Player Stats
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game Data/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public float speed = 7f;
    public float fallLimitY = -15f;   // ниже этой высоты игрок "упал"

    [Header("Health")]
    public int maxHp = 3;
    public int blinkCount = 10;       // сколько раз мигаем после удара
    public float blinkDelay = 0.2f;

    [Header("Shooting")]
    public float shootCooldown = 0.3f;
    public float bulletSpeed = 12f;
    public float bulletLifetime = 3f;
    public int bulletDamage = 1;

    [Header("Score")]
    public int coinPoints = 10;       // очков за одну монетку
}
