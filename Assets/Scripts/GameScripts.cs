using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Model;
using Platinio;


public class GameScripts : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler

{
    Transform lockimage;

    public GameObject StickerObject;
    public GameObject StickerObjectIphone;
    public GameObject Stickers;

    public static GameObject itemBeingDragged;
    public string index1;
    public GameObject SavePanel;
    public GameObject PurchasePanel;

    public GameObject SavePanelIPhone;
    public GameObject PurchasePanelIPhone;

    private Text panelPurchaseText;
    private int confirmationFlag = 0;

    // Use this for initialization
    public string HairName = "";
    public string HairName1 = "";
    public string HairName2 = "";
    public string namecap = "";


    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    public GameObject scrollItemPrefab2;
    public ScrollRect scrollView2;
    public GameObject scrollContent2;
    public ScrollRect scrollViewIPhone;
    public GameObject scrollContentIPhone;
    public ScrollRect scrollView2IPhone;
    public GameObject scrollContent2IPhone;



    public int TOTAL_SKIN_TONE = 5;
    public int TOTAL_ICON = 26;


    public int TOTAL_HAIRS = 15;
    public int TOTAL_EYES = 15;
    public int TOTAL_LIPS = 15;
    public int TOTAL_NOSES = 15;
    public int TOTAL_EARS = 15;
    public int TOTAL_HAIRBANDS = 25;
    public int TOTAL_CAPS = 15;
    public int TOTAL_DRESS = 15;
    public int TOTAL_TIARA = 15;
    public int TOTAL_GOGGLE = 15;
    public int TOTAL_MOUCHSTACHE = 20;
    public int TOTAL_BEARDS = 20;
    public int TOTAL_MIRRORFRAME = 15;
    public int TOTAL_ANIMALS = 30;
    public int TOTAL_BIRDS = 30;
    public int TOTAL_INSECTS = 30;
    public int TOTAL_FISHES = 30;
    public int TOTAL_GIRLS_CHARACTER = 30;
    public int TOTAL_MOVIE_CHARACTERS = 30;
    public int TOTAL_FUNNY_FACES = 30;
    public int TOTAL_EMOTIONS_STICKERS = 30;
    public int TOTAL_TEXT_STICKERS = 30;
    public int TOTAL_MIXED_STICKERS = 30;
    public int TOTAL_HEARTS = 30;
    public int TOTAL_STARS = 30;
    public int TOTAL_TATOOES = 30;




    private bool isFirstItemSelected = false;

    public Image faceImage;
    public Image hairBackImage;
    public Image hairFrontImage;
    public Image eyeImage;
    public Image lipImage;
    public Image noseImage;
    public Image earImage;
    public Image haribandImage;
    public Image capImage;
    public Image dressImage;
    public Image tiaraImage;
    public Image goggleImage;
    public Image mouchstacheImage;
    public Image beardImage;
    public Image mirrorframeImage;

    public Image DeleteText;
    public Image DeleteImage;

    public Image faceImageIPhone;
    public Image hairBackImageIPhone;
    public Image hairFrontImageIPhone;
    public Image eyeImageIPhone;
    public Image lipImageIPhone;
    public Image noseImageIPhone;
    public Image earImageIPhone;
    public Image haribandImageIPhone;
    public Image capImageIPhone;
    public Image dressImageIPhone;
    public Image tiaraImageIPhone;
    public Image goggleImageIPhone;
    public Image mouchstacheImageIPhone;
    public Image beardImageIPhone;
    public Image mirrorframeImageIPhone;

    public Image DeleteTextIPhone;
    public Image DeleteImageIPhone;


    public GameObject StickerObjectIPad;
    public GameObject SavePanelIPad;
    public GameObject PurchasePanelIPad;

    public ScrollRect scrollViewIPad;
    public GameObject scrollContentIPad;
    public ScrollRect scrollView2IPad;
    public GameObject scrollContent2IPad;

    public Image faceImageIPad;
    public Image hairBackImageIPad;
    public Image hairFrontImageIPad;
    public Image eyeImageIPad;
    public Image lipImageIPad;
    public Image noseImageIPad;
    public Image earImageIPad;
    public Image haribandImageIPad;
    public Image capImageIPad;
    public Image dressImageIPad;
    public Image tiaraImageIPad;
    public Image goggleImageIPad;
    public Image mouchstacheImageIPad;
    public Image beardImageIPad;
    public Image mirrorframeImageIPad;

    public Image DeleteTextIPad;
    public Image DeleteImageIPad;

    public AudioClip buttonSound1;
    public AudioClip BgMusic;

    public GameObject loadingScreen;

    public AudioClip[] musicarray = new AudioClip[30];



    //public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;





    private string selectedShape = "";
    private string selectedSkinColor = "";
    private int selectedSkinColorIndex = 0;

    private int categoryid;
    List<Items> itemList = new List<Items>();
    private UIManager uiManager;

    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();
        //GameData.SelectedFaceIndex = 8; //for testing

        //StartCoroutine(PlayMusic());
        print((float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1));
        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            StickerObject = StickerObjectIphone;
            PurchasePanel = PurchasePanelIPhone;
            SavePanel = SavePanelIPhone;
            scrollView = scrollViewIPhone;
            scrollContent = scrollContentIPhone;
            scrollView2 = scrollView2IPhone;
            scrollContent2 = scrollContent2IPhone;

