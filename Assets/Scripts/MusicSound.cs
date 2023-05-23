using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Model;
using DG.Tweening;
using ChartboostSDK;
using Platinio;

public class MusicSound : MonoBehaviour
{
    public static MusicSound Instance = null;

    public AudioClip BgMusic;
    public AudioClip buttonSound1;
    public AudioClip bellSound;
    public AudioClip GallerySound;
    public AudioClip PlayGameSound;
    public AudioClip RemoveAdSound;
    public AudioClip ShopSound;
    public AudioClip UnlockVideoSound;
    public AudioClip JackpotSound;
    

    public Sprite OnSprite;
    public Sprite OffSprite;
    public Sprite Bg;
    public Button SoundButtonOn;
    public Button SoundButtonOff;
    public Button MusicButtonOn;
    public Button MusicButtonOff;

    public GameObject PanelPopupNoNetwork;
    public GameObject SoundPopup;
    public GameObject UnlockPopup;
    public GameObject CongratulationPopup;
    public Image rewardImage;
    public GameObject TutorialPopup;

    bool isPanelOpen = false;

    public Sprite[] animatedFlowerImages;
    public Image FlowerImage;
    public Text text;
    private List<Music> musicList;
    private UIManager uiManager;
    private List<string> delegateHistory;
    List<Jackpot> payspintList = new List<Jackpot>();

    public GameObject removeAdsPopup;
    public Text removeAdsInfotext;
    public GameObject purchasePopUp;
    public Text purchasePopUpInfoText;
    public GameObject quitAppPopUp;
    public GameObject unlockChairImage;
    public GameObject removeAddsChairImage;
    public GameObject shopImage;
    public GameObject galleryButtonImage;
    public GameObject playButtonImage;
    public GameObject jackButtonImage;

    public GameObject loadingScreen;

    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();
        //DatabaseManager.InitializeDB();
        musicList = DatabaseManager.GetMusicsValues();
       
