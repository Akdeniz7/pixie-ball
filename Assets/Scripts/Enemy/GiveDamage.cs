using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public int damage;
    TopunKodu player;
    void Start()
    {
        player = FindObjectOfType<TopunKodu>();
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Oyuncuya Zarar Ver
            player.ishurt = true;
        
        }
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Heroball")
        {
            // Oyuncuya Zarar Ver
            player.ishurt = false;

        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Heroball")
        {
            // Oyuncuya Zarar Ver
            player.ishurt = false;

        }

    }
    
}   
