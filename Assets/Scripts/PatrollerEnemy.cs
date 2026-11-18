using UnityEngine;

// Patrol -> Chase -> Windup -> Cooldown
public class PatrollerEnemy : EnemyBase
{
    enum State { Patrol, Chase, Windup, Cooldown }
    State state = State.Patrol;

    [Header("Movement")]
    public Transform[] waypoints;      // точки патруля (отдельные объекты в сцене)

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
            stops = new float[] { transform.position.x - stats.patrolRange, transform.position.x + stats.patrolRange };
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
                if (Walk(stops[stopIndex], stats.patrolSpeed))
                    stopIndex = (stopIndex + 1) % stops.Length;
                if (sees) SetState(State.Chase);
                break;

            case State.Chase:
                Walk(lastSeenX, stats.chaseSpeed);
                if (sees) timer = stats.loseSightTime;

                if (sees && Vector2.Distance(transform.position, player.position) <= stats.attackRange)
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

        if (s == State.Chase) timer = stats.loseSightTime;
        if (s == State.Cooldown) timer = stats.cooldown;
        if (s == State.Windup)
        {
            Stop();
            FaceTowards(player.position.x);
            timer = stats.windup;
        }
    }

    // Удар попадает, только если игрок не успел отойти
    void Strike()
    {
        if (Vector2.Distance(transform.position, player.position) <= stats.attackRange)
            DamagePlayer(stats.damage);
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
