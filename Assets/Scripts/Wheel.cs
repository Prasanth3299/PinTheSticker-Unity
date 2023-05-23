using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Model;
using System.Collections.Generic;
using System;
using Platinio;
using GameBench;

public class Wheel : MonoBehaviour
{
    public UIManager uiManager;
    private int randomvalue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;
    public AudioClip buttonSound1;
    public AudioClip wheelRotateSound;
    public AudioClip cheeringSound;

    public GameObject skyblue;
    public GameObject blue;
    public GameObject vilote;
    public GameObject purple;
    public GameObject red;
    public GameObject yelolow;
    public GameObject green;
    public GameObject darkvilote;

    public Image[] paidSpinImages;
    public Sprite[] paidSpinLogoImages;

    public Button spinButton;
    public Button spinPaidButton;
    public GameObject SpinImage;
    public int jacpotnumber;
    [SerializeField]
    private Text winText;
    public GameObject payPopUpPanel;
    public Text payPopUpPanelText;
    public GameObject freePopUpPanel;
    public Text freePopUpText;
    public GameObject paidPopUpPanel;
    public Text paidPopUpText;
    public GameObject PanelPopupNoNetwork;
    public GameObject purchaseFailedPopUp;
    public Button freeSpinHomeButton;
    public Button paidSpinHomeButton;
    public GameObject paidSpinButtonDisable;
    public GameObject freeSpinButtonDisable;

    List<Sprite> tempSprites;
    List<string> freeSpinItemList;
    List<int> totalList;
    // List<Items> feespintList = new List<Items>();
    List<Jackpot> payspintList = new List<Jackpot>();
    List<Shop> shopsList = new List<Shop>();
    List<Shop> unlockShopList = new List<Shop>();
    public Sprite[] animatedImagesList;
    public Image paidAnimatedImage;
    public Image freeAnimatedImage;
    public GameObject freeSpinWheel;
    public GameObject paidSpinWheel;
    public Sprite[] winAnimatedImagesList;
    //public Image winAnimatedImage;
    //public GameObject WinanimationHeader;
    public GameObject freeSpinBg;
    public GameObject paidSpinBg;
  
    private  bool PaidSpinCompleted;
    //public bool freeSpinCompleted;

    public GameObject spinWheelSystem;
    public GameObject freeSpinButton, paidSpinButton;


    public GameObject spinParent;

    private float speed = 0;
    private bool spriteFlag = false;

    private BoxCollider2D freeSpinButtonBoxCollider, paidSpinButtonBoxCollider;