            faceImage = faceImageIPhone;
            hairBackImage = hairBackImageIPhone;
            hairFrontImage = hairFrontImageIPhone;
            eyeImage = eyeImageIPhone;
            lipImage = lipImageIPhone;
            noseImage = noseImageIPhone;
            earImage = earImageIPhone;
            haribandImage = haribandImageIPhone;
            capImage = capImageIPhone;
            dressImage = dressImageIPhone;
            tiaraImage = tiaraImageIPhone;
            goggleImage = goggleImageIPhone;
            mouchstacheImage = mouchstacheImageIPhone;
            beardImage = beardImageIPhone;
            mirrorframeImage = mirrorframeImageIPhone;

            DeleteText = DeleteTextIPhone;
            DeleteImage = DeleteImageIPhone;
        }
        else if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            StickerObject = StickerObjectIPad;
            PurchasePanel = PurchasePanelIPad;
            SavePanel = SavePanelIPad;
            scrollView = scrollViewIPad;
            scrollContent = scrollContentIPad;
            scrollView2 = scrollView2IPad;
            scrollContent2 = scrollContent2IPad;

            faceImage = faceImageIPad;
            hairBackImage = hairBackImageIPad;
            hairFrontImage = hairFrontImageIPad;
            eyeImage = eyeImageIPad;
            lipImage = lipImageIPad;
            noseImage = noseImageIPad;
            earImage = earImageIPad;
            haribandImage = haribandImageIPad;
            capImage = capImageIPad;
            dressImage = dressImageIPad;
            tiaraImage = tiaraImageIPad;
            goggleImage = goggleImageIPad;
            mouchstacheImage = mouchstacheImageIPad;
            beardImage = beardImageIPad;
            mirrorframeImage = mirrorframeImageIPad;

            DeleteText = DeleteTextIPad;
            DeleteImage = DeleteImageIPad;
        }

        panelPurchaseText = PurchasePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        scrollContent2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 950f);

        SetSelectedSkinColor();
        SetSelectedShape();


        hairBackImage.enabled = false;
        hairFrontImage.enabled = false;
        eyeImage.enabled = false;
        lipImage.enabled = false;
        noseImage.enabled = false;
        earImage.enabled = false;
        haribandImage.enabled = false;
        capImage.enabled = false;
        dressImage.enabled = false;
        tiaraImage.enabled = false;
        goggleImage.enabled = false;
        mouchstacheImage.enabled = false;
        beardImage.enabled = false;
        mirrorframeImage.enabled = true;
        //SavePanel.SetActive(false);
        DeleteText.enabled = false;
        DeleteImage.enabled = false;
        //PurchasePanel.SetActive(false);





        Debug.Log("Selected Face : " + GameData.SelectedFaceIndex);
        displayFace();

        for (int index = 1; index <= TOTAL_ICON; index++)
        {

            generateIconItem(index);
            //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
        }

        scrollView.verticalNormalizedPosition = 1.0f;
        scrollView2.verticalNormalizedPosition = 1.0f;
        // scrollView.normalizedPosition = new Vector2(0.0f, 0.0f);  
        scrollView.onValueChanged.AddListener(scrollRectCallBack);



    }
    public void EnableAllgameDetails()
    {
        StartCoroutine(PlayMusic());
        confirmationFlag = 0;
        RemoveScrollItem(scrollContent);
        RemoveScrollItem(scrollContent2);
        scrollContent2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 950f);
        scrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 950f);
        SetSelectedSkinColor();
        SetSelectedShape();

        hairBackImage.enabled = false;
        hairFrontImage.enabled = false;
        eyeImage.enabled = false;
        lipImage.enabled = false;
        noseImage.enabled = false;
        earImage.enabled = false;
        haribandImage.enabled = false;
        capImage.enabled = false;
        dressImage.enabled = false;
        tiaraImage.enabled = false;
        goggleImage.enabled = false;
        mouchstacheImage.enabled = false;
        beardImage.enabled = false;
        mirrorframeImage.enabled = true;
        string filename2 = "New UI/Game Screen/Pinthesticker_Gamescreen_NewFrame_DefaultFrame";
        Sprite mirror = Resources.Load<Sprite>(filename2);
        mirrorframeImage.sprite = mirror;
        //SavePanel.SetActive(false);
        DeleteText.enabled = false;
        DeleteImage.enabled = false;
        HairName = "";
        //PurchasePanel.SetActive(false);
        Debug.Log("Selected Face : " + GameData.SelectedFaceIndex);
        displayFace();

        for (int index = 1; index <= TOTAL_ICON; index++)
        {

            generateIconItem(index);
            //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
        }

        scrollView.verticalNormalizedPosition = 1.0f;
        scrollView2.verticalNormalizedPosition = 1.0f;
        // scrollView.normalizedPosition = new Vector2(0.0f, 0.0f);  
        scrollView.onValueChanged.AddListener(scrollRectCallBack);
        foreach (Transform t in StickerObject.transform)
        {
            Destroy(t.gameObject);
        }
    }
    public void UnlockAllgameDetails()
    {
        RemoveScrollItem(scrollContent);
        RemoveScrollItem(scrollContent2);
        scrollContent2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 950f);
        scrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 950f);
        for (int index = 1; index <= TOTAL_ICON; index++)
        {

            generateIconItem(index);
            //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
        }

        scrollView.verticalNormalizedPosition = 1.0f;
        scrollView2.verticalNormalizedPosition = 1.0f;
        // scrollView.normalizedPosition = new Vector2(0.0f, 0.0f);  
        scrollView.onValueChanged.AddListener(scrollRectCallBack);
        /*foreach (Transform t in StickerObject.transform)
        { 
            Destroy(t.gameObject);
        }*/
    }


    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(0.1f);
        int musicFlag = PlayerPrefs.GetInt("music");                           //Music and sound

        if (musicFlag == 0)
        {
            print("GameData.SelectedMusicIndex : " + GameData.SelectedMusicIndex);
            if (GameData.SelectedMusicIndex >= 1)
            {
                AudioManager.Instance.PlayMusic(musicarray[GameData.SelectedMusicIndex - 1]);
            }
            else
            {
                AudioManager.Instance.PlayMusic(BgMusic);
                if (PlayerPrefs.GetInt("music") == 1)
                {
                    AudioManager.Instance.StopMusic();
                }
               // AudioManager.Instance.PlayMusic(musicarray[0]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }







    void SetSelectedSkinColor()
    {
        if (GameData.SelectedFaceIndex % TOTAL_SKIN_TONE == 1)
        {
            selectedSkinColor = "LightBrown";
            selectedSkinColorIndex = 1;
        }
        else if (GameData.SelectedFaceIndex % TOTAL_SKIN_TONE == 2)
        {
            selectedSkinColor = "Yellow";
            selectedSkinColorIndex = 2;
        }
        else if (GameData.SelectedFaceIndex % TOTAL_SKIN_TONE == 3)
        {
            selectedSkinColor = "MediumBrown";
            selectedSkinColorIndex = 3;
        }
        else if (GameData.SelectedFaceIndex % TOTAL_SKIN_TONE == 4)
        {
            selectedSkinColor = "Brown";
            selectedSkinColorIndex = 4;
        }
        else if (GameData.SelectedFaceIndex % TOTAL_SKIN_TONE == 0)
        {
            selectedSkinColor = "DarkBrown";
            selectedSkinColorIndex = 5;
        }



        Debug.Log("selectedSkinColor-" + selectedSkinColor + "-" + selectedSkinColorIndex);
    }





    void SetSelectedShape()
    {
        if (GameData.SelectedFaceIndex <= 5)
        {
            selectedShape = "Circle";
            selectedSkinColorIndex = 1;
        }
        else if (GameData.SelectedFaceIndex <= 10)
        {
            selectedShape = "Oval";
            selectedSkinColorIndex = 2;
        }
        else if (GameData.SelectedFaceIndex <= 15)
        {
            selectedShape = "Rectangle";
            selectedSkinColorIndex = 3;
        }
        else if (GameData.SelectedFaceIndex <= 20)
        {
            selectedShape = "Square";
            selectedSkinColorIndex = 4;
        }
        else if (GameData.SelectedFaceIndex <= 25)
        {
            selectedShape = "Triangle";
            selectedSkinColorIndex = 5;
        }


    }

    public void onBackButton()
    {
        clicksound();
        panelPurchaseText.text = "You have unsaved changes. Do you wish to quit?";
        PurchasePanel.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
        confirmationFlag = 1;
    }
    public void onHomeButton()
    {
        clicksound();
        panelPurchaseText.text = "You have unsaved changes. Do you wish to quit?";
        PurchasePanel.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
        confirmationFlag = 2;
    }

    public void onGalleryButton()
    {
        AudioManager.Instance.StopMusic();
        if (PlayerPrefs.GetInt("music") == 0)
        {
            AudioManager.Instance.PlayMusic(BgMusic);
        }
        clicksound();
        //SceneManager.LoadScene("MyGalleryScene");
        //uiManager.galleryScreen.SetActive(true);
        //uiManager.gameScreen.SetActive(false);

        //uiManager.ChooseGameScreen("CenterToLeft");
        //uiManager.ChooseMusicScreen("CenterToLeft");
        //uiManager.ChooseFaceScreen("CenterToLeft");
        StartCoroutine(ShowLoadingScreen());
    }

    IEnumerator ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1);

        transform.GetComponent<GalleryButton>().SetGalleryDetails();
        transform.GetComponent<GalleryButton>().DisableStartAGameButton();
        uiManager.ChooseGameScreen("CenterToRight");
        transform.GetComponent<GalleryButton>().fromScreenName = "game";
        uiManager.ChooseGalleryScreen("LeftToCenter");
    }

    public void YesButtonClicked()
    {
        AudioManager.Instance.StopMusic();
        if (PlayerPrefs.GetInt("music") == 0)
        {
            AudioManager.Instance.PlayMusic(BgMusic);
        }
        clicksound();
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(false);
        //SceneManager.LoadScene("ShopScene");
        //uiManager.shopScreen.SetActive(true);
        if (confirmationFlag == 1)//Back
        {

            AudioManager.Instance.StopMusic();
            if (PlayerPrefs.GetInt("music") == 0)
            {
                AudioManager.Instance.PlayMusic(BgMusic);
            }
            //AudioManager.Instance.PlayMusic(BgMusic);

            clicksound();

            //SceneManager.LoadScene("ChooseMusic");
            //uiManager.chooseMusicScreen.SetActive(true);
            //uiManager.gameScreen.SetActive(false);
            uiManager.ChooseMusicScreen("RightToCenter");
            uiManager.ChooseGameScreen("CenterToLeft");

        }
        else if(confirmationFlag == 2)//Home
        {

            AudioManager.Instance.StopMusic();
            if (PlayerPrefs.GetInt("music") == 0)
            {
                AudioManager.Instance.PlayMusic(BgMusic);
            }
            //AudioManager.Instance.PlayMusic(BgMusic);
            clicksound();

            //SceneManager.LoadScene("HomeScene");
            //uiManager.homeScreen.SetActive(true);
            //uiManager.gameScreen.SetActive(false);
            uiManager.ChooseHomeScreen("RightToCenter");
            uiManager.ChooseGameScreen("CenterToLeft");
            uiManager.ChooseMusicScreen("CenterToLeft");
            uiManager.ChooseFaceScreen("CenterToLeft");
        }
        else
        {
            transform.GetComponent<ShopScene>().fromScreenName = "game";
            uiManager.ChooseGameScreen("CenterToRight");
            uiManager.ChooseShopScreen("LeftToCenter");
            uiManager.GetComponent<ShopScene>().fetchFromDb();
            //uiManager.gameScreen.SetActive(false);
            //uiManager.ChooseGameScreen("CenterToLeft");
            //uiManager.ChooseMusicScreen("CenterToLeft");
            //uiManager.ChooseFaceScreen("CenterToLeft");
        }
        confirmationFlag = 0;
    }
    public void NoButtonClicked()
    {
        clicksound();
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        confirmationFlag = 0;
       // PurchasePanel.SetActive(false);
    }


    public void OnRepeatButton()
    {
        clicksound();


        hairBackImage.enabled = false;
        hairFrontImage.enabled = false;
        eyeImage.enabled = false;
        lipImage.enabled = false;
        noseImage.enabled = false;
        earImage.enabled = false;
        haribandImage.enabled = false;
        capImage.enabled = false;
        dressImage.enabled = false;
        tiaraImage.enabled = false;
        goggleImage.enabled = false;
        mouchstacheImage.enabled = false;
        beardImage.enabled = false;
        HairName = "";
        // mirrorframeImage.enabled = false;
        string filename2 = "New UI/Game Screen/Pinthesticker_Gamescreen_NewFrame_DefaultFrame";
        Debug.Log(filename2);

        Sprite mirror = Resources.Load<Sprite>(filename2);
        mirrorframeImage.sprite = mirror;




        foreach (Transform t in StickerObject.transform)
        {
            Destroy(t.gameObject);
        }

    }


    public void displayFace()
    {


        //Face
        string filename = "PTS_Game_Big_" + (GameData.SelectedFaceIndex);
        Debug.Log(filename);

        Sprite newSprite = Resources.Load<Sprite>(filename);
        faceImage.sprite = newSprite;


    }

    void scrollRectCallBack(Vector2 value)
    {
        //Debug.Log("callback " + value);
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }


    void RemoveScrollItem(GameObject content)
    {

        int count = content.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            // Debug.Log(i);
            Destroy(content.transform.GetChild(i).gameObject);
        }
        //  Destroy(content.gameObject);
    }





    void generateIconItem(int itemNumber)
    {


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "GameItemIcon/PS_Gamescreen_Icon_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);

        string index1 = "0";
        if (itemNumber < 10)
            index1 = "" + itemNumber;
        else
            index1 = "" + itemNumber;
        string filename2 = "Circle_Selection/Pinthesticker_Gamescreen_CIRCLE_Selection_" + (index1);
        Sprite newSprite2 = Resources.Load<Sprite>(filename2);
        //        Debug.Log(filename2);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab, spawnPosition, spawnRotation);


        scrollItemOb.transform.SetParent(scrollContent.transform, false);
        scrollItemOb.transform.Find("Game Icon1").gameObject.GetComponent<Image>().sprite = newSprite2;

        scrollItemOb.transform.Find("Game Icon1").gameObject.GetComponent<Image>().enabled = false;

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickIconFunction(itemNumber));


        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.transform.Find("Game Icon1").gameObject.GetComponent<Button>().name = filename2;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }


    void onClickIconFunction(int indexSelected)
    {


        if (isFirstItemSelected == false)
        {
            isFirstItemSelected = true;
        }
        else
        {
            RemoveScrollItem(scrollContent2);

        }
        if (indexSelected == 1)
        {//hairs

            categoryid = 5;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);

            for (int index = 1; index <= TOTAL_HAIRS; index++)
            {
                generateHairs(index);

            }


        }
        else if (indexSelected == 2)//eyes
        {
            categoryid = 1;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_EYES; index++)
            {

                generateEyes(index);



            }


        }
        else if (indexSelected == 3)//lips
        {
            categoryid = 3;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_LIPS; index++)
            {

                generateLips(index);

            }

        }
        else if (indexSelected == 4)//Nose
        {
            categoryid = 2;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_NOSES; index++)
            {

                generateNoses(index);

            }

        }
        else if (indexSelected == 5)//Ear
        {
            categoryid = 4;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_EARS; index++)
            {

                generateEars(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }
        else if (indexSelected == 6)//Hairband
        {
            categoryid = 6;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_HAIRBANDS; index++)
            {
                generateHairBands(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }
        else if (indexSelected == 7)//Caps
        {
            categoryid = 7;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_CAPS; index++)
            {
                generateCaps(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }
        else if (indexSelected == 8)//Dress
        {
            categoryid = 9;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_DRESS; index++)
            {
                generateDress(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }




        else if (indexSelected == 9)//Tiara
        {
            categoryid = 8;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_TIARA; index++)
            {
                generateTiara(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }



        else if (indexSelected == 10)//Goggle
        {
            categoryid = 12;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_GOGGLE; index++)
            {
                generateGoggle(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }

        else if (indexSelected == 11)//Mouchstache
        {
            categoryid = 10;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_MOUCHSTACHE; index++)
            {
                generateMouchstache(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }

        else if (indexSelected == 12)//Beards
        {
            categoryid = 11;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_BEARDS; index++)
            {
                generateBeards(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }


        else if (indexSelected == 13)//Mirror frames
        {
            categoryid = 26;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_MIRRORFRAME; index++)
            {
                generateMirrorFrame(index);
                //  scrollView.normalizedPosition = new Vector2(0.0f, 0.0f); 
            }
            // scrollView2.verticalNormalizedPosition = 1.0f;
        }

        // scrollView2.verticalNormalizedPosition = 1.0f;


        else if (indexSelected == 14)
        {
            categoryid = 13;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_ANIMALS; index++)
            {
                generateAnimals(index);

            }

        }


        else if (indexSelected == 15)
        {
            categoryid = 14;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_BIRDS; index++)
            {
                generateBirds(index);

            }

        }

        else if (indexSelected == 16)
        {
            categoryid = 15;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_INSECTS; index++)
            {

                generateInsects(index);

            }

        }
        else if (indexSelected == 17)
        {
            categoryid = 16;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_FISHES; index++)
            {

                generateFishes(index);

            }

        }
        else if (indexSelected == 18)
        {
            categoryid = 18;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_GIRLS_CHARACTER; index++)
            {

                generateGirlsCharacter(index);

            }

        }

        else if (indexSelected == 19)
        {
            categoryid = 17;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_MOVIE_CHARACTERS; index++)
            {

                generateMovieCharacters(index);

            }

        }

        else if (indexSelected == 20)
        {
            categoryid = 20;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_FUNNY_FACES; index++)
            {

                generateFunnyFaces(index);

            }

        }

        else if (indexSelected == 21)
        {
            categoryid = 21;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_EMOTIONS_STICKERS; index++)
            {

                generateEmotionsStickes(index);

            }

        }

        else if (indexSelected == 22)
        {
            categoryid = 22;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_TEXT_STICKERS; index++)
            {

                generateTextStickers(index);

            }

        }

        else if (indexSelected == 23)
        {
            categoryid = 19;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_MIXED_STICKERS; index++)
            {

                generateMixedStickers(index);

            }

        }


        else if (indexSelected == 24)
        {
            categoryid = 25;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_HEARTS; index++)
            {

                generateHeartsStickers(index);

            }

        }

        else if (indexSelected == 25)
        {
            categoryid = 24;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_STARS; index++)
            {

                generateStarsStickers(index);

            }

        }

        else if (indexSelected == 26)
        {
            categoryid = 23;
            itemList = DatabaseManager.GetItems(categoryid);
            roundimage(indexSelected);
            for (int index = 1; index <= TOTAL_TATOOES; index++)
            {

                generateTatooesStickers(index);

            }

        }





    }
    void roundimage(int indexSelected)
    {
        var RoundGreen = GameObject.Find("Circle_Selection/Pinthesticker_Gamescreen_CIRCLE_Selection_1");
        for (int k = 1; k < 27; k++)
        {
            RoundGreen = GameObject.Find("Circle_Selection/Pinthesticker_Gamescreen_CIRCLE_Selection_" + k);
            // Debug.Log(RoundGreen);
            RoundGreen.GetComponent<Image>().enabled = false;
        }
        RoundGreen = GameObject.Find("Circle_Selection/Pinthesticker_Gamescreen_CIRCLE_Selection_" + indexSelected);
        // Debug.Log(RoundGreen);
        RoundGreen.GetComponent<Image>().enabled = true;
    }


    void generateHairs(int itemNumber)
    {
        clicksound();    //sound and Music
        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Hair/PS_Gamescreen_Hairstyles/PS_Gamescreen_Hair_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
       
        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);

        scrollItemOb.transform.SetParent(scrollContent2.transform, false);
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;


        // if (itemNumber==1 ||itemNumber==3)
        if (itemList[itemNumber - 1].unlockflag == 1)

            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onHairLockClickFunction(itemNumber));


        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickHairFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;


        //GetComponent<ScrollRect>().verticalNormalizedPosition = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);


    }

    void onHairLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickHairFunction(int indexSelected)
    {
        clicksound();

        
            if (capImage.enabled == false)
            {
            string index = "0";
            if (indexSelected < 10)
                index = "0" + indexSelected;
            else
                index = "" + indexSelected;


            if (itemList[indexSelected - 1].unlockflag == 1)
            {
                    string filenamHairBack = "Hair/Pin the sticker_Hairstyles/PTS_" + index + "_Back";
                    Debug.Log(filenamHairBack);
                    Sprite hairback = Resources.Load<Sprite>(filenamHairBack);
                    hairBackImage.sprite = hairback;

                    HairName = "Hair" + index;
                    Debug.Log(HairName);


                    string filenamHairFront = "Hair/Pin the sticker_Hairstyles/PTS_" + index + "_Front";
                    Sprite hairfront = Resources.Load<Sprite>(filenamHairFront);
                    hairFrontImage.sprite = hairfront;

                    hairBackImage.enabled = true;
                    hairFrontImage.enabled = true;
                }
                else
                {
                panelPurchaseText.text = "Do you want to buy this item?";
                PurchasePanel.transform.GetComponent<Popup>().Toggle();
                PurchasePanel.SetActive(true);
            }
            

        }


        else if (capImage.enabled == true)
        {
            hairBackImage.enabled = false;
            hairFrontImage.enabled = false;
            string index = "0";
            if (indexSelected < 10)
                index = "0" + indexSelected;
            else
                index = "" + indexSelected;

            if (itemList[indexSelected - 1].unlockflag == 1)
            {
                HairName = "Hair" + index;
                Debug.Log(HairName);
                string HairName2 = "Hair" + index;
                Debug.Log(namecap);
                namecap = "" + namecap;


                //file name=PS_Gamescreen_Cap01_Hair01;
                string filenamHairBack = "Hair/Pin the sticker_Hairstyles/PTS_" + index + "_Back";
                Debug.Log(filenamHairBack);
                Sprite hairback = Resources.Load<Sprite>(filenamHairBack);
                hairBackImage.sprite = hairback;
                string filename = "Caps/PS_Gamescreen_Caps/PS_Gamescreen_" + namecap + "_" + HairName2;
                Debug.Log(filename);
                Sprite sprite = Resources.Load<Sprite>(filename);
                capImage.sprite = sprite;
                hairBackImage.enabled = true;
                capImage.enabled = true;
            }
            else
            {
                panelPurchaseText.text = "Do you want to buy this item?";
                PurchasePanel.transform.GetComponent<Popup>().Toggle();
                PurchasePanel.SetActive(true);
            }



        }
    }




    void generateEyes(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;
        
        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Eyes/PS_Gamescreen_Eyes/PS_Gamescreen_Eye_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onEyesLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickEyesFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);


    }
    void onEyesLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }




    void onClickEyesFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Eyes/PintheSticker_Eyes/PS_Gamescreen_Eye_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            eyeImage.sprite = sprite;
            eyeImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }



    void generateLips(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Lips/PS_Gamescreen_Lips/PS_Gamescreen_Lip_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onLipsLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickLipsFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onLipsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickLipsFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {
            string filename = "Lips/Pin the sticker_Lips/PS_Gamescreen_Lip_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            lipImage.sprite = sprite;

            lipImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }



    void generateNoses(int itemNumber)
    {
        clicksound();

        scrollView2.verticalNormalizedPosition = 1;



        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        //PS_Gamescreen_Nose_Brown_Gamescreen_01
        //PS_Gamescreen_Nose_DarkBrown_Gamescreen_01
        //PS_Gamescreen_Nose_LightBrown_Gamescreen_01
        //PS_Gamescreen_Nose_MediumBrown_Gamescreen_01
        //PS_Gamescreen_Nose_Yellow_Gamescreen_01
        string filename = "Nose/Pin the Sticker_Nose_All_InFrames/PS_Gamescreen_Nose_" + selectedSkinColor + "_Gamescreen_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onNoseLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickNosesFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onNoseLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickNosesFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            // string filename = "Eyes/PintheSticker_Eyes/PS_Gamescreen_Eye_" + index;
            string filename = "Nose/Pin the Sticker_Nose_ALL/PS_Gamescreen_Nose_" + selectedSkinColor + "_" + (index);

            Sprite sprite = Resources.Load<Sprite>(filename);
            noseImage.sprite = sprite;

            noseImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }







    void generateEars(int itemNumber)
    {
        clicksound();

        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;


        string filename = "Ear/Pin the Sticker_Ears_All_InFrames/PS_Gamescreen_Ears_" + selectedSkinColor + "_Gamescreen_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onEarsLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickEarsFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);



    }
    void onEarsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickEarsFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            // string filename = "Eyes/PintheSticker_Eyes/PS_Gamescreen_Eye_" + index; 
            string filename = "Ear/Pin_the_Sticker_Ears_ALL/PS_Gamescreen_Ears_" + selectedShape + "_" + selectedSkinColor + "_" + (index);
            Debug.Log(filename);
            Sprite sprite = Resources.Load<Sprite>(filename);
            earImage.sprite = sprite;
            earImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }





    void generateHairBands(int itemNumber)
    {
        clicksound();

        scrollView2.verticalNormalizedPosition = 1;

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "HairBands/PS_Gamescreen_Hairbands -InFrames/PS_Gamescreen_Hairband_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onhairBandLockClickFunction(itemNumber));


        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickHairBandsFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onhairBandLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickHairBandsFunction(int indexSelected)
    {
        clicksound();

        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            if (HairName == "")
            {


                //file name=PS_Gamescreen_Cap01_Hair01;
                string filenamHairBack = "Hair/Pin the sticker_Hairstyles/PTS_01_Back";
                Debug.Log(filenamHairBack);
                Sprite hairback = Resources.Load<Sprite>(filenamHairBack);
                hairBackImage.sprite = hairback;
                hairBackImage.enabled = true;

                string filenamHairFront = "Hair/Pin the sticker_Hairstyles/PTS_01_Front";
                Debug.Log(filenamHairFront);
                Sprite hairfront = Resources.Load<Sprite>(filenamHairFront);
                hairFrontImage.sprite = hairfront;
                hairFrontImage.enabled = true;
            }



            string filename = "HairBands/PS_Gamescreen_Hairbands/PS_Gamescreen_Hairband_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            haribandImage.sprite = sprite;
            haribandImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }






    void generateCaps(int itemNumber)
    {
        clicksound();

        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Caps/PS_Gamescreen_Caps -InFrames/PS_Gamescreen_Cap_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename); file name=PS_Gamescreen_Cap_01,;

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onCapsLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickCapsFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onCapsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }



    void onClickCapsFunction(int indexSelected)
    {
        clicksound();

        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        namecap = "Cap" + index;
        Debug.Log(namecap);
        HairName1 = "" + HairName;
        Debug.Log(HairName1);
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            if (HairName == "")
            {

                HairName = "Hair01";

                //file name=PS_Gamescreen_Cap01_Hair01;
                string filenamHairBack = "Hair/Pin the sticker_Hairstyles/PTS_01_Back";
                Debug.Log(filenamHairBack);
                Sprite hairback = Resources.Load<Sprite>(filenamHairBack);
                hairBackImage.sprite = hairback;
                hairBackImage.enabled = true;
            }



            string filename = "Caps/PS_Gamescreen_Caps/PS_Gamescreen_" + namecap + "_" + HairName;
            Debug.Log(filename);
            Sprite sprite = Resources.Load<Sprite>(filename);
            capImage.sprite = sprite;
            capImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }



    void generateDress(int itemNumber)
    {
        clicksound();

        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Dress/PS_Gamescreen_Dress_All_InFrames/PS_Gamescreen_Dress_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onDressLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickDressFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onDressLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickDressFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Dress/PS_Gamescreen_Dress/PS_Gamescreen_Dress_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            dressImage.sprite = sprite;
            dressImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }




    void generateTiara(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Tiara/PintheSticker_Tiara_InFrames/PS_Gamescreen_Tiara_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onTiaraLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickTiaraFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onTiaraLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickTiaraFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;

        if (itemList[indexSelected - 1].unlockflag == 1)
        {
           
            if (HairName == "")
            {
                //file name=PS_Gamescreen_Cap01_Tiara01;
                string filenamHairBack = "Hair/Pin the sticker_Hairstyles/PTS_01_Back";
                Debug.Log(filenamHairBack);
                Sprite hairback = Resources.Load<Sprite>(filenamHairBack);
                hairBackImage.sprite = hairback;
                hairBackImage.enabled = true;

                string filenamHairFront = "Hair/Pin the sticker_Hairstyles/PTS_01_Front";
                Debug.Log(filenamHairFront);
                Sprite hairfront = Resources.Load<Sprite>(filenamHairFront);
                hairFrontImage.sprite = hairfront;
                hairFrontImage.enabled = true;
            }


            string filename = "Tiara/PintheSticker_Tiara/PS_Gamescreen_Tiara_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            tiaraImage.sprite = sprite;
            tiaraImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }



    void generateGoggle(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Goggles/PintheSticker_Goggles_Inframes/PS_Gamescreen_Goggles_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onGogglesLockClickFunction(itemNumber));


        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickGoggleFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onGogglesLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickGoggleFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Goggles/PintheSticker_Goggles/PS_Gamescreen_Goggles_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            goggleImage.sprite = sprite;
            goggleImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }


    void generateMouchstache(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Mouchstache/PintheSticker_Mouchstache_Inframes/PS_Gamescreen_Mouchstache_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onMouchstacheLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickMouchstacheFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
        // scrollItemOb.transform.Find("Game Icon").position =  new Vector3(2.0f*itemNumber, 0.0f,0.0f);  
        //scrollItemOb.transform.position.Set(1.0f*itemNumber, 1.0f * itemNumber, 1.0f);


        // scrollItemOb.transform.SetPositionAndRotation(spawnPosition,spawnRotation);
        // scrollItemOb.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(1f*itemNumber, 10f);
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onMouchstacheLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickMouchstacheFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Mouchstache/PintheSticker_Mouchstache/PS_Gamescreen_Mouchstache_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            mouchstacheImage.sprite = sprite;
            mouchstacheImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }



    void generateBeards(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "Beards/PintheSticker_Beards_InFrames/PS_Gamescreen_Beard_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onBeardsLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickBeardsFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
       
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onBeardsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickBeardsFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Beards/PintheSticker_Beards/PS_Gamescreen_Beard_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            beardImage.sprite = sprite;
            beardImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }





    void generateMirrorFrame(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;


        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;
        string filename = "MirrorFrames/PintheSticker_MirrorFrames_InFrames/PS_Gamescreen_MirrorFrames_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        // Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onMirrorFrameLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickMirrorFrameFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;
       
        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onMirrorFrameLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickMirrorFrameFunction(int indexSelected)
    {
        clicksound();
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "MirrorFrames/PintheSticker_MirrorFrames/PS_Gamescreen_MirrorFrames_" + index;
            Sprite sprite = Resources.Load<Sprite>(filename);
            mirrorframeImage.sprite = sprite;
            mirrorframeImage.enabled = true;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }




    void generateAnimals(int itemNumber)
    {

        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;



        string filename = "Animal Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onAnimalLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickAnimalFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);
        

    }
    
    void onAnimalLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }



    void onClickAnimalFunction(int indexSelected)
    {
        clicksound();
        Stickers.SetActive(true);

        float x;
        float y;

        Vector3 pos;

        string index = null;
        if (indexSelected > 0)

            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Animal Stickers/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            //GameObject.Find("imageView").GetComponent<Image>().sprite = sprite;


            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;
            //scrollItemOb1.gameObject.GetComponent<Button>().image.sprite = sprite1;

            //x = Random.Range(800, 1100);
            ////y = Random.Range(400, 700);
            x = 0;
            y = 0;
            float z = 0;
            pos = new Vector3(x, y,z);
            scrollItemOb1.transform.position = pos;
            // scrollItemOb1.gameObject.GetComponent<Button>().onClick.AddListener(() => onClickAnimalStickers(indexSelected));
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }



    void generateBirds(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;


        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;

        string filename = "Birds Sticker/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onBirdLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickBirdFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onBirdLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickBirdFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;

        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Birds Sticker/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            print(pos.x) ;
            print(pos.y) ;
            print(pos.z) ;
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }
    void generateInsects(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Insects stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onInsectLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickInsectFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onInsectLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickInsectFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;

        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {


            string filename = "Insects stickers/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }



    void generateFishes(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Fishes Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onFishesLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickFishFunction(itemNumber));

        //scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onFishesLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickFishFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;

        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Fishes Stickers/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }

    void generateGirlsCharacter(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;
        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Girls Character Sticker/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;



        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onGirlsCharactersLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickGirlsCharacterFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onGirlsCharactersLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickGirlsCharacterFunction(int indexSelected)
    {
        clicksound();

        float x;
        float y;

        Vector3 pos;
        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Girls Character Sticker/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }

    void generateMovieCharacters(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Movie Characters/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onMovieCharactersLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickMovieCharactersFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onMovieCharactersLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickMovieCharactersFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;


        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Movie Characters/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }

    void generateFunnyFaces(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Funny Faces Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onFunnyFacesLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickFunnyFacesStickersFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onFunnyFacesLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickFunnyFacesStickersFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;

        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Funny Faces Stickers/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }


    void generateEmotionsStickes(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Emotions Sticker/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onEmotionsLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickEmotionsStickerFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onEmotionsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickEmotionsStickerFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;


        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Emotions Sticker/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }


    void generateTextStickers(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Text Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onTextStickersLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickTextStickersFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onTextStickersLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }


    void onClickTextStickersFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;

        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Text Stickers/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;

            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }

    void generateMixedStickers(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;



        string index = null;
        if (itemNumber > 0)
            index = "" + itemNumber;


        string filename = "Mixed Stickers/" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);

        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onMixedStickersLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickMixedStickersFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onMixedStickersLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickMixedStickersFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;
        string index = null;
        if (indexSelected > 0)
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "Mixed Stickers/" + (index);
            Sprite sprite1 = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = sprite1;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }

    void generateHeartsStickers(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "HEARTS/PS_Gamescreen_Hearts_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onHeartsLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickHeartsFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }

    void onHeartsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }



    void onClickHeartsFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "HEARTS/PS_Gamescreen_Hearts_" + (index);
            Sprite newSprite = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = newSprite;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }

    }


    void generateStarsStickers(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "STARS/PS_Gamescreen_Stars_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;

        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onStarsLockClickFunction(itemNumber));


        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickStarsFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onStarsLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickStarsFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {


            string filename = "STARS/PS_Gamescreen_Stars_" + (index);
            Sprite newSprite = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = newSprite;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }

    void generateTatooesStickers(int itemNumber)
    {
        clicksound();
        scrollView2.verticalNormalizedPosition = 1;

        string index = "0";
        if (itemNumber < 10)
            index = "0" + itemNumber;
        else
            index = "" + itemNumber;

        string filename = "TATOOES/PS_Gamescreen_Tatooes_" + (index);
        Sprite newSprite = Resources.Load<Sprite>(filename);
        Debug.Log(filename);

        Vector3 spawnPosition = new Vector3(itemNumber * 30f, 0f, 0f);
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        GameObject scrollItemOb = Instantiate(scrollItemPrefab2, spawnPosition, spawnRotation);
        scrollItemOb.transform.SetParent(scrollContent2.transform, false);


        if (itemList[itemNumber - 1].unlockflag == 1)
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = false;
        else
            scrollItemOb.transform.Find("Image Lock").GetComponent<Image>().enabled = true;


        scrollItemOb.transform.Find("Image Lock").gameObject.GetComponent<Button>().onClick.AddListener(() => onTatooesLockClickFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickTatooesFunction(itemNumber));

        scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().name = filename;

        scrollItemOb.gameObject.GetComponent<RectTransform>().position = new Vector3(1f * itemNumber, 10f, 10f);

    }
    void onTatooesLockClickFunction(int indexSelected)
    {
        panelPurchaseText.text = "Do you want to buy this item?";
        PurchasePanel.transform.GetComponent<Popup>().Toggle();
        PurchasePanel.SetActive(true);
    }

    void onClickTatooesFunction(int indexSelected)
    {
        clicksound();
        float x;
        float y;

        Vector3 pos;
        string index = "0";
        if (indexSelected < 10)
            index = "0" + indexSelected;
        else
            index = "" + indexSelected;
        if (itemList[indexSelected - 1].unlockflag == 1)
        {

            string filename = "TATOOES/PS_Gamescreen_Tatooes_" + (index);
            Sprite newSprite = Resources.Load<Sprite>(filename);
            GameObject scrollItemOb1 = Instantiate(Stickers);
            scrollItemOb1.transform.SetParent(StickerObject.transform, false);
            scrollItemOb1.gameObject.GetComponent<Image>().sprite = newSprite;


            //x = Random.Range(800, 1100);
            //y = Random.Range(400, 700);
            x = y = 0;
            pos = new Vector3(x, y);
            scrollItemOb1.transform.position = pos;
        }
        else
        {
            panelPurchaseText.text = "Do you want to buy this item?";
            PurchasePanel.transform.GetComponent<Popup>().Toggle();
            PurchasePanel.SetActive(true);
        }


    }


    public void clicksound()
    {
        int soundFlag = PlayerPrefs.GetInt("sound");                           //Music and sound

        if (soundFlag == 0)
        {
            AudioManager.Instance.PlaySound(buttonSound1);
        }


    }





}
