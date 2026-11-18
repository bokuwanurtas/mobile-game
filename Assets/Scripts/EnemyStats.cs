using UnityEngine;

// Данные врага. Один класс, три ассета: ChargerStats, PatrollerStats, ShooterStats.
// Поля, которые тип врага не использует, просто игнорируются.
// Create -> Game Data -> Enemy Stats
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Game Data/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("General")]
    public int maxHp = 2;
    public int damage = 1;
    public float hitBlinkTime = 0.06f;
    public float turnInterval = 2.5f;     // как часто оглядывается (Charger, Shooter)

    [Header("Vision")]
    public float visionRange = 6f;
    [Range(10, 360)] public float visionAngle = 120f;

    [Header("Attack timing")]
    public float windup = 0.7f;           // Charger, Patroller: замах
    public float aimTime = 0.6f;          // Shooter: прицеливание
    public float cooldown = 1.5f;         // Patroller, Shooter

    [Header("Charger")]
    public float chargeSpeed = 9f;
    public float chargeTime = 0.8f;
    public float stunTime = 1.5f;

    [Header("Patroller")]
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3f;
    public float patrolRange = 3f;
    public float attackRange = 1.2f;
    public float loseSightTime = 2f;

    [Header("Shooter")]
    public float projectileSpeed = 6f;
    public float projectileLife = 3f;
    public float aimLineLength = 6f;
}
