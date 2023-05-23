using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ChooseFaceScrollActions : MonoBehaviour
{

    //public ScrollRect scrollView;
   
    //public Image mainFaceImage;
    public int nSelectedFaceIndex;
    public AudioClip buttonSound1;
    public Image imageMainPhoto;

    private UIManager uiManager;

    [SerializeField] private float time = 1.0f;
    [SerializeField] private Ease ease = Ease.Linear;
    // Use this for initialization
    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();
        Debug.Log("Start ChooseFaceScrollActions");
        this.nSelectedFaceIndex = 1;
        string filename = "PTS_Home_Big_1" ;
        Sprite newSprite = Resources.Load<Sprite>(filename);
        //GameObject.Find("Image Main Photo").GetComponent<Image>().sprite = newSprite;
        imageMainPhoto.sprite = newSprite;
    }

    public void ResetFaceDetails()
    {
        this.nSelectedFaceIndex = 1;
        string filename = "PTS_Home_Big_1";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        //GameObject.Find("Image Main Photo").GetComponent<Image>().sprite = newSprite;
        imageMainPhoto.sprite = newSprite;
    }


    public void onContinueButton()
    {
        clicksound();
        //AudioManager.Instance.Play(buttonSound1);
        Debug.Log("onContinueButton");
       

        GameData.SelectedFaceIndex = this.nSelectedFaceIndex;
        Debug.Log("GameData.SelectedFaceIndex  - " + GameData.SelectedFaceIndex);
        //SceneManager.LoadScene("ChooseMusic");
        //uiManager.chooseMusicScreen.SetActive(true);
        transform.GetComponent<ChooseMusic>().UnlockPurchasedScrolls();
        uiManager.ChooseMusicScreen("LeftToCenter");
        //uiManager.GetComponent<ChooseMusic>().EnableMusic();
        //uiManager.chooseFaceScreen.SetActive(false);
        uiManager.ChooseFaceScreen("CenterToRight");
    }   
    public void onBackButton()
    {
        clicksound();
        // AudioManager.Instance.Play(buttonSound1);
        //SceneManager.LoadScene("HomeScene");
        //uiManager.homeScreen.SetActive(true);
        uiManager.ChooseHomeScreen("RightToCenter");
        uiManager.ChooseFaceScreen("CenterToLeft");
    }

    public void NextButtonClicked()

    {
        clicksound();
        // AudioManager.Instance.Play(buttonSound1);
        this.nSelectedFaceIndex = nSelectedFaceIndex + 1;
        if (nSelectedFaceIndex > 25)
        {
            this.nSelectedFaceIndex = 1;
        }

        string filename = "PTS_Home_Big_" + (this.nSelectedFaceIndex);
        Sprite newSprite = Resources.Load<Sprite>(filename);

        // GameObject.Find("Image Main Photo").GetComponent<Image>().color = Random.ColorHSV();
        GameObject.Find("Image Main Photo").GetComponent<Image>().sprite = newSprite;
       
       

    }
    public void PreviousButtonClicked()

    {
        clicksound();

        
        if (nSelectedFaceIndex ==1)
        {
            this.nSelectedFaceIndex = 26;
        }
        this.nSelectedFaceIndex = nSelectedFaceIndex - 1;
        Debug.Log(nSelectedFaceIndex);

        string filename = "PTS_Home_Big_" + (this.nSelectedFaceIndex);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        GameObject.Find("Image Main Photo").GetComponent<Image>().sprite = newSprite;
        
        


    }
    public void clicksound()
    {
        int soundFlag = PlayerPrefs.GetInt("sound");

        if (soundFlag == 0)
        {
            AudioManager.Instance.PlaySound(buttonSound1);
        }
    }
}

