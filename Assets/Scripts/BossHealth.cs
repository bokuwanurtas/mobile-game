using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BossHealth : MonoBehaviour
{
    public int hp = 8;
    public TextMeshProUGUI hpText;

    public void Start(){
        UpdateHP();
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHP();

        if (hp <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
    public void UpdateHP(){
        hpText.text = "Boss HP: " + hp + "/8";
    }
}
