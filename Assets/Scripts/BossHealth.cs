using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BossHealth : MonoBehaviour
{
    public BossStats stats;   // BossStats.asset
    public int hp;
    public TextMeshProUGUI hpText;

    public void Start(){
        hp = stats.maxHp;
        UpdateHP();
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHP();

        if (hp <= 0)
        {
            LevelController.instance.CompleteLevel(stats.killScore);
            SceneManager.LoadScene("WinScreen");
        }
    }
    public void UpdateHP(){
        hpText.text = "Boss HP: " + hp + "/" + stats.maxHp;
    }
}
