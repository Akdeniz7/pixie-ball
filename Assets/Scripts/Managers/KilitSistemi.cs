using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KilitSistemi : MonoBehaviour
{
    public List<Button> leveller;  //leveller listesi

    private void Start()
    {
        if (!PlayerPrefs.HasKey("level")) //default değer atama level adlı bir key yoksa 1 ataması yapıyor
            PlayerPrefs.SetInt("level", 1);
          if (!PlayerPrefs.HasKey("seviyeSayisi")) //default değer atama level adlı bir key yoksa 1 ataması yapıyor
        PlayerPrefs.SetInt("seviyeSayisi", leveller.Count);

        kilitleriAc(); //açık olan levelleri açıyor
    }


    public void kilitleriAc()//açılan bölümlerin tıklanabilirliğini aktif hale getiriyor.
    {
        for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
        {
            leveller[i].interactable = true;
        }
    }

    public string levelAdi(int id)//id den level'in ismini döndürüyor
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void levelAc(int id)//buton üzerinden gelen id ye göre level açılıyor
    {
        PlayerPrefs.SetString("suankiSecilenLevel", levelAdi(id));
        SceneManager.LoadScene(levelAdi(id));
    }

   
}