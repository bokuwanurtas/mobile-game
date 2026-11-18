using UnityEngine;

// Idle (оглядывается) -> Windup (красный) -> Charge (рывок) -> Stunned
public class ChargerEnemy : EnemyBase
{
    enum State { Idle, Windup, Charge, Stunned }
    State state = State.Idle;

    float timer;
    int chargeDir;

    void Start()
    {
        SetState(State.Idle);
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;

        switch (state)
        {
            case State.Idle:
                Stop();
                if (timer <= 0)
                {
                    Face(-facing);
                    timer = stats.turnInterval;
                }
                if (CanSeePlayer()) SetState(State.Windup);
                break;

            case State.Windup:
                Stop();
                if (timer <= 0) SetState(State.Charge);
                break;

            case State.Charge:
                rb.linearVelocity = new Vector2(chargeDir * stats.chargeSpeed, rb.linearVelocity.y);

                bool hitWall = stats.chargeTime - timer > 0.15f && Mathf.Abs(rb.linearVelocity.x) < 0.5f;
                if (timer <= 0 || hitWall) SetState(State.Stunned);
                break;

            case State.Stunned:
                Stop();
                if (timer <= 0) SetState(State.Idle);
                break;
        }
    }

    void SetState(State s)
    {
        state = s;
        Telegraph(s == State.Windup);

        if (s == State.Idle) timer = stats.turnInterval;
        if (s == State.Stunned) timer = stats.stunTime;
        if (s == State.Charge) timer = stats.chargeTime;
        if (s == State.Windup)
        {
            FaceTowards(player.position.x);
            chargeDir = facing;               // направление рывка фиксируем в начале замаха
            timer = stats.windup;
        }
    }

    // Урон только во время рывка
    void OnCollisionStay2D(Collision2D other)
    {
        if (state == State.Charge && other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(stats.damage);
            SetState(State.Stunned);
        }
    }
}
