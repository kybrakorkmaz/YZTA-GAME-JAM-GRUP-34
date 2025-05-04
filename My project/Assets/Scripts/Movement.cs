using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 4f;  // Sabit yatay hız
    public float jumpForce = 0.5f;  // Zıplama kuvveti
    public float jumpHorizontalBoost = 2f; // Zıplarken yatay hız artışı

    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded = false; // Zeminle temas kontrolü

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = 0;

        // Sağa-sola hareket kontrolü
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveInput = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveInput = 1;

        // Zıplama işlemi: Zeminle temas olduğu sürece zıplama yapılabilir
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            isGrounded = false; // Zıpladıktan sonra zemin temasını kapat

            // Zıplama kuvveti uygula ve hareket yönüne bağlı olarak yatay hız ekle
            rb.velocity = new Vector2(moveInput * jumpHorizontalBoost, jumpForce);
        }

        // Rigidbody ile hareketi kontrol et (sağa-sola hareket)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Karakterin dönüşünü sıfırla (dönme hareketi olmasın)
        transform.rotation = Quaternion.identity;

        // Animasyonları güncelle
        animator.SetFloat("Speed", Mathf.Abs(moveInput * moveSpeed));  // Yatay hareket animasyonu
        animator.SetBool("IsJumping", !isGrounded); // Zıplama animasyonu

        // Yön değiştirme animasyonu: Sağ veya sol hareket ediyorsa
        if (moveInput > 0) // Sağ hareket
        {
            animator.SetBool("IsWalkingRight", true); // Sağ yürüme animasyonunu aktif et
            animator.SetBool("IsWalkingLeft", false); // Sol yürüme animasyonunu kapat
        }
        else if (moveInput < 0) // Sol hareket
        {
            animator.SetBool("IsWalkingLeft", true); // Sol yürüme animasyonunu aktif et
            animator.SetBool("IsWalkingRight", false); // Sağ yürüme animasyonunu kapat
        }
        else // Hareket etmiyorsa
        {
            animator.SetBool("IsWalkingLeft", false); // Sol yürüme animasyonunu kapat
            animator.SetBool("IsWalkingRight", false); // Sağ yürüme animasyonunu kapat
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer zeminle temas varsa
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Zeminle temas edildiğinde zıplama tekrar aktif olur
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Eğer zeminle temas kesilirse
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Zeminle temas kesildiğinde zıplama devre dışı kalır
        }
    }
}