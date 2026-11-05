using UnityEngine;


public class BombHazard : Hazard
{
    protected override void OnHitPlayer(Health p)
    {
        p.TakeDamage(2);
    }
}
