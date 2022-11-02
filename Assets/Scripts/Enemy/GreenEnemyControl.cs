using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyControl : MonoBehaviour
{
    Rigidbody2D enemyBody2D;
    public float enemySpeed;
    //Çarpacağı duvarı bulma
    bool isGrounded;
    Transform groundCheck;
    const float GroundCheckRadius = 0.2f;
    [Tooltip("Duvarın ne olduğunu belirler.")]
    public LayerMask groundLayer;
    public bool moveRight;
    EnemyHealth enemyHealth;
    Animator GreenEnemyAnim;
    //Bosluğa düşmeme
    bool onEdge;
    Transform edgeCheck;

    void Start()
    {
        enemyBody2D = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        edgeCheck = transform.Find("EdgeCheck");
        GreenEnemyAnim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        
        // Duvara değip değmemeyi kontrol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, groundLayer);
        onEdge = Physics2D.OverlapCircle(edgeCheck.position, GroundCheckRadius, groundLayer);
        if (isGrounded || !onEdge)
            moveRight = !moveRight;

        enemyBody2D.velocity = (moveRight) ? new Vector2(enemySpeed, 0) : new Vector2(-enemySpeed, 0);
        transform.localScale= (moveRight) ? new Vector2(-0.45f,0.45f) : new Vector2(0.45f,0.45f);
    }
}
