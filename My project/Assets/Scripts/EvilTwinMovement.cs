using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTwinMovement : MonoBehaviour
{
    public float chaseSpeed = 1f;
    private float moveDirection = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isChasing = false;
    private Transform player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Gems.hasPurpleGem) // Hero mücevheri aldıysa Evil Twin kovalamaya başlasın
        {
            chaseSpeed = 0.5f; // Evil Twin hızlansın
            isChasing = true;
            animator.SetBool("IsChasing", true);
            // X ve Y ekseni serbest bırak, Z ekseni sabit kalsın
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            moveDirection = (player.position.x > transform.position.x) ? 1f : -1f;
            spriteRenderer.flipX = (moveDirection < 0);
            rb.velocity = new Vector2(moveDirection * chaseSpeed, rb.velocity.y);
        }
    }
}
