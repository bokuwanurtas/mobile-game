using UnityEngine;

// Patrol -> Chase -> Windup -> Cooldown
public class PatrollerEnemy : EnemyBase
{
    enum State { Patrol, Chase, Windup, Cooldown }
    State state = State.Patrol;

    [Header("Movement")]
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3f;
    public Transform[] waypoints;      // точки патруля (отдельные объекты в сцене)
    public float patrolRange = 3f;     // если точек нет: ходим +- от старта

    [Header("Attack")]
    public float attackRange = 1.2f;
    public float windup = 0.6f;
    public float cooldown = 1.5f;
    public int damage = 1;
    public float loseSightTime = 2f;   // сколько ещё ищем игрока после потери

    float[] stops;
    int stopIndex;
    float timer;
    float lastSeenX;

    void Start()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            stops = new float[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
                stops[i] = waypoints[i].position.x;
        }
        else
        {
            stops = new float[] { transform.position.x - patrolRange, transform.position.x + patrolRange };
        }
    }

    void Update()
    {
        if (player == null) return;

        bool sees = CanSeePlayer();
        if (sees) lastSeenX = player.position.x;
        timer -= Time.deltaTime;

        switch (state)
        {
            case State.Patrol:
                if (Walk(stops[stopIndex], patrolSpeed))
                    stopIndex = (stopIndex + 1) % stops.Length;
                if (sees) SetState(State.Chase);
                break;

            case State.Chase:
                Walk(lastSeenX, chaseSpeed);
                if (sees) timer = loseSightTime;

                if (sees && Vector2.Distance(transform.position, player.position) <= attackRange)
                    SetState(State.Windup);
                else if (timer <= 0)
                    SetState(State.Patrol);
                break;

            case State.Windup:
                Stop();
                if (timer <= 0)
                {
                    Strike();
                    SetState(State.Cooldown);
                }
                break;

            case State.Cooldown:
                Stop();
                if (timer <= 0)
                    SetState(sees ? State.Chase : State.Patrol);
                break;
        }
    }

    void SetState(State s)
    {
        state = s;
        Telegraph(s == State.Windup);

        if (s == State.Chase) timer = loseSightTime;
        if (s == State.Cooldown) timer = cooldown;
        if (s == State.Windup)
        {
            Stop();
            FaceTowards(player.position.x);
            timer = windup;
        }
    }

    // Удар попадает, только если игрок не успел отойти
    void Strike()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
            DamagePlayer(damage);
    }

    // Идём к targetX. true = дошли
    bool Walk(float targetX, float speed)
    {
        float dx = targetX - transform.position.x;

        if (Mathf.Abs(dx) < 0.2f)
        {
            Stop();
            return true;
        }

        Face(dx > 0 ? 1 : -1);
        rb.linearVelocity = new Vector2(facing * speed, rb.linearVelocity.y);
        return false;
    }
}
