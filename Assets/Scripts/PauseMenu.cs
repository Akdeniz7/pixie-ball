using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausep;

    void Start()
    {
        pausep.SetActive(false);
        
    }

    void Update()
    {
        
    }
    public void butondan_gelen(string ne_geldi)
    {
        if (ne_geldi == "pause")
        {
            pausep.SetActive(true);
            Time.timeScale = 0;

        } 
        else if (ne_geldi == "devamet")
        {
            pausep.SetActive(false);
            Time.timeScale = 1;
        } 
        else if (ne_geldi == "MainMenu")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
         } 
        else if (ne_geldi == "restart")
        { Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1;

        }
    }


}
