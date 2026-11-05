using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // перетащи префаб пули в инспекторе
    public float cooldown = 0.3f;   
    float nextShot;                 // время, когда можно стрелять снова

    SpriteRenderer sr;

    public AudioClip shootClip;
    AudioSource src;

    private PlayerInput playerInput;
    private InputAction fireAction;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
    }

    void Update()
    {
        if (fireAction.WasPressedThisFrame() && Time.time >= nextShot){
            nextShot = Time.time + cooldown;

            Quaternion rotation = sr.flipX
                ? Quaternion.Euler(0, 0, 180)
                : Quaternion.identity;

            Instantiate(bulletPrefab, transform.position, rotation);
            src.PlayOneShot(shootClip);
        }
    }
}



