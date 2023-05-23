using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class SplashScene : MonoBehaviour
{
    void Start()
    {
        //var date = Convert.ToDateTime(PlayerPrefs.GetString("SysTime"));
        Debug.Log("date" + PlayerPrefs.GetString("SystTime").Length);
      
        if (PlayerPrefs.GetString("SystTime").Length == 0)
        {
            PlayerPrefs.SetInt("ButtonEnabled", 1);
           
        }
        else 
        {
            var date = Convert.ToDateTime(PlayerPrefs.GetString("SystTime"));
            if ((System.DateTime.Now - date).Seconds < 40)
            {
                PlayerPrefs.SetInt("ButtonEnabled", 1);
            }
            else
            {
                PlayerPrefs.SetInt("ButtonEnabled", 0);
            }
        }
        
      
        StartCoroutine(ShowNextScreen());

    }


    IEnumerator ShowNextScreen()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("HomeScene");
    }
}
