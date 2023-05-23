using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ChartboostSDK;
using Platinio;

public class TakeScreenshot : MonoBehaviour
{
    public RectTransform mirrorImage;
    public RectTransform mirrorImageIPhone;
    public RectTransform mirrorImageIPad;
    public AudioClip buttonSound1;
    public AudioClip BgMusic;
    public Image Frame;
    public Image FrameIPhone;
    public Image FrameIPad;
    public GameObject SavePanel;
    public GameObject SavePanelIPhone;
    public GameObject SavePanelIPad;
    public GameObject leftPanel;
    public GameObject leftPanelIPhone;
    public GameObject leftPanelIPad;
    public GameObject rightPanel;
    public GameObject rightPanelIPhone;
    public GameObject rightPanelIPad;
    public GameObject BGImage;
    public GameObject BGImageIPhone;
    public GameObject BGImageIPad;
    private UIManager uiManager;
    public GameObject loadingScreen;

    private float mirrorImageWidth = 0, mirrorImageHeight = 0;
    
    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();
        //IPhone
        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            mirrorImage = mirrorImageIPhone;
            Frame = FrameIPhone;
            SavePanel = SavePanelIPhone;
            leftPanel = leftPanelIPhone;
            rightPanel = rightPanelIPhone;
            BGImage = BGImageIPhone;
        }
        //IPad
        else if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            mirrorImage = mirrorImageIPad;
            Frame = FrameIPad;
            SavePanel = SavePanelIPad;
            leftPanel = leftPanelIPad;
            rightPanel = rightPanelIPad;
            BGImage = BGImageIPad;

        }

        mirrorImageWidth = mirrorImage.rect.width;
        mirrorImageHeight = mirrorImage.rect.height;


        //DatabaseManager.InitializeDB();
        //SavePanel.SetActive(true);
    }

    [SerializeField]
    

    public void TakeAShot()
    {
        clicksound();
        StartCoroutine("CaptureIt");
        //SavePanel.SetActive(true);
        //SavePanel.transform.GetComponent<Popup>().Toggle();
        
        DatabaseManager.SaveinMusicGallery(GameData.SelectedMusicIndex-1);


    }
   
    private IEnumerator CaptureIt()
    {
        //GameObject.Find("Left Panel Canvas").GetComponent<Canvas>().enabled = false;
        //GameObject.Find("Right Panel Canvas").GetComponent<Canvas>().enabled = false;
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);
        Frame.enabled = false;
        BGImage.SetActive(false);
        //SavePanel.SetActive(false);


        // GameObject.Find("Background Canvas").GetComponent<Canvas>().enabled = false;
        /*string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;

        ScreenCapture.CaptureScreenshot(pathToSave);
        Debug.Log(fileName);
        yield return new WaitForEndOfFrame();
        //GameObject.Find("Left Panel Canvas").GetComponent<Canvas>().enabled = true;
        //GameObject.Find("Right Panel Canvas").GetComponent<Canvas>().enabled = true;
        leftPanel.SetActive(true);
        rightPanel.SetActive(true);
        Frame.enabled = true;*/
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        NativeShare native = new NativeShare();
        native.SetSubject("Hi Girls, it's Sticker time!");
        native.SetText("Hi Girls, it's Sticker time!");
        /*RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Texture2D tempTex = new Texture2D((int)Screen.width, (int)Screen.height, TextureFormat.RGB24, false);
        tempTex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        loadingScreen.SetActive(true);
        byte[] bytes = tempTex.EncodeToJPG();*/
        yield return new WaitForEndOfFrame();
        // Depending on your render pipeline, this may not work.
        var bak_cam_targetTexture = Camera.main.targetTexture;
        var bak_cam_clearFlags = Camera.main.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;
        var tex_transparent = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBAHalf, false);

        // Must use 24-bit depth buffer to be able to fill background.
        var render_texture = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.ARGBHalf);

        var grab_area = new Rect(0, 0, Screen.width, Screen.height);

        RenderTexture.active = render_texture;
        Camera.main.targetTexture = render_texture;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;

        // Simple: use a clear background
        Camera.main.backgroundColor = Color.clear;
        Camera.main.backgroundColor = new Color32(255, 51, 156, 0);
        Camera.main.Render();
        tex_transparent.ReadPixels(grab_area, 0, 0);
        tex_transparent.Apply();

        // Encode the resulting output texture to a byte array then write to the file
        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        //System.IO.File.WriteAllBytes(path + name + ".jpg", pngShot);
        //File.WriteAllBytes(savePath, pngShot);

        
        string name = "Screenshot" + timeStamp;
        string path = "";
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/Screenshots/";
#else
        path = Application.persistentDataPath + "/Resources/Screenshots/";
