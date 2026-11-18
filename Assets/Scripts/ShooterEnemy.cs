using UnityEngine;

// Idle (оглядывается) -> Aim (красная линия) -> Cooldown
public class ShooterEnemy : EnemyBase
{
    enum State { Idle, Aim, Cooldown }
    State state = State.Idle;

    [Header("Attack")]
    public GameObject projectilePrefab;   // префаб BossBullets
    public AudioClip shootClip;

    [Header("Aim line")]
    public LineRenderer aimLine;

    Vector2 aimDir;
    float timer;

    void Start()
    {
        // линию отцепляем, чтобы её не масштабировал враг
        if (aimLine != null) aimLine.transform.SetParent(null);
        SetState(State.Idle);
    }

    void OnDestroy()
    {
        if (aimLine != null) Destroy(aimLine.gameObject);
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;

        switch (state)
        {
            case State.Idle:
                if (timer <= 0)
                {
                    Face(-facing);
                    timer = stats.turnInterval;
                }
                if (CanSeePlayer()) SetState(State.Aim);
                break;

            case State.Aim:
                DrawAimLine();
                if (timer <= 0)
                {
                    Fire();
                    SetState(State.Cooldown);
                }
                break;

            case State.Cooldown:
                if (timer <= 0) SetState(State.Idle);
                break;
        }
    }

    void SetState(State s)
    {
        state = s;
        Telegraph(s == State.Aim);
        if (aimLine != null) aimLine.enabled = (s == State.Aim);

        if (s == State.Idle) timer = stats.turnInterval;
        if (s == State.Cooldown) timer = stats.cooldown;
        if (s == State.Aim)
        {
            // направление фиксируем в начале прицеливания: игрок успевает уйти
            aimDir = (player.position - EyePos()).normalized;
            Face(aimDir.x >= 0 ? 1 : -1);
            timer = stats.aimTime;
        }
    }

    Vector3 EyePos()
    {
        return transform.position + Vector3.up * eyeHeight;
    }

    void DrawAimLine()
    {
        if (aimLine == null) return;
        aimLine.SetPosition(0, EyePos());
        aimLine.SetPosition(1, EyePos() + (Vector3)(aimDir * stats.aimLineLength));
    }

    void Fire()
    {
        Vector3 pos = EyePos() + (Vector3)(aimDir * 0.6f);
        GameObject b = Instantiate(projectilePrefab, pos, Quaternion.identity);

        BossProjectile p = b.GetComponent<BossProjectile>();
        p.direction = aimDir;
        p.speed = stats.projectileSpeed;
        p.damage = stats.damage;

        Destroy(b, stats.projectileLife);
        Play(shootClip);
    }
}