        if (PlayerPrefs.GetInt("music") == 0 && AudioManager.Instance.GetMusicFlag()==false)
        {

            AudioManager.Instance.SetMusicFlag(true);
            
            /*if (PlayerPrefs.GetInt("music") == 1)
            {
                AudioManager.Instance.PlayMusic(BgMusic);
            }*/
            AudioManager.Instance.PlayMusic(BgMusic);
            MusicButtonOn.image.sprite = Bg;
            MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            MusicButtonOff.image.sprite = OffSprite;
            MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
        }
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            SoundButtonOn.image.sprite = Bg;
            SoundButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            SoundButtonOff.image.sprite = OffSprite;
            SoundButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
        }
 
        if (PlayerPrefs.GetInt("music") == 0)
        {
            AudioManager.Instance.PlayMusic(BgMusic);
            MusicButtonOn.image.sprite = Bg;
            
            MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            MusicButtonOff.image.sprite = OffSprite;
            MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
        }
        else
        {

            MusicButtonOn.image.sprite = OnSprite;
            MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
            MusicButtonOff.image.sprite = Bg;
            MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);

        }

        // SoundPopup.SetActive(false);
        //  UnlockPopup.SetActive(false);
        //CongratulationPopup.SetActive(false);


    }

    
    // Update is called once per frame
    private void Update()
    {
       // FlowerImage.sprite = animatedFlowerImages[(int)(Time.time * 10) % animatedFlowerImages.Length];
       /*if(uiManager.homeScreen.activeSelf)
        {
            if(!uiManager.chooseFaceScreen.activeSelf && !uiManager.chooseMusicScreen.activeSelf && !uiManager.gameScreen.activeSelf
                && !uiManager.galleryScreen.activeSelf && !uiManager.shopScreen.activeSelf && !uiManager.spinWheelScreen.activeSelf
                && !uiManager.viewScreen.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OnCloseButtonClicked();
                }
            }
        }*/
    }

    public void PlayBgMusic()
    {
        clicksound();
        int musicFlag = PlayerPrefs.GetInt("music");
        print("Music flag :" + musicFlag);
        if (musicFlag == 1)
        {
            musicFlag = 0;
            
            AudioManager.Instance.SetMusicFlag(true);
            MusicButtonOn.image.sprite = Bg;
            MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            MusicButtonOff.image.sprite = OffSprite;
            MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
            AudioManager.Instance.PlayMusic(BgMusic);
            /*if (GameData.SelectedMusicIndex >= 1)
            {
                AudioManager.Instance.PlayMusic(transform.GetComponent<GameScripts>().musicarray[GameData.SelectedMusicIndex - 1]);
            }
            else
            {
                AudioManager.Instance.PlayMusic(BgMusic);
                //AudioManager.Instance.PlayMusic(transform.GetComponent<GameScripts>().musicarray[0]);
            }*/
        }
        else
        {
            musicFlag = 1;
           
            AudioManager.Instance.SetMusicFlag(false);
            MusicButtonOn.image.sprite = OnSprite;
            MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
            MusicButtonOff.image.sprite = Bg;
            MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            AudioManager.Instance.StopMusic();

            
        }
        PlayerPrefs.SetInt("music", musicFlag);
    }

   
    public void PlayButtonSound1()
    {

       
        int soundFlag = PlayerPrefs.GetInt("sound");
        if (soundFlag == 0)
        {
            soundFlag = 1;
        }
        else
        {
            soundFlag = 0;
        }
       PlayerPrefs.SetInt("sound", soundFlag);
        if (soundFlag == 0)
        {
           
            AudioManager.Instance.PlaySound(buttonSound1);
            SoundButtonOn.image.sprite = Bg;
            SoundButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            SoundButtonOff.image.sprite = OffSprite;
            SoundButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);

        }
        else
        {
            AudioManager.Instance.PlaySound(buttonSound1);
            
            //AudioManager.Instance.StopSound();
            SoundButtonOff.image.sprite = Bg;
            SoundButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
            SoundButtonOn.image.sprite = OnSprite;
            SoundButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
        }

    }
    public void clicksound()
    {
        int soundFlag = PlayerPrefs.GetInt("sound");

        if (soundFlag == 0)
        {
            AudioManager.Instance.PlaySound(buttonSound1);
        }
    }


    public void PlayButtonPressed()
    {
        clicksound();
        PlayButtonAnimationClicked();
       StartCoroutine (PlayButtonCallBack());
        //Debug.Log("Gallerybuttonpressed");
        // SceneManager.LoadScene("ChooseFaceScene"); 
        //SceneManager.LoadScene("ChooseFaceScene");
        //uiManager.chooseFaceScreen.SetActive(true);
        /*uiManager.ChooseFaceScreen("LeftToCenter");
        uiManager.ChooseHomeScreen("CenterToRight");*/
        //uiManager.homeScreen.SetActive(false);
    }
    public IEnumerator  PlayButtonCallBack()
    {
        yield return new WaitForSeconds(0.3f);
        uiManager.ChooseFaceScreen("LeftToCenter");
        uiManager.ChooseHomeScreen("CenterToRight");

    }   
    public void GalleryButtonPressed()
    {
        GalleryButtonAnimationClicked();
        //clicksound();
        //Debug.Log("Gallerybuttonpressed");
        //SceneManager.LoadScene("MyGalleryScene");
        //uiManager.galleryScreen.SetActive(true);
        StartCoroutine(ShowLoadingScreen());
        //uiManager.chooseMusicScreen.SetActive(false);
        //uiManager.ChooseMusicScreen("CenterToRight");
    }

    IEnumerator ShowLoadingScreen()
    {
        yield return new WaitForSeconds(0.3f);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1);

        transform.GetComponent<GalleryButton>().SetGalleryDetails();
        transform.GetComponent<GalleryButton>().fromScreenName = "";
        uiManager.ChooseGalleryScreen("LeftToCenter");
        uiManager.ChooseHomeScreen("CenterToRight");
    }

    public void JackpotButtonPressed()
    {
        JackpotSoundClicked();
        JackButtonAnimationClicked();
        /*Debug.Log("Jackpotbuttonpressed");
        //SceneManager.LoadScene("SpinWheelScene");
        //uiManager.spinWheelScreen.SetActive(true);
        transform.GetComponent<Wheel>().SpinWheelUpdate();
        uiManager.freeSpinWheelScreen.SetActive(true);
        uiManager.paidSpinWheelScreen.SetActive(false);
        //uiManager.homeScreen.SetActive(false);
        uiManager.ChooseSpinWheelScreen("LeftToCenter");
        uiManager.ChooseHomeScreen("CenterToRight");
        JackpotSoundClicked();*/
        StartCoroutine (JackpotButtonCallBack());
    }
    public IEnumerator JackpotButtonCallBack()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Jackpotbuttonpressed");
        //SceneManager.LoadScene("SpinWheelScene");
        //uiManager.spinWheelScreen.SetActive(true);
        transform.GetComponent<Wheel>().SpinWheelUpdate();
        uiManager.freeSpinWheelScreen.SetActive(true);
        uiManager.paidSpinWheelScreen.SetActive(false);
        //uiManager.homeScreen.SetActive(false);
        uiManager.ChooseSpinWheelScreen("LeftToCenter");
        uiManager.ChooseHomeScreen("CenterToRight");
        //JackpotSoundClicked();
    }
    public void ShopButtonPressed()
    {
        //clicksound();
        ShopButtonAnimationClicked();
        Debug.Log("Shopbuttonpressed");
        StartCoroutine(ShopButtonPressedCallBack());
        //SceneManager.LoadScene("ShopScene");
        //uiManager.shopScreen.SetActive(true);
       /* uiManager.ChooseHomeScreen("CenterToRight");
        transform.GetComponent<ShopScene>().fromScreenName = "";
        uiManager.ChooseShopScreen("LeftToCenter");
        uiManager.GetComponent<ShopScene>().fetchFromDb();*/
        //uiManager.chooseMusicScreen.SetActive(false);
        //uiManager.ChooseMusicScreen("CenterToRight");
    }

    public IEnumerator ShopButtonPressedCallBack()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.ChooseHomeScreen("CenterToRight");
        transform.GetComponent<ShopScene>().fromScreenName = "";
        uiManager.ChooseShopScreen("LeftToCenter");
        uiManager.GetComponent<ShopScene>().fetchFromDb();
    }
    public void SoundPanalClicked()
    {
       // clicksound();
       // SoundPopup.SetActive(true);
    }



    public void OkbuttonClicked()
    {
        //clicksound();
        Debug.Log("Okbuttonpressed");
       // SoundPopup.SetActive(false);


    }
    public void UnlockPanalClicked()
    {
        // clicksound();
        //UnlockPopup.SetActive(true);
        //Chartboost.cacheRewardedVideo(CBLocation.Default);
        if (isPanelOpen == false)
        {
            GameObject.Find("Panel Unlock Popup").GetComponent<Animation>().Play("Unlock");
            GameObject.Find("Panel Unlock Popup").GetComponent<Animation>().Play("UnlockVideoPanel");

            isPanelOpen = true;

        }
      

    }
    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        print(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
    }

    public void NoThanksbuttonClicked()
    {
        clicksound();
        Debug.Log("NoThanksbuttonpressed");
       // UnlockPopup.SetActive(false);


    }
    public void WatchNowbuttonClicked()
    {
        clicksound();
        Debug.Log("WatchNowbuttonpressed");
        /*UnlockPopup.SetActive(false);
        payspintList = DatabaseManager.GetPaySpinItems();
        FillFreeSpin();
        CongratulationPopup.GetComponent<Popup>().Toggle();*/
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                UnlockPopup.SetActive(false);
                UnlockPopup.GetComponent<Popup>().Toggle();
                Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
            }
            else
            {
                print("NO NETWORK");
                UnlockPopup.SetActive(false);
                UnlockPopup.GetComponent<Popup>().Toggle();
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            AudioManager.Instance.SetMusicFlag(false);
            AudioManager.Instance.PauseMusic();
            print("NETWORK");
            UnlockPopup.SetActive(false);
            UnlockPopup.GetComponent<Popup>().Toggle();
            Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
        }
        else
        {

            print("NO NETWORK");
            UnlockPopup.SetActive(false);
            UnlockPopup.GetComponent<Popup>().Toggle();
            PanelPopupNoNetwork.SetActive(true);
        }

    }

    IEnumerator checkInternetConnection(System.Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    public void NoNetworkOKButtonClicked()
    {
        clicksound();
        PanelPopupNoNetwork.SetActive(false);
    }

    private void CompleteMethod(bool completed)
    {
        //UnlockPopup.SetActive(false);
        //UnlockPopup.GetComponent<Popup>().Toggle();
        AudioManager.Instance.SetMusicFlag(true);
        AudioManager.Instance.UnPauseMusic();
        payspintList = DatabaseManager.GetPaySpinItems();
        FillFreeSpin();
        CongratulationPopup.GetComponent<Popup>().Toggle();
    }

    public void CongratsOkButtonClicked()
    {
        clicksound();
        Debug.Log("CongratsOkbuttonpressed");
       
      //  CongratulationPopup.SetActive(false);

    }

    public void SettingButtonClicked()
    {

        if (isPanelOpen == false)
        {
            GameObject.Find("Panel Settings Details").GetComponent<Animation>().Play("SettingBottomToTop");

            isPanelOpen = true;

        }
        else if (isPanelOpen == true)
        {
            GameObject.Find("Panel Settings Details").GetComponent<Animation>().Play("SettingTopToBottom");

            isPanelOpen = false;
        }


    }

    public void CloseSettingsPanel()
    {
        //transform.GetComponent<GearMenu>().SwitchOff();
    }

    public void bellbuttonCflicked(Transform objTransform)
    {
        objTransform.DOBlendablePunchRotation(new Vector3(2, 2, 2), 1);
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(bellSound);
        }
        CloseSettingsPanel();
    }

    public void GallerySoundClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(GallerySound);
        }
        CloseSettingsPanel();
    }
    public void PlayGameSoundClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(PlayGameSound);
        }
        CloseSettingsPanel();
    }
    public void RemoveAdSoundClicked()
    {
        RemoveAddsChairAmimationClicked();
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            //AudioManager.Instance.PlaySound(RemoveAdSound);
            AudioManager.Instance.PlaySound(UnlockVideoSound);
        }
        if (PlayerPrefs.GetInt("NoAds", 0) == 0)
        {
            removeAdsPopup.SetActive(true);
            removeAdsPopup.GetComponent<Popup>().Toggle();
            removeAdsInfotext.text = "Do you want to remove Ads for $0.99?";
        }
        else
        {
            purchasePopUp.SetActive(true);
            purchasePopUp.GetComponent<Popup>().Toggle();
            purchasePopUpInfoText.text = "You have already purchased No Ads";
        }
        CloseSettingsPanel();
    }

    public void OnRemoveAdPurchaseClicked()
    {
        clicksound();
        CloseSettingsPanel();
        StartCoroutine(RemoveAdds());
        if (PlayerPrefs.GetInt("NoAds", 0) == 0)
        {
            /*StartCoroutine(checkInternetConnection((isConnected) => {
                if (isConnected)
                {
                    print("NETWORK");
                    removeAdsPopup.SetActive(false);
                    removeAdsPopup.GetComponent<Popup>().Toggle();
                    transform.GetComponent<IAPManager>().OnRemoveAdsClicked();
                }
                else
                {
                    print("NO NETWORK");
                    removeAdsPopup.SetActive(false);
                    removeAdsPopup.GetComponent<Popup>().Toggle();
                    PanelPopupNoNetwork.SetActive(true);
                }

            }));*/
            //OnRemoveAdsPurchaseSuccessfull();
            if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                //print("NETWORK");
               // removeAdsPopup.SetActive(false);
               // removeAdsPopup.GetComponent<Popup>().Toggle();
                //transform.GetComponent<IAPManager>().OnRemoveAdsClicked();
            }
            else
            {

                //print("NO NETWORK");
                //removeAdsPopup.SetActive(false);
               // removeAdsPopup.GetComponent<Popup>().Toggle();
                //PanelPopupNoNetwork.SetActive(true);
            }
        }
        else
        {
            //purchasePopUp.SetActive(true);
           // purchasePopUpInfoText.text = "Your have already purchased No Ads";
            //purchasePopUp.GetComponent<Popup>().Toggle();
        }
    }

    public IEnumerator RemoveAdds()
    {
        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.GetInt("NoAds", 0) == 0)
        {
            /*StartCoroutine(checkInternetConnection((isConnected) => {
                if (isConnected)
                {
                    print("NETWORK");
                    removeAdsPopup.SetActive(false);
                    removeAdsPopup.GetComponent<Popup>().Toggle();
                    transform.GetComponent<IAPManager>().OnRemoveAdsClicked();
                }
                else
                {
                    print("NO NETWORK");
                    removeAdsPopup.SetActive(false);
                    removeAdsPopup.GetComponent<Popup>().Toggle();
                    PanelPopupNoNetwork.SetActive(true);
                }

            }));*/
            //OnRemoveAdsPurchaseSuccessfull();
            if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                print("NETWORK");
                removeAdsPopup.SetActive(false);
                removeAdsPopup.GetComponent<Popup>().Toggle();
                transform.GetComponent<IAPManager>().OnRemoveAdsClicked();
            }
            else
            {

                print("NO NETWORK");
                removeAdsPopup.SetActive(false);
                removeAdsPopup.GetComponent<Popup>().Toggle();
                PanelPopupNoNetwork.SetActive(true);
            }
        }
        else
        {
            purchasePopUp.SetActive(true);
            purchasePopUpInfoText.text = "Your have already purchased No Ads";
            purchasePopUp.GetComponent<Popup>().Toggle();
        }
    }


    public void OnRemoveAdsPurchaseSuccessfull()
    {
        purchasePopUp.SetActive(true);
        purchasePopUp.GetComponent<Popup>().Toggle();
        purchasePopUpInfoText.text = "Your purchase is successfull";
        PlayerPrefs.SetInt("NoAds", 1);
    }
    public void OnRemoveAdsPurchaseFailed()
    {
        purchasePopUp.SetActive(true);
        purchasePopUp.GetComponent<Popup>().Toggle();
        purchasePopUpInfoText.text = "Your purchase is unsuccessfull.\n Please try again later";
    }
    public void PurchasePopupOkClicked()
    {
        clicksound();
        purchasePopUp.GetComponent<Popup>().Toggle();
        //purchasePopUp.SetActive(false);
    }

    public void ShopSoundClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(ShopSound);
        }
        CloseSettingsPanel();
    }
    public void UnlockVideoSoundClicked()
    {
        UnlockChairAnimationClicked();
        payspintList = DatabaseManager.GetPaySpinItems();
        if (payspintList.Count == 0)
        {
            purchasePopUp.SetActive(true);
            purchasePopUp.GetComponent<Popup>().Toggle();
            //purchasePopUpInfoText.text = "There are no videos available now to watch";
            purchasePopUpInfoText.text = "All available items have been unlocked";
            print("no video available");
        }
        else
        {
            UnlockPopup.SetActive(true);
            UnlockPopup.GetComponent<Popup>().Toggle();
        }
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(UnlockVideoSound);
        }
        CloseSettingsPanel();
    }

    public void JackpotSoundClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(JackpotSound);
        }
        CloseSettingsPanel();
    }
    public void OnCloseButtonClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(ShopSound);
        }
        //quitAppPopUp.SetActive(true);
        quitAppPopUp.GetComponent<Popup>().Toggle();
    }
    public void OnCloseButtonOkClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(ShopSound);
        }
        Application.Quit();
    }
    public void OnCloseButtonCancelClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(ShopSound);
        }
        
        quitAppPopUp.GetComponent<Popup>().Toggle();
        //quitAppPopUp.SetActive(false);
    }

    void FillFreeSpin()
    {
        //skyblue
        Sprite skyblueSprite = null;
        if (payspintList[0].jackpotid == 1) //eyes
            skyblueSprite = GenerateEyes(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 2) //Nose
            skyblueSprite = GenerateNose(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 3) //lips
            skyblueSprite = GenerateLips(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 4) //Ear
            skyblueSprite = GenerateEar(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 5) //Hair
            skyblueSprite = GenerateHair(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 6) //HairBand
            skyblueSprite = GenerateHairBand(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 7) //Caps
            skyblueSprite = GenerateCaps(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 8) //Tiara
            skyblueSprite = GenerateTiara(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 9) //Dress
            skyblueSprite = GenerateDress(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 10) //Mouchstache
            skyblueSprite = GenerateMouchstache(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 11) //Beards
            skyblueSprite = GenerateBeards(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 12) //Goggles
            skyblueSprite = GenerateGoggles(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 13) //Animals 
            skyblueSprite = GenerateAnimals(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 14) //Birds 
            skyblueSprite = GenerateBirds(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 15) //Insects 
            skyblueSprite = GenerateInsects(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 16) //Fishes 
            skyblueSprite = GenerateFishes(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 17) //Cartoon Movie Characters 
            skyblueSprite = GenerateCartoonMovieCharacters(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 18) //Girl Stickers 
            skyblueSprite = GenerateGirlStickers(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 19) //Mixed Stickers 
            skyblueSprite = GenerateMixedStickers(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 20) //Funny Faces 
            skyblueSprite = GenerateFunnyFaces(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 21) //Emoticons 
            skyblueSprite = GenerateEmoticons(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 22) //Text 
            skyblueSprite = GenerateText(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 23) //Tatooes 
            skyblueSprite = GenerateTatooes(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 24) //Color/GlitterStars 
            skyblueSprite = GenerateGlitterStars(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 25) //Color/ GlitterHearts 
            skyblueSprite = GenerateGlitterHearts(payspintList[0].jackpotid);

        else if (payspintList[0].jackpotid == 26) //Mirror frames 
            skyblueSprite = GenerateMirrorframes(payspintList[0].jackpotid);

        //........ 26;
        //blue
        Sprite blueSprite = null;

        if (payspintList[1].jackpotid == 1) //eyes
            blueSprite = GenerateEyes(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 2) //Nose
            blueSprite = GenerateNose(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 3) //lips
            blueSprite = GenerateLips(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 4) //Ear
            blueSprite = GenerateEar(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 5) //Hair
            blueSprite = GenerateHair(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 6) //HairBand
            blueSprite = GenerateHairBand(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 7) //Caps
            blueSprite = GenerateCaps(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 8) //Tiara
            blueSprite = GenerateTiara(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 9) //Dress
            blueSprite = GenerateDress(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 10) //Mouchstache
            blueSprite = GenerateMouchstache(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 11) //Beards
            blueSprite = GenerateBeards(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 12) //Goggles
            blueSprite = GenerateGoggles(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 13) //Animals 
            blueSprite = GenerateAnimals(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 14) //Birds 
            blueSprite = GenerateBirds(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 15) //Insects 
            blueSprite = GenerateInsects(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 16) //Fishes 
            blueSprite = GenerateFishes(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 17) //Cartoon Movie Characters 
            blueSprite = GenerateCartoonMovieCharacters(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 18) //Girl Stickers 
            blueSprite = GenerateGirlStickers(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 19) //Mixed Stickers 
            blueSprite = GenerateMixedStickers(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 20) //Funny Faces 
            blueSprite = GenerateFunnyFaces(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 21) //Emoticons 
            blueSprite = GenerateEmoticons(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 22) //Text 
            blueSprite = GenerateText(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 23) //Tatooes 
            blueSprite = GenerateTatooes(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 24) //Color/GlitterStars 
            blueSprite = GenerateGlitterStars(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 25) //Color/ GlitterHearts 
            blueSprite = GenerateGlitterHearts(payspintList[1].jackpotid);

        else if (payspintList[1].jackpotid == 26) //Mirror frames 
            blueSprite = GenerateMirrorframes(payspintList[1].jackpotid);

        //violet -
        Sprite viloteSprite = null;

        if (payspintList[2].jackpotid == 1) //eyes
            viloteSprite = GenerateEyes(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 2) //Nose
            viloteSprite = GenerateNose(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 3) //lips
            viloteSprite = GenerateLips(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 4) //Ear
            viloteSprite = GenerateEar(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 5) //Hair
            viloteSprite = GenerateHair(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 6) //HairBand
            viloteSprite = GenerateHairBand(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 7) //Caps
            viloteSprite = GenerateCaps(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 8) //Tiara
            viloteSprite = GenerateTiara(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 9) //Dress
            viloteSprite = GenerateDress(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 10) //Mouchstache
            viloteSprite = GenerateMouchstache(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 11) //Beards
            viloteSprite = GenerateBeards(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 12) //Goggles
            viloteSprite = GenerateGoggles(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 13) //Animals 
            viloteSprite = GenerateAnimals(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 14) //Birds 
            viloteSprite = GenerateBirds(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 15) //Insects 
            viloteSprite = GenerateInsects(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 16) //Fishes 
            viloteSprite = GenerateFishes(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 17) //Cartoon Movie Characters 
            viloteSprite = GenerateCartoonMovieCharacters(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 18) //Girl Stickers 
            viloteSprite = GenerateGirlStickers(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 19) //Mixed Stickers 
            viloteSprite = GenerateMixedStickers(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 20) //Funny Faces 
            viloteSprite = GenerateFunnyFaces(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 21) //Emoticons 
            viloteSprite = GenerateEmoticons(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 22) //Text 
            viloteSprite = GenerateText(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 23) //Tatooes 
            viloteSprite = GenerateTatooes(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 24) //Color/GlitterStars 
            viloteSprite = GenerateGlitterStars(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 25) //Color/ GlitterHearts 
            viloteSprite = GenerateGlitterHearts(payspintList[2].jackpotid);

        else if (payspintList[2].jackpotid == 26) //Mirror frames 
            viloteSprite = GenerateMirrorframes(payspintList[2].jackpotid);

        //purple

        Sprite purpleSprite = null;

        if (payspintList[3].jackpotid == 1) //eyes
            purpleSprite = GenerateEyes(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 2) //Nose
            purpleSprite = GenerateNose(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 3) //lips
            purpleSprite = GenerateLips(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 4) //Ear
            purpleSprite = GenerateEar(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 5) //Hair
            purpleSprite = GenerateHair(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 6) //HairBand
            purpleSprite = GenerateHairBand(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 7) //Caps
            purpleSprite = GenerateCaps(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 8) //Tiara
            purpleSprite = GenerateTiara(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 9) //Dress
            purpleSprite = GenerateDress(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 10) //Mouchstache
            purpleSprite = GenerateMouchstache(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 11) //Beards
            purpleSprite = GenerateBeards(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 12) //Goggles
            purpleSprite = GenerateGoggles(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 13) //Animals 
            purpleSprite = GenerateAnimals(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 14) //Birds 
            purpleSprite = GenerateBirds(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 15) //Insects 
            purpleSprite = GenerateInsects(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 16) //Fishes 
            purpleSprite = GenerateFishes(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 17) //Cartoon Movie Characters 
            purpleSprite = GenerateCartoonMovieCharacters(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 18) //Girl Stickers 
            purpleSprite = GenerateGirlStickers(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 19) //Mixed Stickers 
            purpleSprite = GenerateMixedStickers(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 20) //Funny Faces 
            purpleSprite = GenerateFunnyFaces(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 21) //Emoticons 
            purpleSprite = GenerateEmoticons(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 22) //Text 
            purpleSprite = GenerateText(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 23) //Tatooes 
            purpleSprite = GenerateTatooes(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 24) //Color/GlitterStars 
            purpleSprite = GenerateGlitterStars(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 25) //Color/ GlitterHearts 
            purpleSprite = GenerateGlitterHearts(payspintList[3].jackpotid);

        else if (payspintList[3].jackpotid == 26) //Mirror frames 
            purpleSprite = GenerateMirrorframes(payspintList[3].jackpotid);

        //red
        Sprite redSprite = null;

        if (payspintList[4].jackpotid == 1) //eyes
            redSprite = GenerateEyes(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 2) //Nose
            redSprite = GenerateNose(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 3) //lips
            redSprite = GenerateLips(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 4) //Ear
            redSprite = GenerateEar(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 5) //Hair
            redSprite = GenerateHair(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 6) //HairBand
            redSprite = GenerateHairBand(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 7) //Caps
            redSprite = GenerateCaps(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 8) //Tiara
            redSprite = GenerateTiara(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 9) //Dress
            redSprite = GenerateDress(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 10) //Mouchstache
            redSprite = GenerateMouchstache(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 11) //Beards
            redSprite = GenerateBeards(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 12) //Goggles
            redSprite = GenerateGoggles(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 13) //Animals 
            redSprite = GenerateAnimals(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 14) //Birds 
            redSprite = GenerateBirds(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 15) //Insects 
            redSprite = GenerateInsects(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 16) //Fishes 
            redSprite = GenerateFishes(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 17) //Cartoon Movie Characters 
            redSprite = GenerateCartoonMovieCharacters(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 18) //Girl Stickers 
            redSprite = GenerateGirlStickers(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 19) //Mixed Stickers 
            redSprite = GenerateMixedStickers(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 20) //Funny Faces 
            redSprite = GenerateFunnyFaces(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 21) //Emoticons 
            redSprite = GenerateEmoticons(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 22) //Text 
            redSprite = GenerateText(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 23) //Tatooes 
            redSprite = GenerateTatooes(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 24) //Color/GlitterStars 
            redSprite = GenerateGlitterStars(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 25) //Color/ GlitterHearts 
            redSprite = GenerateGlitterHearts(payspintList[4].jackpotid);

        else if (payspintList[4].jackpotid == 26) //Mirror frames 
            redSprite = GenerateMirrorframes(payspintList[4].jackpotid);

        //yellow
        Sprite yellowSprite = null;

        if (payspintList[5].jackpotid == 1) //eyes
            yellowSprite = GenerateEyes(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 2) //Nose
            yellowSprite = GenerateNose(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 3) //lips
            yellowSprite = GenerateLips(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 4) //Ear
            yellowSprite = GenerateEar(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 5) //Hair
            yellowSprite = GenerateHair(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 6) //HairBand
            yellowSprite = GenerateHairBand(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 7) //Caps
            yellowSprite = GenerateCaps(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 8) //Tiara
            yellowSprite = GenerateTiara(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 9) //Dress
            yellowSprite = GenerateDress(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 10) //Mouchstache
            yellowSprite = GenerateMouchstache(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 11) //Beards
            yellowSprite = GenerateBeards(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 12) //Goggles
            yellowSprite = GenerateGoggles(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 13) //Animals 
            yellowSprite = GenerateAnimals(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 14) //Birds 
            yellowSprite = GenerateBirds(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 15) //Insects 
            yellowSprite = GenerateInsects(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 16) //Fishes 
            yellowSprite = GenerateFishes(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 17) //Cartoon Movie Characters 
            yellowSprite = GenerateCartoonMovieCharacters(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 18) //Girl Stickers 
            yellowSprite = GenerateGirlStickers(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 19) //Mixed Stickers 
            yellowSprite = GenerateMixedStickers(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 20) //Funny Faces 
            yellowSprite = GenerateFunnyFaces(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 21) //Emoticons 
            yellowSprite = GenerateEmoticons(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 22) //Text 
            yellowSprite = GenerateText(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 23) //Tatooes 
            yellowSprite = GenerateTatooes(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 24) //Color/GlitterStars 
            yellowSprite = GenerateGlitterStars(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 25) //Color/ GlitterHearts 
            yellowSprite = GenerateGlitterHearts(payspintList[5].jackpotid);

        else if (payspintList[5].jackpotid == 26) //Mirror frames 
            yellowSprite = GenerateMirrorframes(payspintList[5].jackpotid);

        //green
        Sprite greenSprite = null;

        if (payspintList[6].jackpotid == 1) //eyes
            greenSprite = GenerateEyes(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 2) //Nose
            greenSprite = GenerateNose(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 3) //lips
            greenSprite = GenerateLips(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 4) //Ear
            greenSprite = GenerateEar(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 5) //Hair
            greenSprite = GenerateHair(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 6) //HairBand
            greenSprite = GenerateHairBand(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 7) //Caps
            greenSprite = GenerateCaps(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 8) //Tiara
            greenSprite = GenerateTiara(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 9) //Dress
            greenSprite = GenerateDress(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 10) //Mouchstache
            greenSprite = GenerateMouchstache(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 11) //Beards
            greenSprite = GenerateBeards(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 12) //Goggles
            greenSprite = GenerateGoggles(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 13) //Animals 
            greenSprite = GenerateAnimals(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 14) //Birds 
            greenSprite = GenerateBirds(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 15) //Insects 
            greenSprite = GenerateInsects(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 16) //Fishes 
            greenSprite = GenerateFishes(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 17) //Cartoon Movie Characters 
            greenSprite = GenerateCartoonMovieCharacters(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 18) //Girl Stickers 
            greenSprite = GenerateGirlStickers(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 19) //Mixed Stickers 
            greenSprite = GenerateMixedStickers(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 20) //Funny Faces 
            greenSprite = GenerateFunnyFaces(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 21) //Emoticons 
            greenSprite = GenerateEmoticons(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 22) //Text 
            greenSprite = GenerateText(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 23) //Tatooes 
            greenSprite = GenerateTatooes(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 24) //Color/GlitterStars 
            greenSprite = GenerateGlitterStars(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 25) //Color/ GlitterHearts 
            greenSprite = GenerateGlitterHearts(payspintList[6].jackpotid);

        else if (payspintList[6].jackpotid == 26) //Mirror frames 
            greenSprite = GenerateMirrorframes(payspintList[6].jackpotid);

        //dark violet
        Sprite darkvioletSprite = null;

        if (payspintList[7].jackpotid == 1) //eyes
            darkvioletSprite = GenerateEyes(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 2) //Nose
            darkvioletSprite = GenerateNose(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 3) //lips
            darkvioletSprite = GenerateLips(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 4) //Ear
            darkvioletSprite = GenerateEar(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 5) //Hair
            darkvioletSprite = GenerateHair(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 6) //HairBand
            darkvioletSprite = GenerateHairBand(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 7) //Caps
            darkvioletSprite = GenerateCaps(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 8) //Tiara
            darkvioletSprite = GenerateTiara(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 9) //Dress
            darkvioletSprite = GenerateDress(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 10) //Mouchstache
            darkvioletSprite = GenerateMouchstache(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 11) //Beards
            darkvioletSprite = GenerateBeards(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 12) //Goggles
            darkvioletSprite = GenerateGoggles(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 13) //Animals 
            darkvioletSprite = GenerateAnimals(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 14) //Birds 
            darkvioletSprite = GenerateBirds(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 15) //Insects 
            darkvioletSprite = GenerateInsects(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 16) //Fishes 
            darkvioletSprite = GenerateFishes(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 17) //Cartoon Movie Characters 
            darkvioletSprite = GenerateCartoonMovieCharacters(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 18) //Girl Stickers 
            darkvioletSprite = GenerateGirlStickers(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 19) //Mixed Stickers 
            darkvioletSprite = GenerateMixedStickers(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 20) //Funny Faces 
            darkvioletSprite = GenerateFunnyFaces(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 21) //Emoticons 
            darkvioletSprite = GenerateEmoticons(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 22) //Text 
            darkvioletSprite = GenerateText(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 23) //Tatooes 
            darkvioletSprite = GenerateTatooes(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 24) //Color/GlitterStars 
            darkvioletSprite = GenerateGlitterStars(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 25) //Color/ GlitterHearts 
            darkvioletSprite = GenerateGlitterHearts(payspintList[7].jackpotid);

        else if (payspintList[7].jackpotid == 26) //Mirror frames 
            darkvioletSprite = GenerateMirrorframes(payspintList[7].jackpotid);
        int rangeValue = Random.Range(0, 8);
        switch (rangeValue)
        {
            case 0:
                rewardImage.sprite = skyblueSprite;
                break;
            case 1:
                rewardImage.sprite = blueSprite;
                break;
            case 2:
                rewardImage.sprite = viloteSprite;
                break;
            case 3:
                rewardImage.sprite = purpleSprite;
                break;
            case 4:
                rewardImage.sprite = redSprite;
                break;
            case 5:
                rewardImage.sprite = yellowSprite;
                break;
            case 6:
                rewardImage.sprite = greenSprite;
                break;
            case 7:
                rewardImage.sprite = darkvioletSprite;
                break;
        }
        int jacpotnumber = payspintList[rangeValue].jackpotid;
        Jackpot jackpot = payspintList[rangeValue];
        SetJackpotPurchaseSucess(jackpot);
    }
    private void SetJackpotPurchaseSucess(Jackpot jackpot)
    {
        DatabaseManager.SetJackpotPurchaseSucess(jackpot);
    }

    Sprite GenerateEyes(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Eyes/PS_Gamescreen_Eyes/PS_Gamescreen_Eye_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);

        return newSprite;
    }



    Sprite GenerateNose(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;


        string filename = "Nose/Pin the Sticker_Nose_All_InFrames/PS_Gamescreen_Nose_Yellow_Gamescreen_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }

    Sprite GenerateLips(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Lips/PS_Gamescreen_Lips/PS_Gamescreen_Lip_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;

    }


    Sprite GenerateEar(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Ear/Pin the Sticker_Ears_All_InFrames/PS_Gamescreen_Ears_Yellow_Gamescreen_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);

        return newSprite;



    }

    Sprite GenerateHair(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Hair/PS_Gamescreen_Hairstyles/PS_Gamescreen_Hair_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;

    }

    Sprite GenerateHairBand(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "HairBands/PS_Gamescreen_Hairbands -InFrames/PS_Gamescreen_Hairband_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateCaps(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Caps/PS_Gamescreen_Caps -InFrames/PS_Gamescreen_Cap_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }



    Sprite GenerateTiara(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Tiara/PintheSticker_Tiara_InFrames/PS_Gamescreen_Tiara_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }



    Sprite GenerateDress(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Dress/PS_Gamescreen_Dress_All_InFrames/PS_Gamescreen_Dress_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateMouchstache(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Mouchstache/PintheSticker_Mouchstache_Inframes/PS_Gamescreen_Mouchstache_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateBeards(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Beards/PintheSticker_Beards_InFrames/PS_Gamescreen_Beard_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;

    }


    Sprite GenerateGoggles(int itemNumber)
    {

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Goggles/PintheSticker_Goggles_Inframes/PS_Gamescreen_Goggles_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }



    Sprite GenerateAnimals(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;

        string filename = "Animal Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateBirds(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;

        string filename = "Birds Sticker/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateInsects(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Insects stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;

    }


    Sprite GenerateFishes(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Fishes Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }

    Sprite GenerateCartoonMovieCharacters(int itemNumber)
    {

        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Movie Characters/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateGirlStickers(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Girls Character Sticker/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }

    Sprite GenerateMixedStickers(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Mixed Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }



    Sprite GenerateFunnyFaces(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Funny Faces Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateEmoticons(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Emotions Sticker/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateText(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "Text Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }


    Sprite GenerateTatooes(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "TATOOES/PS_Gamescreen_Tatooes_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }



    Sprite GenerateGlitterStars(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "STARS/PS_Gamescreen_Stars_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }



    Sprite GenerateGlitterHearts(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "HEARTS/PS_Gamescreen_Hearts_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }

    Sprite GenerateMirrorframes(int itemNumber)
    {

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "MirrorFrames/PintheSticker_MirrorFrames_InFrames/PS_Gamescreen_MirrorFrames_13";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        return newSprite;
    }
    public void UnlockChairAnimationClicked()
        {
        StartCoroutine(UnlockChairAnimation());
    }
    public IEnumerator  UnlockChairAnimation()
    {
        Animator animator = unlockChairImage.GetComponent<Animator>();
        animator.SetBool("UnlockChairAnimation", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("UnlockChairAnimation",false);
    }
    public void RemoveAddsChairAmimationClicked()
    {
        StartCoroutine(RemoveAddsChairAnimation());
    }
    public IEnumerator RemoveAddsChairAnimation()
    {
        Animator animator = removeAddsChairImage.GetComponent<Animator>();
        animator.SetBool("RemoveAddsChair", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("RemoveAddsChair",false);
    }
    public void ShopButtonAnimationClicked()
    {
        StartCoroutine(ShopButtonAnimation());
    }
    public IEnumerator ShopButtonAnimation()
    {
        Animator animator = shopImage.GetComponent<Animator>();
        animator.SetBool("shopButtonAnimation",true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("shopButtonAnimation", false);
    }
    public void GalleryButtonAnimationClicked()
    {
        StartCoroutine(GalleyButtonAnimation());
    }
    public IEnumerator GalleyButtonAnimation()
    {
        Animator animator = galleryButtonImage.GetComponent<Animator>();
        animator.SetBool("galleyButtonAnimation", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("galleyButtonAnimation", false);
    }

    public void PlayButtonAnimationClicked()
    {
        StartCoroutine(PlayButtonAnimation());
    }
    public IEnumerator PlayButtonAnimation()
    {
        Animator animator = playButtonImage.GetComponent<Animator>();
        animator.SetBool("playButtonAnimation", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("playButtonAnimation", false);
    }
    public void JackButtonAnimationClicked()
    {
        print("jackanimation");
        StartCoroutine(JackButtonAnimation());
    }
    public IEnumerator JackButtonAnimation()
    {
        Animator animator = jackButtonImage.GetComponent<Animator>();
        animator.SetBool("JackButtonAnimation", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("JackButtonAnimation", false);
     }
}











