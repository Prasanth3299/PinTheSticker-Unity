using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour
{
    public GameObject SavingPopup;

    public void OkButtonPressed()
    {
       
        Debug.Log("OkButtonPressed");
        SceneManager.LoadScene("GameScene");
    }
    public void SaveButtonPressed()
    {
        SavingPopup.SetActive(true);

    }
    public void CancelButtonPressed()
    {
        SavingPopup.SetActive(false);

    }



}
