using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Model;
using ChartboostSDK;
using DanielLochner.Assets.SimpleScrollSnap;
using Platinio;
public class ChooseMusic : MonoBehaviour
{

    public ScrollRect scrollView;
    public ScrollRect scrollViewIPhone;
    public GameObject scrollContent;
    public GameObject scrollContentIPhone;
    public GameObject scrollItemPrefab;
    public GameObject ContinueButton;
    public GameObject ContinueButtonIPhone;
    public GameObject PurchasePanel;
    public GameObject PurchasePanelIPhone;
    public GameObject LoadingPanel;
    public GameObject LoadingPanelIPhone;
    public AudioClip buttonSound1;
    private int nSelectedMusicIndex=1;
    public int i = 1;
    public int TOTAL_SONGS = 30;
    List<Music> musicList = new List<Music>();
    private UIManager uiManager;
    string[] MusicName = new string[] { "Twinkle twinkle", "Wheels on the bus","Nick Nack Paddy wack",
        "Down at the Station","Baa Baa black sheep","Once I caught a fish alive","Johnny Johnny",
        "Five little monkeys", "Im a little teapot","Finger family","Rain rain go away","Heads and shoulders",
        "Five little ducks","Bingo","If your happy", "1 2 buckle my shoe","Old Macdonald Had A Farm",
        "Pat a cake","Hop little bunny","Hickory dickory","The wheels on the bus","Mary had a little lamb",
        "Humpty dumpty","Miss polly had a dolly","Big Ship Sails","Baby Shark","Ding Dong bell",
        "Apples and bananas","The Bear went over","Down by the bay"};

    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();
        print((float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1));
        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {

            scrollView = scrollViewIPhone;
            scrollContent = scrollContentIPhone;
            //ContinueButton = ContinueButtonIPhone;
            PurchasePanel = PurchasePanelIPhone;
            LoadingPanel = LoadingPanelIPhone;
        }
        LoadingPanel.SetActive(true);

