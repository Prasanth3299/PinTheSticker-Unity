using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Model;
using DanielLochner.Assets.SimpleScrollSnap;
public class GalleryButton : MonoBehaviour
{
    public AudioClip BgMusic;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    public int i = 1;
    public string filename;
    private int nSelectedImageIndex;
    public AudioClip buttonSound1;
    string[] files = null;
    public Text displayText;
    public Image EmptyGallery;
    float widthValue = 0;
    public AudioClip[] musicarray = new AudioClip[30];
    List<Gallery> musicList = new List<Gallery>();
    private UIManager uiManager;
    public GameObject startAGameButton;
    public GameObject loadingScreen;
    public string fromScreenName = "";

    private List<Sprite> galleryImages = new List<Sprite>();
    private List<string> galleryScreenshotNames = new List<string>();

    private int galleryImageCount = 0;
    void Start()
    {

        widthValue = scrollContent.GetComponent<RectTransform>().rect.width;
        uiManager = transform.GetComponent<UIManager>();

        if((Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1))))
        {
            scrollContent.GetComponent<GridLayoutGroup>().spacing = new Vector2(250, 500);
        }
        else
        {
            scrollContent.GetComponent<GridLayoutGroup>().spacing = new Vector2(500, 500);

        }
        //DatabaseManager.InitializeDB();
        musicList = DatabaseManager.GetGalleryValues();

      
        if (musicList.Count > 0)
        {
            int musicFlag = PlayerPrefs.GetInt("music", 0);                           //Music and sound
            if (musicFlag == 0)
            {
                PlayerPrefs.SetInt("music", 0);
                transform.GetComponent<MusicSound>().MusicButtonOn.image.sprite = transform.GetComponent<MusicSound>().Bg;
                transform.GetComponent<MusicSound>().MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
                transform.GetComponent<MusicSound>().MusicButtonOff.image.sprite = transform.GetComponent<MusicSound>().OffSprite;
                transform.GetComponent<MusicSound>().MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
                
                //AudioManager.Instance.PlayMusic(musicarray[musicList[0].musicindex]);
                AudioManager.Instance.PlayMusic(BgMusic);
                GameData.SelectedGallery = musicList[0];
            }
            else
            {
                

                transform.GetComponent<MusicSound>().MusicButtonOff.image.sprite = transform.GetComponent<MusicSound>().Bg;
                transform.GetComponent<MusicSound>().MusicButtonOff.transform.GetChild(0).GetComponent<Image>().color = new Color32(52, 232, 252, 255);
                transform.GetComponent<MusicSound>().MusicButtonOn.image.sprite = transform.GetComponent<MusicSound>().OnSprite;
                transform.GetComponent<MusicSound>().MusicButtonOn.transform.GetChild(0).GetComponent<Image>().color = new Color32(191, 193, 192, 255);
                //AudioManager.Instance.StopMusic();
            }
        }

        /*files = Directory.GetFiles(Application.dataPath + "/", "*.png");

         // files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
          PlayerPrefs.SetInt("SavedImageCount",files.Length);
          int count = PlayerPrefs.GetInt("SavedImageCount");
          Debug.Log("Count : "+count);



          Debug.Log(files.Length);

          if (files.Length > 0)
          {
              EmptyGallery.enabled = false;
              startAGameButton.SetActive(false);

              GetPictureAndShowIt();

          }
         else
          {
              displayText.text = "Gallery is Empty";
              EmptyGallery.enabled = true;
              //GameObject.Find("Save Button").SetActive(false);
              //GameObject.Find("Share Button").SetActive(false);
              //GameObject.Find("Previous Button").SetActive(false);
              //GameObject.Find("Next Button").SetActive(false);
              AudioManager.Instance.StopMusic();
          }*/
        string path = "";
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/ScreenshotTemp/";


#else
            path = Application.persistentDataPath + "/Resources/ScreenshotTemp/";
