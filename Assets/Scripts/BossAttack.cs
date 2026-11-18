using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;

    BossHealth bossHealth;
    BossStats stats;
    Animator anim;


    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        stats = bossHealth.stats;
        InvokeRepeating(nameof(Shoot), stats.shootFirstDelay, stats.shootInterval);
        anim = GetComponent<Animator>();
    }

    void Shoot(){
        if (anim != null){
            anim.SetTrigger("Attack");
        }
        int bulletCount;

        if (bossHealth.hp > stats.phase2Hp)
        {
            bulletCount = stats.bulletsPhase1;
        }
        else
        {
            bulletCount = stats.bulletsPhase2;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            float y = (i - (bulletCount - 1) / 2f) * stats.bulletSpacing;

            Vector3 position = transform.position
                + Vector3.left * stats.bulletOffsetX
                + Vector3.up * y;

            GameObject bullet = Instantiate(projectilePrefab, position, Quaternion.identity);

            BossProjectile p = bullet.GetComponent<BossProjectile>();
            p.speed = stats.bulletSpeed;
            p.damage = stats.bulletDamage;
        }
    }
}
