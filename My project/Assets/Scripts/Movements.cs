using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Varsayılan hız
    public float baseSpeed = 0.5f; // İlk hız
    public float blueGemBoost = 0.3f; // Mavi mücevher hız artırıcı
    public float redGemSlowdown = -0.5f; // Kırmızı mücevher hız azaltıcı

    private float moveDirection = 0.0f; // Karakterin yönü (-1: sola, 1: sağa)
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = true; // Zemin kontrolü
    private bool jump;
    public float jumpForce = 2.0f; // Zıplama kuvveti

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mücevher durumu kontrolü
        UpdateSpeed();

        // Sağ hareket
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1.0f;
            spriteRenderer.flipX = false;
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        // Sol hareket
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1.0f;
            spriteRenderer.flipX = true;
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        // Zıplama
        else if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jump = true;
            isGrounded = false;
        }
        // Hareket yok
        else
        {
            moveDirection = 0.0f;
            animator.SetFloat("Speed", 0.0f);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Zıplama kuvveti
        if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (spriteRenderer.flipX)
                rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            else
                rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);

            jump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    // Mücevherlere bağlı olarak hızı güncelle
    void UpdateSpeed()
    {
        moveSpeed = baseSpeed; // Başlangıç hızını sıfırla

        if (Gems.hasBlueGem) 
            moveSpeed += blueGemBoost; // Mavi mücevher hız artışı
        
        if (Gems.hasRedGem) 
            moveSpeed += redGemSlowdown; // Kırmızı mücevher hız azaltma
    }
}
