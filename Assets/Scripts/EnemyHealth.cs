using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp;
    public AudioClip hitClip;

    SpriteRenderer sr;
    EnemyStats stats;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        stats = GetComponent<EnemyBase>().stats;
        hp = stats.maxHp;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hitClip != null)
            AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position);

        if (hp <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        sr.enabled = false;
        yield return new WaitForSeconds(stats.hitBlinkTime);
        sr.enabled = true;
    }
}