    // Use this for initialization
    private void Start()
    {
        tempSprites = new List<Sprite>();
        totalList = new List<int>();
        freeSpinItemList = new List<string>();
        coroutineAllowed = true;
        PaidSpinCompleted = false;
        //DatabaseManager.InitializeDB();

        speed = 47.0f;
        spriteFlag = false;

        freeSpinButtonBoxCollider = freeSpinButton.GetComponent<BoxCollider2D>();
        paidSpinButtonBoxCollider = paidSpinButton.GetComponent<BoxCollider2D>();

        //feespintList = DatabaseManager.GetFreeSpinItems();
        //payspintList = DatabaseManager.GetPaySpinItems();
        //shopsList = DatabaseManager.GetShopsValues();
        //FillFreeSpin();
        //FillPaidSpin();
        // var date = Convert.ToDateTime(PlayerPrefs.GetString("SysTime"));

        //spinButton.interactable = true;
        //SpinImage.SetActive(true);
    }
    public void SpinWheelUpdate()
    {
        TimeSpan timeDifference;
        if (PlayerPrefs.GetString("SystTime").Length != 0)
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("SystTime"));
            long temp = Convert.ToInt64(tempTime.ToBinary());
            DateTime oldDate = DateTime.FromBinary(temp);
            oldDate = oldDate.AddDays(1);
            timeDifference = oldDate.Subtract(currentDate);
            if (timeDifference.Seconds > -1 && timeDifference.Hours < 24)
            {
                PlayerPrefs.SetInt("ButtonEnabled", 0);
            }
            else
            {
                PlayerPrefs.SetInt("ButtonEnabled", 1);
            }
        }

        payspintList = DatabaseManager.GetPaySpinItems();
        shopsList = DatabaseManager.GetShopsValues();
        FillFreeSpin();
        freeSpinBg.transform.eulerAngles = Vector3.zero;
        paidSpinBg.transform.eulerAngles = Vector3.zero;
        payPopUpPanel.SetActive(false);
        freePopUpPanel.SetActive(false);
        paidPopUpPanel.SetActive(false);
        //spinButton.interactable = false;
        //spinPaidButton.interactable = false;
        freeSpinButtonDisable.SetActive(true);
        paidSpinButtonDisable.SetActive(true);
        freeSpinHomeButton.interactable = true;
        paidSpinHomeButton.interactable = true;
        if (PlayerPrefs.GetInt("ButtonEnabled") == 0)
        {
            //spinButton.interactable = false;
            //spinPaidButton.interactable = false;
            freeSpinButtonDisable.SetActive(true);
            paidSpinButtonDisable.SetActive(true);
            SpinImage.SetActive(false);
            for (int i = 2; i < shopsList.Count; i++)
            {
                if (shopsList[i].unlockflag == 0)
                {
                    totalList.Add(i);
                    //unlockShopList.Add(shopsList[i]);
                }
            }

            if (totalList.Count == 0)
            {
                //FillPaidSpin();

                freePopUpPanel.SetActive(true);
                int hours = timeDifference.Hours;
                int minutes = timeDifference.Minutes;
                int seconds = timeDifference.Seconds;
               // freePopUpText.text = "No spin wheel items are available for now";
                freePopUpText.text = "All available items have been unlocked";
                print("spin wheel available");
            }
            else
            {
                FillPaidSpin();
                if (totalList.Count == 0)
                {
                    //FillPaidSpin();

                    freePopUpPanel.SetActive(true);
                    //freePopUpText.text = "No spin wheel items are available for now";
                    freePopUpText.text = "All available items have been unlocked";
                    print("spin wheel available");

                }
                else
                {

                    payPopUpPanel.SetActive(true);
                    int hours = timeDifference.Hours;
                    int minutes = timeDifference.Minutes;
                    int seconds = timeDifference.Seconds;
                    payPopUpPanelText.text = "Free spin wheel will be enabled after \n" + hours + "hrs : " + minutes + "mins : " + seconds + "secs\n" +
                    "Press ok for a paid spin wheel for just 0.99$";
                    //spinPaidButton.interactable = true;
                    paidSpinButtonDisable.SetActive(false);
                }

            }

            /*var date = Convert.ToDateTime(PlayerPrefs.GetString("SystTime"));
            
            if(totalList.Count == 0)
            {
                freePopUpPanel.SetActive(true);
                freePopUpText.text = "Free spin wheel will be enabled after " + (System.DateTime.Now - date) + " Seconds";
            }
            else
            { 
                payPopUpPanel.SetActive(true);
                payPopUpPanelText.text = "Free spin wheel will be enabled after " + (System.DateTime.Now - date).Seconds + " Seconds\n" +
                "Press ok for a paid spin wheel for just 0.99$";
            }*/

        }
        else
        {
            
            if (tempSprites.Count > 0)
            {
                payPopUpPanel.SetActive(false);
                freePopUpPanel.SetActive(false);
                //spinButton.interactable = true;
                freeSpinButtonDisable.SetActive(false);
                SpinImage.SetActive(true);
                StartCoroutine(EnableFreeSpinWheel());
                //spriteFlag = true;

            }
            else
            {
                FillPaidSpin();
                if (totalList.Count == 0)
                {
                    //FillPaidSpin();

                    freePopUpPanel.SetActive(true);
                    //freePopUpText.text = "No spin wheel items are available for now";
                    freePopUpText.text = "All available items have been unlocked";
                    print("spin wheel available");
                }
                else
                {
                    payPopUpPanel.SetActive(true);
                    payPopUpPanelText.text = "No Free Spin wheel items are available for now.\nPress ok for a paid spin wheel for just 0.99$";
                    paidSpinButtonDisable.SetActive(false);
                }
            }

        }
    }
    private void Update()
    {
        if (payPopUpPanel.activeSelf)
        {

            TimeSpan timeDifference;
            if (PlayerPrefs.GetString("SystTime").Length != 0)
            {
                DateTime currentDate = System.DateTime.Now;
                DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("SystTime"));
                long temp = Convert.ToInt64(tempTime.ToBinary());
                DateTime oldDate = DateTime.FromBinary(temp);
                oldDate = oldDate.AddDays(1);
                timeDifference = oldDate.Subtract(currentDate);
                int hours = timeDifference.Hours;
                int minutes = timeDifference.Minutes;
                int seconds = timeDifference.Seconds;
                payPopUpPanelText.text = "Free spin wheel will be enabled after \n" + hours + "hrs : " + minutes + "mins : " + seconds + "secs\n" +
                "Press ok for a paid spin wheel for just 0.99$";
            }

            }

        if (freeSpinWheel.activeSelf)
        {
            if (PaidSpinCompleted == false)
            {
                freeAnimatedImage.sprite = animatedImagesList[(int)(Time.time * 10) % animatedImagesList.Length];
            }
            else
            {
                freeAnimatedImage.sprite = winAnimatedImagesList[(int)(Time.time * 10) % winAnimatedImagesList.Length];
            }
        }
        else if(paidSpinWheel.activeSelf)
        {
            if (PaidSpinCompleted == false)
            {
                paidAnimatedImage.sprite = animatedImagesList[(int)(Time.time * 10) % animatedImagesList.Length];
            }
            else 
            {
                paidAnimatedImage.sprite = winAnimatedImagesList[(int)(Time.time * 10) % winAnimatedImagesList.Length];
            }
        }


        /*if(spriteFlag)
        {
            float step = speed * Time.deltaTime;
            spinWheelSystem.transform.position = Vector3.MoveTowards(spinWheelSystem.transform.position, new Vector3(-0.83f, 0, 0), step);
            freeSpinButton.transform.position = Vector3.MoveTowards(freeSpinButton.transform.position, new Vector3(2.754f, -0.007f, 0), step);
            paidSpinButton.transform.position = Vector3.MoveTowards(paidSpinButton.transform.position, new Vector3(2.754f, -0.007f, 0), step);

        }*/

    }

    IEnumerator EnableFreeSpinWheel ()
    {
        yield return new WaitForSeconds(0.6f);

        spinWheelSystem.SetActive(true);

        freeSpinButton.SetActive(true);
        if(spriteFlag)
        {

            SpinWheel.Instance.WheelAnimation(true);
        }
        spriteFlag = true;


    }

    IEnumerator EnablePaidSpinWheel()
    {
        yield return new WaitForSeconds(0f);

        spinWheelSystem.SetActive(true);

        paidSpinButton.SetActive(true);
        SpinWheel.Instance.WheelAnimation(true);


    }


    void FillFreeSpin()
    {
        tempSprites.Clear();
        freeSpinItemList.Clear();
        if (payspintList.Count == 0)
        {
            return;
        }
        else
        {
            int count = 8 - payspintList.Count;
            for (int i = payspintList.Count; i < 8; i++)
            {
                int range = UnityEngine.Random.Range(0, payspintList.Count);
                payspintList.Add(payspintList[range]);
            }
        }
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

        //skyblue.GetComponent<Image>().sprite = skyblueSprite;
        if (skyblueSprite != null)
        {
            tempSprites.Add(skyblueSprite);
        }


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

        //blue.GetComponent<Image>().sprite = blueSprite;
        if (blueSprite != null)
        {
            tempSprites.Add(blueSprite);
        }


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

        //vilote.GetComponent<Image>().sprite = viloteSprite;
        if (viloteSprite != null)
        {
            tempSprites.Add(viloteSprite);
        }

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

        //purple.GetComponent<Image>().sprite = purpleSprite;
        if (purpleSprite != null)
        {
            tempSprites.Add(purpleSprite);
        }


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

        //red.GetComponent<Image>().sprite = redSprite;
        if (redSprite != null)
        {
            tempSprites.Add(redSprite);
        }



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

        //yelolow.GetComponent<Image>().sprite = yellowSprite;
        if (yellowSprite != null)
        {
            tempSprites.Add(yellowSprite);
        }


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

        //green.GetComponent<Image>().sprite = greenSprite;
        if (greenSprite != null)
        {
            tempSprites.Add(greenSprite);
        }

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

        //darkvilote.GetComponent<Image>().sprite = darkvioletSprite;
        if (darkvioletSprite != null)
        {
            tempSprites.Add(darkvioletSprite);
        }
        switch (tempSprites.Count)
        {
            case 1:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                blue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                vilote.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                purple.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                red.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                yelolow.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                green.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                darkvilote.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                break;
            case 2:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];

                vilote.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                purple.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];

                red.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                yelolow.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];

                green.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];

                darkvilote.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];

                break;
            case 3:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                vilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                purple.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                red.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                yelolow.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                green.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                darkvilote.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                break;
            case 4:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                vilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                purple.GetComponent<Image>().sprite = tempSprites[3];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[3];
                red.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                yelolow.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                green.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                darkvilote.GetComponent<Image>().sprite = tempSprites[3];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[3];
                break;
            case 5:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                vilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                purple.GetComponent<Image>().sprite = tempSprites[3];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[3];
                red.GetComponent<Image>().sprite = tempSprites[4];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[4];
                yelolow.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                green.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                darkvilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                break;
            case 6:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                vilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                purple.GetComponent<Image>().sprite = tempSprites[3];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[3];
                red.GetComponent<Image>().sprite = tempSprites[4];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[4];
                yelolow.GetComponent<Image>().sprite = tempSprites[5];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[5];
                green.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                darkvilote.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                break;
            case 7:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                vilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                purple.GetComponent<Image>().sprite = tempSprites[3];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[3];
                red.GetComponent<Image>().sprite = tempSprites[4];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[4];
                yelolow.GetComponent<Image>().sprite = tempSprites[5];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[5];
                green.GetComponent<Image>().sprite = tempSprites[6];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[6];
                darkvilote.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                break;
            case 8:
                skyblue.GetComponent<Image>().sprite = tempSprites[0];
                spinParent.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
                blue.GetComponent<Image>().sprite = tempSprites[1];
                spinParent.transform.GetChild(1).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[1];
                vilote.GetComponent<Image>().sprite = tempSprites[2];
                spinParent.transform.GetChild(2).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[2];
                purple.GetComponent<Image>().sprite = tempSprites[3];
                spinParent.transform.GetChild(3).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[3];
                red.GetComponent<Image>().sprite = tempSprites[4];
                spinParent.transform.GetChild(4).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[4];
                yelolow.GetComponent<Image>().sprite = tempSprites[5];
                spinParent.transform.GetChild(5).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[5];
                green.GetComponent<Image>().sprite = tempSprites[6];
                spinParent.transform.GetChild(6).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[6];
                darkvilote.GetComponent<Image>().sprite = tempSprites[7];
                spinParent.transform.GetChild(7).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[7];
                break;
        }

        //int ind = 0;
        //for(int i = 0;i<spinParent.transform.childCount;i++)
        //{
        //    spinParent.transform.GetChild(i).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = tempSprites[0];
        //    ind++;
        //}
    }


    Sprite GenerateEyes(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_02";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        skyblue.GetComponent<Image>().sprite = newSprite;
        freeSpinItemList.Add("Eyes");
        return newSprite;
    }



    Sprite GenerateNose(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;


        string filename = "GameItemIcon/PS_Gamescreen_Icon_04";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Nose");
        return newSprite;
    }

    Sprite GenerateLips(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_03";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Lips");
        return newSprite;

    }


    Sprite GenerateEar(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_05";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Ears");
        return newSprite;



    }

    Sprite GenerateHair(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_01";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Hair");
        return newSprite;

    }

    Sprite GenerateHairBand(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_06";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Hair band");
        return newSprite;
    }


    Sprite GenerateCaps(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_07";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Cap");
        return newSprite;
    }



    Sprite GenerateTiara(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_09";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Tiara");
        return newSprite;
    }



    Sprite GenerateDress(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_08";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Dress");
        return newSprite;
    }


    Sprite GenerateMouchstache(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_11";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Moustache");
        return newSprite;
    }


    Sprite GenerateBeards(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_12";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Beard");
        return newSprite;

    }


    Sprite GenerateGoggles(int itemNumber)
    {

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_10";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Goggle");
        return newSprite;
    }



    Sprite GenerateAnimals(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;

        string filename = "GameItemIcon/PS_Gamescreen_Icon_14";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Animal");
        return newSprite;
    }


    Sprite GenerateBirds(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;

        string filename = "GameItemIcon/PS_Gamescreen_Icon_15";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Bird");
        return newSprite;
    }


    Sprite GenerateInsects(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_16";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Insect");
        return newSprite;

    }


    Sprite GenerateFishes(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_17";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Fish");
        return newSprite;
    }

    Sprite GenerateCartoonMovieCharacters(int itemNumber)
    {

        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_19";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Movie character");
        return newSprite;
    }


    Sprite GenerateGirlStickers(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_18";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Girl sticker");
        return newSprite;
    }

    Sprite GenerateMixedStickers(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_23";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Mixed sticker");
        return newSprite;
    }



    Sprite GenerateFunnyFaces(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_20";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Faces");
        return newSprite;
    }


    Sprite GenerateEmoticons(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_21";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Emoticons");
        return newSprite;
    }


    Sprite GenerateText(int itemNumber)
    {
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_22";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Text stickers");
        return newSprite;
    }


    Sprite GenerateTatooes(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_26";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Tattoos");
        return newSprite;
    }



    Sprite GenerateGlitterStars(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "GameItemIcon/PS_Gamescreen_Icon_25";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Glitter stars");
        return newSprite;
    }



    Sprite GenerateGlitterHearts(int itemNumber)
    {
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "GameItemIcon/PS_Gamescreen_Icon_24";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Glitter hearts");
        return newSprite;
    }

    Sprite GenerateMirrorframes(int itemNumber)
    {

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_13";
        Sprite newSprite = Resources.Load<Sprite>(filename);
        freeSpinItemList.Add("Mirror frames");
        return newSprite;
    }



    // Update is called once per frame
    public void buttonclick()
    {
        freeSpinHomeButton.interactable = false;
        SpinwheelRotateSoundClicked();
        //StartCoroutine(Spin());
        PlayerPrefs.SetInt("ButtonEnabled", 0);

        PlayerPrefs.SetString("SystTime", System.DateTime.Now.ToString());
        //spinButton.interactable = false;
        freeSpinButtonDisable.SetActive(true);
        //SpinImage.SetActive(false);
    }

    private IEnumerator Spin()
    {
        yield return null;
        coroutineAllowed = false;
        randomvalue = UnityEngine.Random.Range(50, 60);

        timeInterval = 0.08f;


        for (int i = 0; i < randomvalue; i++)
        {

            freeSpinBg.transform.Rotate(0, 0, -22.5f);
            if (i > Mathf.RoundToInt(randomvalue * 0.5f))
                timeInterval = 0.15f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.6f))
                timeInterval = 0.1f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.7f))
                timeInterval = 0.9f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.8f))
                timeInterval = 0.7f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.9f))
                timeInterval = 0.5f;
            yield return new WaitForSeconds(timeInterval);


        }


        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            freeSpinBg.transform.Rotate(0, 0, 22.5f);
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        Debug.Log(finalAngle);
        switch (finalAngle)
        {
            case 0:
                Debug.Log(payspintList[0].jackpotid);
                jacpotnumber = payspintList[0].jackpotid;
                Jackpot jackpot = payspintList[0];
                SetJackpotPurchaseSucess(jackpot, 0);
                break;
            case 45:
                Debug.Log(payspintList[1].jackpotid);
                jacpotnumber = payspintList[1].jackpotid;
                Jackpot jackpot1 = payspintList[1];
                SetJackpotPurchaseSucess(jackpot1, 1);
                break;
            case 90:
                Debug.Log(payspintList[2].jackpotid);
                jacpotnumber = payspintList[2].jackpotid;
                Jackpot jackpot2 = payspintList[2];
                SetJackpotPurchaseSucess(jackpot2, 2);
                break;
            case 135:
                Debug.Log(payspintList[3].jackpotid);
                jacpotnumber = payspintList[3].jackpotid;
                Jackpot jackpot3 = payspintList[3];
                SetJackpotPurchaseSucess(jackpot3, 3);
                break;
            case 180:
                Debug.Log(payspintList[4].jackpotid);
                jacpotnumber = payspintList[4].jackpotid;
                Jackpot jackpot4 = payspintList[4];
                SetJackpotPurchaseSucess(jackpot4, 4);
                break;
            case 225:
                Debug.Log(payspintList[5].jackpotid);
                jacpotnumber = payspintList[5].jackpotid;
                Jackpot jackpot5 = payspintList[5];
                SetJackpotPurchaseSucess(jackpot5, 5);
                break;
            case 270:
                Debug.Log(payspintList[6].jackpotid);
                jacpotnumber = payspintList[6].jackpotid;
                Jackpot jackpot6 = payspintList[6];
                SetJackpotPurchaseSucess(jackpot6, 6);
                break;
            case 315:
                Debug.Log(payspintList[7].jackpotid);
                jacpotnumber = payspintList[7].jackpotid;
                Jackpot jackpot7 = payspintList[7];
                SetJackpotPurchaseSucess(jackpot7, 7);
                break;


        }
        SpinwheelRotateSoundOffClicked();
        PaidSpinCompleted = true;
        SpinwheelCheeringSound();
        StartCoroutine(FreeSpinWheelWinPopup());
        //freePopUpPanel.SetActive(true);
        //freePopUpText.text = "You win free stickes";
        
        coroutineAllowed = true;
    }
    public void BackFreeButtonClicked()
    {
        clicksound();
        PaidSpinCompleted = false;
        //SpinwheelCheeringSoundOff();
        //WinanimationHeader.SetActive(false);
        Debug.Log("Backbuttonpressed");
        spinWheelSystem.SetActive(false);
        freeSpinButton.SetActive(false);
        paidSpinButton.SetActive(false);
        
        //SceneManager.LoadScene("HomeScene");
        //uiManager.homeScreen.SetActive(true);
        //uiManager.spinWheelScreen.SetActive(false);
        //uiManager.spinWheelCanvas.SetActive(false);
        uiManager.ChooseSpinWheelScreen("CenterToLeft");
        uiManager.ChooseHomeScreen("RightToCenter");
    }

    public void OkFreeButtonClicked()
    {
        clicksound();
        PaidSpinCompleted = false;
        SpinwheelCheeringSoundOff();
        //WinanimationHeader.SetActive(false);
        Debug.Log("Okbuttonpressed");
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                transform.GetComponent<IAPManager>().OnPaidSpinWheelClicked();
            }
            else
            {
                print("PaidNO NETWORK");
                payPopUpPanel.SetActive(false);
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            transform.GetComponent<IAPManager>().OnPaidSpinWheelClicked();
        }
        else
        {
            print("PaidNO NETWORK");
            payPopUpPanel.SetActive(false);
            PanelPopupNoNetwork.SetActive(true);
        }
    }

    public void NoNetworkOKButtonClicked()
    {
        clicksound();
        PanelPopupNoNetwork.SetActive(false);
        BackFreeButtonClicked();
    }

    public void SpinDone(int idx)
    {
        if (freeSpinButton.activeSelf)
        {
            jacpotnumber = payspintList[idx].jackpotid;
            Jackpot jackpot = payspintList[idx];
            SetJackpotPurchaseSucess(jackpot, idx);
        }
        else
        {
            
            switch (idx)
            {
                case 0:
                    Shop shop0 = unlockShopList[0];
                    SetShopPurchaseSucess(shop0);
                    break;
                case 1:
                    Shop shop1 = unlockShopList[1];
                    SetShopPurchaseSucess(shop1);
                    break;
                case 2:
                    Shop shop2 = unlockShopList[2];
                    SetShopPurchaseSucess(shop2);
                    break;
                case 3:
                    Shop shop3 = unlockShopList[3];
                    SetShopPurchaseSucess(shop3);
                    break;
                case 4:
                    Shop shop4 = unlockShopList[4];
                    SetShopPurchaseSucess(shop4);
                    break;
                case 5:
                    Shop shop5 = unlockShopList[5];
                    SetShopPurchaseSucess(shop5);
                    break;
                case 6:
                    Shop shop6 = unlockShopList[6];
                    SetShopPurchaseSucess(shop6);
                    break;
                case 7:
                    Shop shop7 = unlockShopList[7];
                    SetShopPurchaseSucess(shop7);
                    break;


            }
            DateTime stopTime = System.DateTime.Now;
            SpinwheelRotateSoundOffClicked();
            //paidPopUpPanel.SetActive(true);
            StartCoroutine(DisplayPopup());

        }
    }

    

    private void SetJackpotPurchaseSucess(Jackpot jackpot, int index)
    {
        print(freeSpinItemList[index]);
        freePopUpText.text = "You win free " + freeSpinItemList[index];
        DatabaseManager.SetJackpotPurchaseSucess(jackpot);
        StartCoroutine(DisplayPopup());
    }

    private IEnumerator DisplayPopup()
    {
        yield return new WaitForSeconds(2f);
        SpinwheelCheeringSound();
        if (freeSpinButton.activeSelf)
        {
            freePopUpPanel.SetActive(true);
        }
        else
        {
            paidPopUpPanel.SetActive(true);
            
        }
        spinWheelSystem.SetActive(false);
        freeSpinButton.SetActive(false);
        paidSpinButton.SetActive(false);
        yield return new WaitForSeconds(4f);
        paidSpinHomeButton.interactable = true;
        freeSpinHomeButton.interactable = true;
        transform.GetComponent<Wheel>().EnableSpinButtons(true);
    }

    public void DisableHomeButton()
    {

        paidSpinHomeButton.interactable = false;
        freeSpinHomeButton.interactable = false;
    }

    public void EnableSpinButtons(bool value)
    {
        freeSpinButtonBoxCollider.enabled = value;
        paidSpinButtonBoxCollider.enabled = value;
    }

    void FillPaidSpin()
    {
        totalList.Clear();
        unlockShopList.Clear();
        List<int> tempList = new List<int>();
        for (int i = 2; i < shopsList.Count; i++)
        {
            if (shopsList[i].unlockflag == 0)
            {
                totalList.Add(i);
                tempList.Add(i);
                //unlockShopList.Add(shopsList[i]);
            }
        }
        if (totalList.Count == 0)
        {
            return;
        }
        int rangeValue = 0;
        if (totalList.Count < 8)
        {
            print("total" + totalList.Count);
            /*int count = 8 - totalList.Count;
            for (int i = 0; i < totalList.Count; i++)
            {
                if (tempList.Count <= 0)
                {
                    tempList = totalList;
                }
                rangeValue = UnityEngine.Random.Range(0, tempList.Count);
                paidSpinImages[i].sprite = paidSpinLogoImages[tempList[rangeValue]];
                spinParent.transform.GetChild(i).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = paidSpinLogoImages[tempList[rangeValue]];
                tempList.RemoveAt(rangeValue);
            }
            for (int i = totalList.Count, j = 0; i < 8; i++, j++)
            {
                if (j >= totalList.Count)
                {
                    j = 0;
                }
                rangeValue = UnityEngine.Random.Range(0, totalList.Count);
                paidSpinImages[i].sprite = paidSpinLogoImages[totalList[j]];
                spinParent.transform.GetChild(j).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = paidSpinLogoImages[totalList[j]];

                unlockShopList.Add(shopsList[j]);
            }*/
            for(int i = 0;i<totalList.Count;i++)
            {
                spinParent.transform.GetChild(i).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = paidSpinLogoImages[totalList[i]];
                unlockShopList.Add(shopsList[totalList[i]]);
            }
            for(int i = totalList.Count;i<8;i++)
            {
                rangeValue = UnityEngine.Random.Range(0, totalList.Count);
                spinParent.transform.GetChild(i).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = paidSpinLogoImages[totalList[rangeValue]];
                unlockShopList.Add(shopsList[totalList[rangeValue]]);
            }

        }
        else
        {
            /*for (int i = 0; i < 8; i++)
            {
                if (tempList.Count <= 0)
                {
                    tempList = totalList;
                }
                rangeValue = UnityEngine.Random.Range(0, tempList.Count);
                paidSpinImages[i].sprite = paidSpinLogoImages[tempList[rangeValue]];
                spinParent.transform.GetChild(i).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = paidSpinLogoImages[tempList[rangeValue]];

                tempList.RemoveAt(rangeValue);
            }*/
            for (int i = 0; i < 8; i++)
            {
                spinParent.transform.GetChild(i).transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = paidSpinLogoImages[totalList[i]];
                unlockShopList.Add(shopsList[totalList[i]]);
            }
        }
    }
    public void buttonPaidclick()
    {
        
        freeSpinButton.SetActive(false);
        DateTime currentTime = System.DateTime.Now;
        SpinwheelRotateSoundClicked();
        transform.GetComponent<SpinWheelManager>().OnClickPaidSpin();
        //StartCoroutine(PaidSpin());
        paidSpinHomeButton.interactable = false;
        //spinPaidButton.interactable = false;
        paidSpinButtonDisable.SetActive(true);
        SpinImage.SetActive(false);
    }

    public void OnPaidSpinPurchaseSuccessfull()
    {
        //spinPaidButton.interactable = true;
        paidSpinButtonDisable.SetActive(false);
        uiManager.paidSpinWheelScreen.SetActive(true);
        uiManager.freeSpinWheelScreen.SetActive(false);
        StartCoroutine(CallPaidSpin());
    }

    public void OnPaidSpinPurchaseFailed()
    {
        payPopUpPanel.SetActive(false);
        purchaseFailedPopUp.GetComponent<Popup>().Toggle();
    }

    public void OnPaidSpinPurchaseFailedOKButtonClicked()
    {
        clicksound();
        uiManager.ChooseSpinWheelScreen("CenterToLeft");
        uiManager.ChooseHomeScreen("RightToCenter");
        purchaseFailedPopUp.GetComponent<Popup>().Toggle();
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
    private IEnumerator CallPaidSpin()
    {
        StartCoroutine(EnablePaidSpinWheel());
        DisableHomeButton();
        yield return new WaitForSeconds(2f);
        buttonPaidclick();
    }

    private IEnumerator PaidSpin()
    {
        yield return null;
        coroutineAllowed = false;
        randomvalue = UnityEngine.Random.Range(50, 60);

        timeInterval = 0.08f;


        for (int i = 0; i < randomvalue; i++)
        {

            paidSpinBg.transform.Rotate(0, 0, -22.5f);
            if (i > Mathf.RoundToInt(randomvalue * 0.5f))
                timeInterval = 0.15f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.6f))
                timeInterval = 0.1f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.7f))
                timeInterval = 0.9f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.8f))
                timeInterval = 0.7f;
            else if (i > Mathf.RoundToInt(randomvalue * 0.9f))
                timeInterval = 0.5f;
            yield return new WaitForSeconds(timeInterval);


        }


        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            paidSpinBg.transform.Rotate(0, 0, 22.5f);
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        Debug.Log(finalAngle);
        switch (finalAngle)
        {
            case 0:
                Shop shop0 = unlockShopList[0];
                SetShopPurchaseSucess(shop0);
                break;
            case 45:
                Shop shop1 = unlockShopList[1];
                SetShopPurchaseSucess(shop1);
                break;
            case 90:
                Shop shop2 = unlockShopList[2];
                SetShopPurchaseSucess(shop2);
                break;
            case 135:
                Shop shop3 = unlockShopList[3];
                SetShopPurchaseSucess(shop3);
                break;
            case 180:
                Shop shop4 = unlockShopList[4];
                SetShopPurchaseSucess(shop4);
                break;
            case 225:
                Shop shop5 = unlockShopList[5];
                SetShopPurchaseSucess(shop5);
                break;
            case 270:
                Shop shop6 = unlockShopList[6];
                SetShopPurchaseSucess(shop6);
                break;
            case 315:
                Shop shop7 = unlockShopList[7];
                SetShopPurchaseSucess(shop7);
                break;


        }
        DateTime stopTime = System.DateTime.Now;
        SpinwheelRotateSoundOffClicked();
        //WinanimationHeader.SetActive(true);
        PaidSpinCompleted = true;
        SpinwheelCheeringSound();
        StartCoroutine(SpinWheelWinPopup());
        //paidPopUpPanel.SetActive(true);
        //homeButton.interactable = true;
        //paidPopUpText.text = "You win free stickes";

        coroutineAllowed = true;
    }
    public IEnumerator SpinWheelWinPopup()
    {
        yield return new WaitForSeconds(3f);
        paidPopUpPanel.SetActive(true);
    }
    public IEnumerator FreeSpinWheelWinPopup()
    {
        yield return new WaitForSeconds(3f);
        freePopUpPanel.SetActive(true);
    }
    private void SetShopPurchaseSucess(Shop shop)
    {
        paidPopUpText.text = "You win free " + shop.shopname;
        DatabaseManager.SetShopPurchaseSucess(shop);
        switch (shop.shopid)
        {
            case 1:
                if (shopsList[2].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[2]);
                }
                if (shopsList[3].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[3]);
                }
                if (shopsList[4].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[4]);
                }
                if (shopsList[5].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[5]);
                }
                if (shopsList[6].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[6]);
                }
                if (shopsList[7].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[7]);
                }
                if (shopsList[8].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[8]);
                }
                break;
            case 2:
                if (shopsList[9].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[9]);
                }
                if (shopsList[10].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[10]);
                }
                if (shopsList[11].unlockflag == 0)
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[11]);
                }
                break;
            case 3:
                if ((shopsList[3].unlockflag == 1) && (shopsList[4].unlockflag == 1) && (shopsList[5].unlockflag == 1)
                    && (shopsList[6].unlockflag == 1) && (shopsList[7].unlockflag == 1) && (shopsList[8].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 4:
                if ((shopsList[2].unlockflag == 1) && (shopsList[4].unlockflag == 1) && (shopsList[5].unlockflag == 1)
                    && (shopsList[6].unlockflag == 1) && (shopsList[7].unlockflag == 1) && (shopsList[8].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 5:
                if ((shopsList[2].unlockflag == 1) && (shopsList[3].unlockflag == 1) && (shopsList[5].unlockflag == 1)
                    && (shopsList[6].unlockflag == 1) && (shopsList[7].unlockflag == 1) && (shopsList[8].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 6:
                if ((shopsList[2].unlockflag == 1) && (shopsList[3].unlockflag == 1) && (shopsList[4].unlockflag == 1)
                    && (shopsList[6].unlockflag == 1) && (shopsList[7].unlockflag == 1) && (shopsList[8].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 7:
                if ((shopsList[2].unlockflag == 1) && (shopsList[3].unlockflag == 1) && (shopsList[4].unlockflag == 1)
                    && (shopsList[5].unlockflag == 1) && (shopsList[7].unlockflag == 1) && (shopsList[8].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 8:
                if ((shopsList[2].unlockflag == 1) && (shopsList[3].unlockflag == 1) && (shopsList[4].unlockflag == 1)
                    && (shopsList[5].unlockflag == 1) && (shopsList[6].unlockflag == 1) && (shopsList[8].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 9:
                if ((shopsList[2].unlockflag == 1) && (shopsList[3].unlockflag == 1) && (shopsList[4].unlockflag == 1)
                    && (shopsList[5].unlockflag == 1) && (shopsList[6].unlockflag == 1) && (shopsList[7].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[0]);
                }
                break;
            case 10:
                if ((shopsList[10].unlockflag == 1) && (shopsList[11].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[1]);
                }
                break;
            case 11:
                if ((shopsList[9].unlockflag == 1) && (shopsList[11].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[1]);
                }
                break;
            case 12:
                if ((shopsList[9].unlockflag == 1) && (shopsList[10].unlockflag == 1))
                {
                    DatabaseManager.SetShopPurchaseSucess(shopsList[1]);
                }
                break;
        }
    }

    public void SpinwheelRotateSoundClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(wheelRotateSound);
        }
        
    }
    public void SpinwheelRotateSoundOffClicked()
    {
        AudioManager.Instance.StopSound();
    }
    public void clicksound()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(buttonSound1);
        }


    }
    public void SpinwheelCheeringSound()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(cheeringSound);
        }

    }
    public void SpinwheelCheeringSoundOff()
    {
        AudioManager.Instance.StopSound();
    }

}
