using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int hp = 3;

    bool invincible;
    SpriteRenderer sr;

    public AudioClip hurtClip;
    AudioSource src;

    public TextMeshProUGUI hpText;

    public Image[] hearts;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        UpdateHearts();
    }

    public void TakeDamage(int dmg)
    {
        if (invincible) return;

        hp -= dmg;
        UpdateHearts();
        src.PlayOneShot(hurtClip);

        StartCoroutine(Invincibility());

        if (hp <= 0) GameOver();
    }

    public void Heal(int amount)
    {
        hp += amount;
        UpdateHearts();
    }

    IEnumerator Invincibility()
    {
        invincible = true;

        for (int i = 0; i < 10; i++)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        sr.enabled = true;
        invincible = false;
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hp;
        }
    }
}