#endif
        if (Directory.Exists(path))
        {
            string[] files = Directory.GetDirectories(path);
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] fileInfo = info.GetFiles();
            for (int i = 0; i < fileInfo.Length; i++)
            {
                File.Delete(path + fileInfo[i].Name);
            }
        }
        GetGalleryInfo();
    }

    public void GetGalleryInfo()
    {
        galleryImages.Clear();
        galleryScreenshotNames.Clear();
        string path;
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/Screenshots/";
#else
        path = Application.persistentDataPath + "/Resources/Screenshots/";
#endif
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string[] files = Directory.GetDirectories(path);
        DirectoryInfo info = new DirectoryInfo(path);
        FileInfo[] fileInfo = info.GetFiles();
        //print("Length" + fileInfo.Length);
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Extension == ".png")
            {
                Texture2D texture = LoadImageFromBytes(fileInfo[i].Name);
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));

                galleryImages.Add(sp);
                galleryScreenshotNames.Add(fileInfo[i].Name);
            }
        }
    }

    public void SetImage(byte[] bytes)
    {
        Texture2D loadTexture = new Texture2D(2, 2, TextureFormat.RGBAHalf, false);
        loadTexture.LoadImage(bytes);
        Sprite sp = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height),
                    new Vector2(0.5f, 0.5f));
        galleryImages.Add(sp);
    }

    public Sprite GetImage(int index)
    {
        return galleryImages[index];
    }

    public void DeleteImage(int index)
    {
        galleryImages.RemoveAt(index);
        galleryScreenshotNames.RemoveAt(index);
    }

    public string GetScreenshotName(int index)
    {
        return galleryScreenshotNames[index];
    }

    public void SetScreenshotName(string ssName)
    {
        galleryScreenshotNames.Add(ssName);
    }

    public void SetGalleryDetails()
    {
        StartCoroutine(LoadGalleryInfo());
    }

    IEnumerator LoadGalleryInfo()
    {
        loadingScreen.SetActive(true);
        RectTransform scrollrect = scrollContent.GetComponent<RectTransform>();
        scrollrect.sizeDelta = new Vector2(widthValue, scrollrect.sizeDelta.y);
        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            Destroy(scrollContent.transform.GetChild(i).gameObject);
        }
        //print("Length" + fileInfo.Length);
        galleryImageCount = 0;
        for (i = 0; i < galleryImages.Count; i++)
        {
                galleryImageCount++;
                string filename2 = "New UI/MyGalleryScreens/Pinthesticker_MyGallery_CenterBox_New";
                Sprite newSprite2 = Resources.Load<Sprite>(filename2);
                GameObject scrollItemOb = Instantiate(scrollItemPrefab);
                scrollItemOb.transform.name = "Screenshot_"+i;
                scrollItemOb.transform.SetParent(scrollContent.transform, false);
                scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener
                    (delegate { ImageClickIconFunction(scrollItemOb.transform.name); });
                scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = galleryImages[i];

                scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.type = Image.Type.Simple;
                scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.preserveAspect = true;
                //scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<Image>().sprite = newSprite2;
                //scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<Button>().image.preserveAspect = true;
                print((float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1));
                if (Mathf.Approximately(1.8f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
                {
                    scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.width + 66, scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.height + 66);
                }
                else if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
                {
                    scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.width - 125, scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.height);

                }
                else if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
                {
                    scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.width + 66, scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.height - 20);

                }
                else
                {
                    scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.width + 66, scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<RectTransform>().rect.height + 10);
                }
            
        }
        CheckEmptyGallery(galleryImageCount);
        scrollrect.sizeDelta = new Vector2(((widthValue * 0.5f) * scrollContent.transform.childCount)+ (widthValue * 0.5f), scrollrect.sizeDelta.y);
        //scrollContent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(2);
        loadingScreen.SetActive(false);
    }

    public void CheckEmptyGallery(int galleryImageCount)
    {
        if (galleryImageCount > 0)
        {
            EmptyGallery.enabled = false;
            displayText.text = "";
            startAGameButton.SetActive(false);
        }
        else
        {
            displayText.text = "Gallery is Empty";
            EmptyGallery.enabled = true;
            startAGameButton.SetActive(true);
            //AudioManager.Instance.StopMusic();
        }
    }

    public void SaveImageAsBytes(Sprite tempSprite, string imageName)
    {
        byte[] bytes = tempSprite.texture.EncodeToPNG();
#if UNITY_EDITOR
        System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/Screenshots/" + imageName + ".png", bytes);
#else
        System.IO.File.WriteAllBytes(Application.persistentDataPath+"/Resources/Screenshots/" + imageName + ".png", bytes);
#endif
    }

    public Texture2D LoadImageFromBytes(string imageName)
    {
        byte[] bytes;
#if UNITY_EDITOR
        bytes = System.IO.File.ReadAllBytes(Application.dataPath + "/Resources/Screenshots/" + imageName);
#else
        bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Resources/Screenshots/"+ imageName);
#endif
        Texture2D loadTexture = new Texture2D(2, 2, TextureFormat.RGBAHalf, false);
        loadTexture.LoadImage(bytes);
        return loadTexture;
        // Sprite tempSprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height),new Vector2(0.5f, 0.5f), 1);
        //return tempSprite;
    }

    void GetPictureAndShowIt()
    {
        for (i = 0; i < files.Length; i++)

        {
            string pathToFile = files[i];

            Texture2D texture = GetScreenshotImage(pathToFile);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));


            string filename2 = "New UI/MyGalleryScreens/Pinthesticker_MyGallery_CenterBox_New";
            Sprite newSprite2 = Resources.Load<Sprite>(filename2);
            Debug.Log("filename2 : "+filename2);


            GameObject scrollItemOb = Instantiate(scrollItemPrefab);
            scrollItemOb.transform.SetParent(scrollContent.transform, false);
           //scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().onClick.AddListener(() =>ImageClickIconFunction());
            scrollItemOb.transform.Find("Game Icon").gameObject.GetComponent<Button>().image.sprite = sp;
            scrollItemOb.transform.Find("Game Icon border").gameObject.GetComponent<Image>().sprite = newSprite2;


        }
    }


    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGBAHalf, false);

            texture.LoadImage(fileBytes);
        }
        return texture;
    }

   public void ImageClickIconFunction(string name)
    {
        loadingScreen.SetActive(true);
        clicksound();
        GameData.SelectedImageIndex = this.nSelectedImageIndex;
        Debug.Log("GameData.SelectedImageIndex " + GameData.SelectedImageIndex);
        //SceneManager.LoadScene("ViewScene");
        //uiManager.viewScreen.SetActive(true);
        //uiManager.galleryScreen.SetActive(false);
        uiManager.ChooseGalleryScreen("CenterToRight");
        uiManager.ChooseViewScreen("LeftToCenter");
        StartCoroutine(ShowImage(name));
    }

    IEnumerator ShowImage(string name)
    {
        transform.GetComponent<ViewScene>().CurrentObjectName = name;
        transform.GetComponent<ViewScene>().SetUpImageToView();
        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            Destroy(scrollContent.transform.GetChild(i).gameObject);
        }
        yield return new WaitForSeconds(2);
        loadingScreen.SetActive(false);
    }
   
    public void NextButton()

    {
        int galleryindex = 0;
        clicksound();
        int count = PlayerPrefs.GetInt("SavedImageCount");
        //Debug.Log(count);

        this.nSelectedImageIndex = nSelectedImageIndex + 1;
        galleryindex = nSelectedImageIndex;
        if (nSelectedImageIndex >= count)
        {
            this.nSelectedImageIndex = 0;
            galleryindex = 0;
        }
        Debug.Log(this.nSelectedImageIndex);

        //AudioManager.Instance.StopMusic();
        //AudioManager.Instance.PlayMusic(musicarray[musicList[galleryindex].musicindex]);
        // GameData.SelectedGallery = musicList[galleryindex];
        /*float width = scrollContent.GetComponent<RectTransform>().sizeDelta.x;
        width = width / 2;
        width = width / scrollContent.transform.childCount;
        scrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            scrollContent.GetComponent<RectTransform>().anchoredPosition.x + width,
            scrollContent.GetComponent<RectTransform>().anchoredPosition.y);*/

    }

    public void PreviousButton()

    {
        int galleryindex = 0;
        clicksound();
       int count = PlayerPrefs.GetInt("SavedImageCount");
        if (nSelectedImageIndex == 0)
        {
            this.nSelectedImageIndex = count;
           
        }
        this.nSelectedImageIndex = nSelectedImageIndex - 1;


        galleryindex = nSelectedImageIndex;
        Debug.Log(nSelectedImageIndex);

        //AudioManager.Instance.StopMusic();
        // AudioManager.Instance.PlayMusic(musicarray[musicList[galleryindex].musicindex]);
        //GameData.SelectedGallery = musicList[galleryindex];
        /*float width = scrollContent.GetComponent<RectTransform>().sizeDelta.x;
        width = width / 2;
        width = width / scrollContent.transform.childCount;
        scrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            scrollContent.GetComponent<RectTransform>().anchoredPosition.x-width,
            scrollContent.GetComponent<RectTransform>().anchoredPosition.y);*/

    }
    public void BackButtonPressed()
    {
        //AudioManager.Instance.StopMusic();
        clicksound();
        Debug.Log("Backbuttonpressed");
        //SceneManager.LoadScene("HomeScene");
        
        if (fromScreenName == "")
        {
            //uiManager.homeScreen.SetActive(true);
            //uiManager.galleryScreen.SetActive(false);
            uiManager.ChooseHomeScreen("RightToCenter");
            uiManager.ChooseGalleryScreen("CenterToLeft");
        }
        else if (fromScreenName == "game")
        {
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
                    AudioManager.Instance.PlayMusic(musicarray[0]);
                }
            }
            uiManager.ChooseGameScreen("RightToCenter");
            uiManager.ChooseGalleryScreen("CenterToLeft");
        }
        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            Destroy(scrollContent.transform.GetChild(i).gameObject);
        }
    }
    public void BeginAGameButtonPressed()
    {
        
        clicksound();
        Debug.Log("BeginAGameButtonPressed");
        //SceneManager.LoadScene("ChooseFaceScene");s
        //uiManager.chooseFaceScreen.SetActive(true);
        uiManager.ChooseFaceScreen("LeftToCenter");
        //uiManager.galleryScreen.SetActive(false);
        uiManager.ChooseGalleryScreen("CenterToRight");
        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            Destroy(scrollContent.transform.GetChild(i).gameObject);
        }
    }

    public void DisableStartAGameButton()
    {
        startAGameButton.SetActive(false);
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





