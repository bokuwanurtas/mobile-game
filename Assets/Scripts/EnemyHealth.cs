using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 2;
    public AudioClip hitClip;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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
        yield return new WaitForSeconds(0.06f);
        sr.enabled = true;
    }
}
