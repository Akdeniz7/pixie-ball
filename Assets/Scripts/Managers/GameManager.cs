using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    TopunKodu player;
    public TextMeshProUGUI points;
    void Start()
    {
        player = FindObjectOfType<TopunKodu>();

    }

    
    void Update()
    {
        points.text = "Score : " + player.currentPoints.ToString();
        if (player.isDead)
        {
            Invoke("RestartGame", 2);

        }
    }
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
}
