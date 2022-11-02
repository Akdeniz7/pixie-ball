using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartUI;
    private TopunKodu player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TopunKodu>();
    }

    private void Update()
    {
        HeartUI.sprite = HeartSprites[player.currentPlayerHealth];
    }
}
