using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxEnemyHealth;
    public float currentEnemyHealth;
    internal bool gotDamage;
    public float damage;
    TopunKodu player;
    public GameObject deathParticle;
    SpriteRenderer graph;
    CircleCollider2D cir2D;
    Rigidbody2D body2D;
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        player = FindObjectOfType<TopunKodu>();
        graph = GetComponent<SpriteRenderer>();
        cir2D = GetComponent<CircleCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            graph.enabled = false;
            cir2D.enabled = false;
            deathParticle.SetActive(true);
            body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            Destroy(gameObject,1);
        }
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerItem"&& player.canDamage)
        {
            currentEnemyHealth -= damage;

        }
    }
}