#endif
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        System.IO.File.WriteAllBytes(path + name + ".png", pngShot);
        transform.GetComponent<GalleryButton>().SetImage(pngShot); //Add new screenshot to gallery images list
        transform.GetComponent<GalleryButton>().SetScreenshotName(name + ".png");
        Camera.main.clearFlags = bak_cam_clearFlags;
        Camera.main.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);
        Texture2D.Destroy(tex_transparent);
        loadingScreen.SetActive(true);

        //yield return new WaitForEndOfFrame();
        //GameObject.Find("Left Panel Canvas").GetComponent<Canvas>().enabled = true;
        //GameObject.Find("Right Panel Canvas").GetComponent<Canvas>().enabled = true;
        yield return new WaitForSecondsRealtime(1);
        leftPanel.SetActive(true);
        rightPanel.SetActive(true);
        Frame.enabled = true;
        BGImage.SetActive(true);
        
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(false);
        SavePanel.SetActive(true);
        SavePanel.transform.GetComponent<Popup>().Toggle();
    }


    public void YesButtonClicked()
    {
        
        clicksound();
        if(PlayerPrefs.GetInt("NoAds", 0) == 0)
        {
            //Chartboost.showInterstitial(CBLocation.Default);
            //Chartboost.cacheInterstitial(CBLocation.Default);

            AudioManager.Instance.SetMusicFlag(false);
            AudioManager.Instance.PauseMusic();
            Advertisements.Instance.ShowInterstitial(InterstitialClosed);
        }
        //SceneManager.LoadScene("MyGalleryScene");
        //uiManager.galleryScreen.SetActive(true);
        /*uiManager.ChooseGameScreen("CenterToRight");
        uiManager.ChooseMusicScreen("RightToCenter");
        uiManager.ChooseMusicScreen("CenterToLeft");
        uiManager.ChooseFaceScreen("RightToCenter");
        uiManager.ChooseFaceScreen("CenterToLeft");
        uiManager.ChooseGalleryScreen("LeftToCenter");
        transform.GetComponent<GalleryButton>().SetGalleryDetails();*/
        SavePanel.SetActive(false);
        SavePanel.transform.GetComponent<Popup>().Toggle();
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
        AudioManager.Instance.StopMusic();
        if (PlayerPrefs.GetInt("music") == 0)
        {
            AudioManager.Instance.PlayMusic(BgMusic);
        }
    }

    public void InterstitialClosed()
    {
        AudioManager.Instance.SetMusicFlag(true);
        AudioManager.Instance.UnPauseMusic();
    }

    public void N0ButtonPressed()
    {
        clicksound();
        SavePanel.transform.GetComponent<Popup>().Toggle();
        StartCoroutine(ShowLoadingScreenNOButton());
    }

    IEnumerator ShowLoadingScreenNOButton()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        transform.GetComponent<GalleryButton>().SetGalleryDetails();
        //SavePanel.SetActive(false);
    }


    public void ShareButtonClicked()
    {
        clicksound();
        StartCoroutine("CaptureIt");

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
