using UnityEngine;

// Общая база для врагов: глаза (дальность + угол + Raycast), поворот, красная вспышка перед атакой
public class EnemyBase : MonoBehaviour
{
    [Header("Vision")]
    public float visionRange = 6f;                 // меньше, чем пол-экрана, чтобы не бить из-за кадра
    [Range(10, 360)] public float visionAngle = 120f;
    public float eyeHeight = 0.3f;

    [Header("Look")]
    public bool artFacesRight = true;              // куда смотрит спрайт по умолчанию
    public Color telegraphColor = Color.red;       // цвет перед атакой
    public AudioClip windupClip;                   // звук замаха

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Transform player;
    protected Health playerHealth;
    protected int facing = 1;                      // 1 = вправо, -1 = влево

    AudioSource src;
    Color normalColor;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        normalColor = sr.color;

        // ищем игрока один раз, не в Update
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
            playerHealth = p.GetComponent<Health>();
        }
    }

    // Видит ли игрока: на экране + дистанция + угол + луч (стены блокируют)
    protected bool CanSeePlayer()
    {
        if (player == null || !sr.isVisible) return false;

        Vector2 eye = (Vector2)transform.position + Vector2.up * eyeHeight;
        Vector2 toPlayer = (Vector2)player.position - eye;

        if (toPlayer.magnitude > visionRange) return false;
        if (Vector2.Angle(Vector2.right * facing, toPlayer) > visionAngle / 2f) return false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(eye, toPlayer, visionRange);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.isTrigger) continue;                    // монеты, пули
            if (hit.collider.gameObject == gameObject) continue;     // сам враг
            return hit.collider.CompareTag("Player");                // первое препятствие: игрок или стена
        }
        return false;
    }

    protected void Face(int dir)
    {
        if (dir == 0) return;
        facing = dir;
        sr.flipX = artFacesRight ? dir < 0 : dir > 0;
    }

    protected void FaceTowards(float x)
    {
        Face(x >= transform.position.x ? 1 : -1);
    }

    protected void Stop()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    // Красный цвет + звук = игрок видит замах
    protected void Telegraph(bool on)
    {
        sr.color = on ? telegraphColor : normalColor;
        if (on) Play(windupClip);
    }

    protected void Play(AudioClip clip)
    {
        if (src != null && clip != null) src.PlayOneShot(clip);
    }

    protected void DamagePlayer(int dmg)
    {
        if (playerHealth != null) playerHealth.TakeDamage(dmg);
    }

    // Жёлтый конус зрения (выдели врага в сцене)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 eye = transform.position + Vector3.up * eyeHeight;
        Vector3 forward = Vector3.right * facing;
        float half = visionAngle / 2f;
        Gizmos.DrawLine(eye, eye + Quaternion.Euler(0, 0, half) * forward * visionRange);
        Gizmos.DrawLine(eye, eye + Quaternion.Euler(0, 0, -half) * forward * visionRange);
    }
}