        //DatabaseManager.InitializeDB();
        EnableMusic();
    }
    
    public void EnableMusic()
    {
        if(scrollContent.transform.childCount != 0)
        {
            return;
        }
        musicList = DatabaseManager.GetMusicsValues();
        for (int index = 1; index <= TOTAL_SONGS; index++)
        {
            generateItem(index);
        }
        PlayerPrefs.SetInt("SavedSongsCount", TOTAL_SONGS);
        int count = PlayerPrefs.GetInt("SavedSongsCount");
        //UnityMainThreadDispatcher.Instance().Enqueue(ListMusic());
        LoadingPanel.SetActive(false);
    }

    public void UnlockPurchasedScrolls()
    {
        musicList = DatabaseManager.GetMusicsValues();
        for (int i=0;i< scrollContent.transform.childCount;i++)
        {
            //print(musicList[i].musicname +" : "+ musicList[i].unlockflag);
            if (musicList[i].unlockflag == 1)
            {
                scrollContent.transform.GetChild(i).Find("MusicItem").transform.Find("Image Category box").transform.Find("ImageLock").GetComponent<Image>().enabled = false;

            }
            else
            {
                scrollContent.transform.GetChild(i).Find("MusicItem").transform.Find("Image Category box").transform.Find("ImageLock").GetComponent<Image>().enabled = true;
            }
        }
        if (GameData.SelectedMusicIndex > 1)
        {
            scrollView.GetComponent<SimpleScrollSnap>().GoToPanel(GameData.SelectedMusicIndex - 1);
        }
        else
        {
            scrollView.GetComponent<SimpleScrollSnap>().GoToPanel(GameData.SelectedMusicIndex);
        }
    }

    public IEnumerator ListMusic()
    {
        yield return new WaitForSeconds(0.5f);
        LoadingPanel.SetActive(false);
    }


    void Update()
    {

    }

    public void OnValueChanged(Vector2 value)
    {
        Debug.Log(value);
    }

    void generateItem(int itemNumber)
    {
        string filename = "PTS-SONGS/Song" + (itemNumber)+".mp3";
        //Debug.Log(filename);
        Sprite Music = Resources.Load<Sprite>(filename);

        string IconFilename = "CYS_SongIcons/PTS_" + (itemNumber);
        //Debug.Log(IconFilename);
        Sprite MusicIcon = Resources.Load<Sprite>(IconFilename);


        GameObject MusicscrollItemOb = Instantiate(scrollItemPrefab);
        MusicscrollItemOb.transform.SetParent(scrollContent.transform, false);
        MusicscrollItemOb.transform.Find("MusicItem").gameObject.GetComponent<Button>().onClick.AddListener(() => MusicClickIconFunction(itemNumber));
        MusicscrollItemOb.transform.Find("MusicItem").gameObject.GetComponent<Button>().image.sprite = Music;
        MusicscrollItemOb.transform.Find("MusicItem").transform.Find("Image Category box").transform.Find("ImageMusicIcon").GetComponent<Image>().sprite = MusicIcon;
        if(musicList[itemNumber-1].unlockflag == 1 )
        {
            MusicscrollItemOb.transform.Find("MusicItem").transform.Find("Image Category box").transform.Find("ImageLock").GetComponent<Image>().enabled = false;
           
        }
        else
        {
            MusicscrollItemOb.transform.Find("MusicItem").transform.Find("Image Category box").transform.Find("ImageLock").GetComponent<Image>().enabled = true;
           
        }
        

        Text text = MusicscrollItemOb.transform.Find("MusicItem").transform.Find("Image Category box").transform.Find("Text").gameObject.GetComponentInChildren<Text>();
        text.text = MusicName[itemNumber-1];
    }
    public void MusicClickIconFunction(int itemNumber)
    {
        clicksound();
        //UnityMainThreadDispatcher.Instance().Enqueue(Choosedmusic());
        StartCoroutine(Choosedmusic(itemNumber));
    }
    
    public IEnumerator Choosedmusic(int itemNumber)
    {
        //Chartboost.cacheInterstitial(CBLocation.Default);
        LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
       /* if (musicList[this.nSelectedMusicIndex - 1].unlockflag == 0)
        {
            PurchasePanel.SetActive(true);
            LoadingPanel.SetActive(false);
        }
        else*/
        {
            //GameData.SelectedMusicIndex = this.nSelectedMusicIndex;
            if (itemNumber > nSelectedMusicIndex)
            {
                int count = itemNumber - nSelectedMusicIndex;
                for (int i = 0; i < count; i++)
                {
                    NextButton();
                }
            }
            else if (itemNumber < nSelectedMusicIndex)
            {
                int count = nSelectedMusicIndex - itemNumber;
                for (int i = 0; i < count; i++)
                {
                    PreviousButton();
                }
            }
            else
            {
                //GameData.SelectedMusicIndex = this.nSelectedMusicIndex;
            }
            nSelectedMusicIndex = itemNumber;
            scrollView.GetComponent<SimpleScrollSnap>().GoToPanel(itemNumber - 1);
            LoadingPanel.SetActive(false);
            if (musicList[this.nSelectedMusicIndex - 1].unlockflag == 0)
            {
                PurchasePanel.transform.GetComponent<Popup>().Toggle();
                PurchasePanel.SetActive(true);
                LoadingPanel.SetActive(false);
            }
            else
            {
                GameData.SelectedMusicIndex = this.nSelectedMusicIndex;
                //SceneManager.LoadScene("GameScene");
                //uiManager.gameScreen.SetActive(true);
                uiManager.ChooseGameScreen("LeftToCenter");
                uiManager.GetComponent<GameScripts>().EnableAllgameDetails();
                //uiManager.chooseMusicScreen.SetActive(false);
                uiManager.ChooseMusicScreen("CenterToRight");
                if (PlayerPrefs.GetInt("NoAds", 0) == 0)
                {
                    int count = PlayerPrefs.GetInt("GameSceneCount", 0);
                    if (count >= 3)
                    {
                        count = 0;
                        PlayerPrefs.SetInt("GameSceneCount", count);
                        //Chartboost.showInterstitial(CBLocation.Default);
                        //Chartboost.cacheInterstitial(CBLocation.Default);
                        AudioManager.Instance.SetMusicFlag(false);
                        AudioManager.Instance.PauseMusic();
                        Advertisements.Instance.ShowInterstitial(InterstitialClosed);
                    }
                    else
                    {
                        count++;
                        PlayerPrefs.SetInt("GameSceneCount", count);
                    }
                }
            }
            
        }
    }

    public void InterstitialClosed()
    {
        AudioManager.Instance.SetMusicFlag(true);
        AudioManager.Instance.UnPauseMusic();
    }


    public void NextButton()

        {
        clicksound();
        int galleryindex = 0;
           
            int count = PlayerPrefs.GetInt("SavedSongsCount");
           // Debug.Log(count);

            this.nSelectedMusicIndex = nSelectedMusicIndex+1;
            galleryindex = nSelectedMusicIndex;
            if (nSelectedMusicIndex >=count+1)
            {
                this.nSelectedMusicIndex =1;
                galleryindex = 1;
            }
            Debug.Log("nSelectedMusicIndex : " + nSelectedMusicIndex);


        }

        public void PreviousButton()

        {
        clicksound();
        int galleryindex = 0;
         
            int count = PlayerPrefs.GetInt("SavedSongsCount");
            if (nSelectedMusicIndex ==1)
            {
                this.nSelectedMusicIndex = count+1;

            }
            this.nSelectedMusicIndex = nSelectedMusicIndex-1;


            galleryindex = nSelectedMusicIndex;
            Debug.Log("nSelectedMusicIndex : "+nSelectedMusicIndex);



        }
    public void YesButtonClicked()
    {

        PurchasePanel.SetActive(false);
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        clicksound();
        //SceneManager.LoadScene("ShopScene");
        //uiManager.shopScreen.SetActive(true);
        transform.GetComponent<ShopScene>().fromScreenName = "music";
        uiManager.ChooseShopScreen("LeftToCenter");
        uiManager.GetComponent<ShopScene>().fetchFromDb();
        //uiManager.chooseMusicScreen.SetActive(false);
        uiManager.ChooseMusicScreen("CenterToRight");
    }
    public void BackButtonClicked()
    {
        clicksound();
        //SceneManager.LoadScene("ChooseFaceScene");
        //uiManager.chooseFaceScreen.SetActive(true);
        uiManager.ChooseFaceScreen("RightToCenter");
        uiManager.ChooseMusicScreen("CenterToLeft");
        //uiManager.chooseMusicScreen.SetActive(false);
        PurchasePanel.SetActive(false);
    }
    public void NoButtonClicked()
    {
        clicksound();
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        //PurchasePanel.SetActive(false);
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

