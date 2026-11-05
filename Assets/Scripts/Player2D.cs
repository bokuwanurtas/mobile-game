using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class Player2D : MonoBehaviour
{

    public GameObject portal;

    public float speed = 7f;
    private Rigidbody2D rb;

    public float jumpForce = 12f;
    private bool onGround;

    private Animator anim;
    private SpriteRenderer sr;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    // Coin
    private int coins;
    private int coinsTotal;
    public TextMeshProUGUI coinsText;

    public SurvivalTimer survivalTimer;

    public AudioClip pickupClip;
    AudioSource src;

    void Start()
    {
        coinsTotal = GameObject.FindGameObjectsWithTag("Coin").Length;

        if (portal != null){
            portal.SetActive(false);
        }

        onGround = true;
        anim.SetBool("Grounded", true);

        src = GetComponent<AudioSource>();
        UpdateCoinsText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            UpdateCoinsText();
            src.PlayOneShot(pickupClip);
        }

        
        if (other.CompareTag("Exit"))
        {
            if (coins < coinsTotal)
            {
                Debug.Log("Collect all coins first! " + coins + " / " + coinsTotal);
            }
            else if (!survivalTimer.TimeCompleted)
            {
                Debug.Log("Survive " + survivalTimer.goal + " seconds first!");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }

    // Jump and check onGround
    void Update()
    {
        if (jumpAction.WasPressedThisFrame() && onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            onGround = false;
            anim.SetBool("Grounded", false);
        }
        
        if (survivalTimer != null && portal != null)
        {
            if (coins >= coinsTotal && survivalTimer.TimeCompleted
                && !portal.activeSelf)
            {
                portal.SetActive(true);
                Debug.Log("PORTAL OPENED!");
            }
        }
        CheckFall();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")){
            onGround = true;
            anim.SetBool("Grounded", true);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Move to the left and right
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void FixedUpdate()
    {
        float x = moveAction.ReadValue<Vector2>().x;

        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);

        anim.SetBool("isRunning", x != 0);

        if (x != 0)
            sr.flipX = x < 0;
    }

    void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = coins + " / " + coinsTotal;
        }
    }

    void CheckFall()
    {
        if (transform.position.y < -15f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}