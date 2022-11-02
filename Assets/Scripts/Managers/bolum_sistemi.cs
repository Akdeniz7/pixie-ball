using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bolum_sistemi : MonoBehaviour
{
   
    void Start()
    {
        kilit_sistemi();
    }
    public Button[] buttonlar;
    public void bolum_ac(string bolum_ismi)
    {
        SceneManager.LoadScene(bolum_ismi);
    }
    public void kilit_sistemi()
    {
        int bolums = PlayerPrefs.GetInt("bolum");
        for (int i = 0; i < buttonlar.Length; i++)
        {
            if (bolums + 1 >= int.Parse(buttonlar[i].name))
            {
                buttonlar[i].interactable = true;
            }
            else
            {
                buttonlar[i].interactable = false;
            }
        }
    }

}
