using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    
    void Start()
    {
        Invoke(nameof(Explode), 2f);   
    }

    void Explode()
    {
        // здесь: анимация взрыва, звук, урон вокруг (урок 9 и A7)
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(explosion, 0.7f);
        Destroy(gameObject);
    }
}
