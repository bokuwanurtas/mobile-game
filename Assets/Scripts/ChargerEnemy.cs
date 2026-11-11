using UnityEngine;

// Idle (оглядывается) -> Windup (красный) -> Charge (рывок) -> Stunned
public class ChargerEnemy : EnemyBase
{
    enum State { Idle, Windup, Charge, Stunned }
    State state = State.Idle;

    [Header("Attack")]
    public float windup = 0.7f;
    public float chargeSpeed = 9f;
    public float chargeTime = 0.8f;
    public float stunTime = 1.5f;
    public int damage = 1;
    public float turnInterval = 2.5f;

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
                    timer = turnInterval;
                }
                if (CanSeePlayer()) SetState(State.Windup);
                break;

            case State.Windup:
                Stop();
                if (timer <= 0) SetState(State.Charge);
                break;

            case State.Charge:
                rb.linearVelocity = new Vector2(chargeDir * chargeSpeed, rb.linearVelocity.y);

                bool hitWall = chargeTime - timer > 0.15f && Mathf.Abs(rb.linearVelocity.x) < 0.5f;
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

        if (s == State.Idle) timer = turnInterval;
        if (s == State.Stunned) timer = stunTime;
        if (s == State.Charge) timer = chargeTime;
        if (s == State.Windup)
        {
            FaceTowards(player.position.x);
            chargeDir = facing;               // направление рывка фиксируем в начале замаха
            timer = windup;
        }
    }

    // Урон только во время рывка
    void OnCollisionStay2D(Collision2D other)
    {
        if (state == State.Charge && other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(damage);
            SetState(State.Stunned);
        }
    }
}
