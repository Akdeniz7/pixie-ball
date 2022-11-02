using System.Collections;
using UnityEngine;

public class TopunInput : MonoBehaviour
{
    TopunKodu player;
    
    void Start()
    {
        player = GetComponent<TopunKodu>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") )
        {
            player.Jump();
        }
    }
}
