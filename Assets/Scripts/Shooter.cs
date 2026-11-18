using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // перетащи префаб пули в инспекторе
    PlayerStats stats;
    float nextShot;                 // время, когда можно стрелять снова

    SpriteRenderer sr;

    public AudioClip shootClip;
    AudioSource src;

    private PlayerInput playerInput;
    private InputAction fireAction;

    void Start()
    {
        stats = GetComponent<Player2D>().stats;
        sr = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
    }

    void Update()
    {
        if (fireAction.WasPressedThisFrame() && Time.time >= nextShot){
            nextShot = Time.time + stats.shootCooldown;

            Quaternion rotation = sr.flipX
                ? Quaternion.Euler(0, 0, 180)
                : Quaternion.identity;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.GetComponent<Bullet>().stats = stats;
            src.PlayOneShot(shootClip);
        }
    }
}



