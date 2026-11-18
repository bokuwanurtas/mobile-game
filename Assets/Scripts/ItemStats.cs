using UnityEngine;

// Данные падающих предметов: камни, бомбы, сердечки. Create -> Game Data -> Item Stats
[CreateAssetMenu(fileName = "ItemStats", menuName = "Game Data/Item Stats")]
public class ItemStats : ScriptableObject
{
    public int hazardDamage = 1;       // камень
    public int bombDamage = 2;
    public int healAmount = 1;         // сердечко
    public float hazardLifetime = 10f; // через сколько секунд предмет исчезает
    public float bombFuseTime = 2f;    // до взрыва бомбы
    public float explosionTime = 0.7f; // сколько живёт взрыв
}
