using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float distance = 0.7f;

    BossHealth bossHealth;
    Animator anim;


    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        InvokeRepeating(nameof(Shoot), 1f, 2f);
        anim = GetComponent<Animator>();
    }

    void Shoot(){
        if (anim != null){
            anim.SetTrigger("Attack");
        }
        int bulletCount;

        if (bossHealth.hp > 4)
        {
            bulletCount = 2;
        }
        else
        {
            bulletCount = 4;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            float y = (i - (bulletCount - 1) / 2f) * distance;

            Vector3 position = transform.position
                + Vector3.left * 1.5f
                + Vector3.up * y;

            Instantiate(projectilePrefab, position, Quaternion.identity);
        }
    }
}
