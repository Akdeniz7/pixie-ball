using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public void SahneDegistir(string sahne_Adi)
    {
        SceneManager.LoadScene(sahne_Adi);
    }
}