using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Model;
using Platinio;

public class ViewScene : MonoBehaviour
{
    public GameObject mirrorImage;
    public GameObject mirrorImageIPhone;
    public GameObject mirrorImageIPad;
    public GameObject[] buttonObjects;
    public AudioClip buttonSound1;
    public GameObject SavePanel;
    public GameObject DeletePanel;
    public Image photoImage;
    public Image frameImage;
    public Image frameImageIphone;
    public Image frameImageIpad;
    public Image sizeImage;
    public Image sizeIphoneImage;
    public Image sizeIpadImage;
    string[] files = null;
    public GameObject CanvasShareObj;

    private bool isProcessing = false;
    private bool isFocus = false;

    public string CurrentObjectName = "";
    private UIManager uiManager;
    public GameObject loadingScreen;

    private float frameImageWidth = 0;
    private float frameImageHeight = 0;
    private float scaleX = 0;

    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();

        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            frameImage = frameImageIphone;
            //sizeImage = sizeIphoneImage;
            mirrorImage = mirrorImageIPhone;
        }

        else if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            frameImage = frameImageIpad;
            //sizeImage = sizeIpadImage;
            mirrorImage = mirrorImageIPad;
        }

        frameImageWidth = mirrorImage.GetComponent<RectTransform>().rect.width;
        frameImageHeight = mirrorImage.GetComponent<RectTransform>().rect.height;
        scaleX = GetScale(Screen.width, Screen.height, new Vector2(1920, 1080), 1);
        frameImageWidth = frameImageWidth * scaleX;
        frameImageHeight = frameImageHeight * scaleX;

        if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            frameImageWidth = frameImageWidth - (63 * scaleX * 2);
            frameImageHeight = frameImageHeight - (145f * scaleX * 2);
        }
        else
        {
            frameImageWidth = frameImageWidth - (77.9295f * scaleX * 2);
        }

        //Delete any images present in gallery if app is opened for the first time after (re)installation
        if (PlayerPrefs.GetInt("executeOnce", 0) == 0)
        {
            StartCoroutine(DeleteImageExecuteOnce());
            PlayerPrefs.SetInt("executeOnce", 1);
        }
        //DatabaseManager.InitializeDB();
        //files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");            //Mobile
        // files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        /*files = Directory.GetFiles(Application.dataPath + "/", "*.png");                       //System
        string pathToFile = files[GameData.SelectedImageIndex];
            Debug.Log(GameData.SelectedImageIndex);
            Debug.Log(pathToFile);
       
        Texture2D texture = GetScreenshotImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        photoImage.sprite = sp;*/
        // SavePanel.SetActive(false);


    }

    private float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
    {
        return Mathf.Pow(width / scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight) *
               Mathf.Pow(height / scalerReferenceResolution.y, scalerMatchWidthOrHeight);
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

    public void SetUpImageToView()
    {
        string imageName = CurrentObjectName;
        print(CurrentObjectName);

        Sprite sp = transform.GetComponent<GalleryButton>().GetImage(int.Parse(CurrentObjectName.Split('_')[1]));
        photoImage.GetComponent<RectTransform>().sizeDelta = new Vector3(1362, 1074, 0);
        photoImage.preserveAspect = true;
        photoImage.sprite = sp;
    }

    public void ExitButtonPressed()
    {
        //DeletePanel.GetComponent<Popup>().Toggle();
        
        //clicksound();
        StartCoroutine(ShowLoadingScreen());
        Debug.Log("Exitbuttonpressed");
        //SceneManager.LoadScene("MyGalleryScene");
        //uiManager.galleryScreen.SetActive(true);
        //uiManager.viewScreen.SetActive(false);
    }

    IEnumerator ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        transform.GetComponent<GalleryButton>().SetGalleryDetails();
        if (transform.GetComponent<GalleryButton>().fromScreenName == "game")
        {
            transform.GetComponent<GalleryButton>().DisableStartAGameButton();
        }

        uiManager.ChooseGalleryScreen("RightToCenter");
        uiManager.ChooseViewScreen("CenterToLeft");
    }

    public void Delete()
    {
        clicksound();
        // files = Directory.GetFiles(Application.dataPath + "/", "*.png");
        //files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        Debug.Log(GameData.SelectedImageIndex);
        //string pathToFile = files[GameData.SelectedImageIndex];
        //SceneManager.LoadScene("MyGalleryScene");
        //uiManager.galleryScreen.SetActive(true);
        //uiManager.viewScreen.SetActive(false);
        uiManager.ChooseGalleryScreen("RightToCenter");
        uiManager.ChooseViewScreen("CenterToLeft");
        //File.Delete(pathToFile);
        
        Debug.Log("Deletebuttonpressed");

        //AudioManager.Instance.StopMusic();

       // DatabaseManager.DeleteFromMusicGallery(GameData.SelectedGallery.galleryid);

    }

    public void OkButtonPressed()
    {
        clicksound();
        //  files = Directory.GetFiles(Application.dataPath + "/", "*.png");
        /*files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");

         string pathToFile = files[GameData.SelectedImageIndex];*/
        StartCoroutine(DeleteImage());
    }

    IEnumerator DeleteImageExecuteOnce()
    {
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
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Extension == ".png")
            {
                if (File.Exists(path + fileInfo[i].Name))
                {
                    File.Delete(path + fileInfo[i].Name);
                }
            }
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator DeleteImage()
    {
        loadingScreen.SetActive(true);
        string path;
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/Screenshots/";
#else
        path = Application.persistentDataPath + "/Resources/Screenshots/";
#endif
        string ssName = transform.GetComponent<GalleryButton>().GetScreenshotName(int.Parse(CurrentObjectName.Split('_')[1]));
        if (File.Exists(path + ssName))
        {
            File.Delete(path + ssName);
        }
        transform.GetComponent<GalleryButton>().DeleteImage(int.Parse(CurrentObjectName.Split('_')[1]));
        yield return new WaitForSeconds(1);
        transform.GetComponent<GalleryButton>().SetGalleryDetails();
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(false);
        ExitButtonPressed();
    }



    public void SaveButtonPressed()
    {
        clicksound();
        //SavePanel.SetActive(true);
    }
    public void CloseButtonPressed()
    {
        clicksound();
       // SavePanel.SetActive(false);
    }
    public void DeleteButtonPressed()
    {
        clicksound();
        DeletePanel.SetActive(true);
        DeletePanel.GetComponent<Popup>().Toggle();
    }
   
    public void CancelButtonPressed()
    {
        clicksound();
        
        DeletePanel.GetComponent<Popup>().Toggle();
        DeletePanel.SetActive(false);
    }

    void Update()
    {
        
    }
    public void ShareBtnPress()
    {
        /*if (!isProcessing)
        {
            CanvasShareObj.SetActive(true);
            StartCoroutine(ShareScreenshot());
        }*/
        clicksound();
        print(" buttonObjects.Length :" + buttonObjects.Length);
        for(int i=0; i < buttonObjects.Length; i++)
        {
            buttonObjects[i].SetActive(false);
        }
        StartCoroutine(ShareScreenshotGalleryImage(CurrentObjectName));
      
        //transform.GetComponent<NativeShareScript>().ShareBtnPress();
        /*for (int i = 0; i < buttonObjects.Length; i++)
        {
            print("success");
            buttonObjects[i].SetActive(true);
        }*/
    }
    IEnumerator ShareScreenshotGalleryImage(string imageName)
    {
        isProcessing = true;
        print("Is processing true...");
        yield return new WaitForEndOfFrame();


        /*string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot " + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        // ScreenCapture.CaptureScreenshot("screenshot.png", 2);
        string destination = Path.Combine(Application.persistentDataPath, pathToSave);
        Debug.Log(destination);*/



        //GameObject.Find("Left Panel Canvas").GetComponent<Canvas>().enabled = true;
        //GameObject.Find("Right Panel Canvas").GetComponent<Canvas>().enabled = true;
        //leftPanel.SetActive(true);
        //rightPanel.SetActive(true);
        //Frame.enabled = true;

        //yield return new WaitForEndOfFrame();
        string name = "";
        string path = "";

       
        if (!Application.isEditor)
        {
            /*AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                "Share your Creation");
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                intentObject, "My creativity");
            currentActivity.Call("startActivity", chooser);*/
            string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            NativeShare native = new NativeShare();

#if UNITY_ANDROID
            native.SetSubject("https://play.google.com/store/apps/details?id=com.rg.stickergame");
            native.SetText("https://play.google.com/store/apps/details?id=com.rg.stickergame");
#endif

            /* RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
             Camera.main.targetTexture = rt;
             Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
             Camera.main.Render();
             RenderTexture.active = rt;
             screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
             Camera.main.targetTexture = null;
             RenderTexture.active = null; // JC: added to avoid errors
             Destroy(rt);*/

            loadingScreen.SetActive(true);
            //byte[] bytes = screenShot.EncodeToJPG();
            //System.IO.File.WriteAllBytes(path + name + ".jpg", pngShot);
            /*byte[] bytes = transform.GetComponent<NativeShareScript>().LoadImageFromBytes(imageName).EncodeToPNG();*/
            Sprite sp = transform.GetComponent<GalleryButton>().GetImage(int.Parse(CurrentObjectName.Split('_')[1]));
            Texture2D temp = sp.texture;

            Color[] pix = temp.GetPixels((int)(Screen.width / 2 - (frameImageWidth) / 2), (int)(Screen.height / 2 - frameImageHeight / 2), (int)frameImageWidth, (int)frameImageHeight);
            //Color[] pix = temp.GetPixels((int)(Screen.width / 2 - frameImageWidth / 2), cropYValue, (int)frameImageWidth, (int)frameImageHeight);
            Texture2D destTex = new Texture2D((int)frameImageWidth, (int)frameImageHeight, TextureFormat.RGBAHalf, false);
            destTex.SetPixels(pix);
            destTex.Apply();

            // conversion to bytes
            byte[] bytes = destTex.EncodeToPNG();
        string filePath = Path.Combine(Application.temporaryCachePath, "PTS.png");
            File.WriteAllBytes(filePath, bytes);
            native.AddFile(filePath);
            //native.AddFile (temp,  "PTS.png");
            native.Share();
            //yield return new WaitForSecondsRealtime(1);
        }
        //leftPanel.SetActive(true);
        //rightPanel.SetActive(true);
        // Frame.enabled = true;
#if UNITY_ANDROID
        yield return new WaitUntil(() => isFocus);
#endif
        if (File.Exists(path + name + ".png"))
        {
            File.Delete(path + name + ".png");
        }
        transform.GetComponent<GalleryButton>().SetGalleryDetails();
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(false);
        isProcessing = false;
        print("Coroutine Completed..");
        for (int i = 0; i < buttonObjects.Length; i++)
        {
            print("success");
            buttonObjects[i].SetActive(true);
        }
    }

    IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();

        clicksound();
      // files = Directory.GetFiles(Application.dataPath + "/", "*.png");
          files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
       
        string pathToFile = files[GameData.SelectedImageIndex];

        string destination = Path.Combine(Application.persistentDataPath, pathToFile);
        Debug.Log(destination);

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                "Share your craetivity");
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                intentObject, "My craetivity");
            currentActivity.Call("startActivity", chooser);

            yield return new WaitForSecondsRealtime(1);
        }

        yield return new WaitUntil(() => isFocus);

        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
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
