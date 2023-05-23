using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NativeShareScript : MonoBehaviour
{
    public GameObject mirrorImage;
    public GameObject mirrorImageIPhone;
    public GameObject mirrorImageIPad;
    public GameObject CanvasShareObj;
    public GameObject CanvasShareObjIPhone;
    public GameObject CanvasShareObjIPad;
    public GameObject leftPanel;
    public GameObject leftPanelIPhone;
    public GameObject leftPanelIPad;
    public GameObject rightPanel;
    public GameObject rightPanelIPhone;
    public GameObject rightPanelIPad;
    private bool isProcessing = false;
    private bool isFocus = false;
    public Image Frame;
    public Image FrameIPhone;
    public Image FrameIPad;
    public Image sizeImage;
    public Image sizeIphoneImage;
    public Image sizeIpadImage;
    public GameObject BGImage;
    public GameObject BGImageIPad;
    public GameObject BGImageIPhone;
    public GameObject loadingScreen;
    public AudioClip buttonSound1;

    private UIManager uiManager;
    private float frameImageWidth = 0, frameImageHeight = 0;
    private float scaleX = 0;

    private void Start()
    {
        uiManager = transform.GetComponent<UIManager>();
        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            //sizeImage = sizeIphoneImage;
            Frame = FrameIPhone;
            leftPanel = leftPanelIPhone;
            rightPanel = rightPanelIPhone;
            CanvasShareObj = CanvasShareObjIPhone;
            BGImage = BGImageIPhone;
            mirrorImage = mirrorImageIPhone;
        }
        else if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            //sizeImage = sizeIpadImage;
            Frame = FrameIPad;
            leftPanel = leftPanelIPad;
            rightPanel = rightPanelIPad;
            CanvasShareObj = CanvasShareObjIPad;
            BGImage = BGImageIPad;
            mirrorImage = mirrorImageIPad;
        }

        frameImageWidth = mirrorImage.GetComponent<RectTransform>().rect.width;
        frameImageHeight = mirrorImage.GetComponent<RectTransform>().rect.height;
        scaleX = GetScale(Screen.width, Screen.height, new Vector2(1920, 1080), 1);
        frameImageWidth = frameImageWidth * scaleX;
        frameImageHeight = frameImageHeight * scaleX;
        
        //iPad
        if ((Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1))))
        {
            frameImageWidth = frameImageWidth - (63 * scaleX * 2);
            frameImageHeight = frameImageHeight - (145f * scaleX * 2);

        }
        else
        {
            frameImageWidth = frameImageWidth - (77.9295f * scaleX * 2);
        }

    }

    private float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
    {
        return Mathf.Pow(width / scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight) *
               Mathf.Pow(height / scalerReferenceResolution.y, scalerMatchWidthOrHeight);
    }

    public void ShareBtnPress()
    {
        if (!isProcessing)
        {
            print("Is Processing false..");
            // CanvasShareObj.SetActive(true);
            //GameObject.Find("Left Panel Canvas").GetComponent<Canvas>().enabled = false;
            //GameObject.Find("Right Panel Canvas").GetComponent<Canvas>().enabled = false;
            leftPanel.SetActive(false);
            rightPanel.SetActive(false);
            Frame.enabled = false;
            BGImage.SetActive(false);
            StartCoroutine(ShareScreenshot());
        }
    }

    public void LinkShareBtnPress()
    {
        if (!isProcessing)
        {
            print("Is Processing false..");
            // CanvasShareObj.SetActive(true);
            //GameObject.Find("Left Panel Canvas").GetComponent<Canvas>().enabled = false;
            //GameObject.Find("Right Panel Canvas").GetComponent<Canvas>().enabled = false;
            StartCoroutine(ShareLink());
        }
    }

    IEnumerator ShareLink()
    {
        clicksound();
        isProcessing = true;
        print("Is processing true...");
        yield return new WaitForEndOfFrame();
        if (!Application.isEditor)
        {
            NativeShare native = new NativeShare();

#if UNITY_ANDROID
            native.SetSubject("https://play.google.com/store/apps/details?id=com.rg.stickergame");
            native.SetText("https://play.google.com/store/apps/details?id=com.rg.stickergame");
#elif UNITY_IOS
                native.SetSubject("https://apps.apple.com/us/app/hi-girls-its-sticker-time/id1547382135");
                native.SetText("https://apps.apple.com/us/app/hi-girls-its-sticker-time/id1547382135");
#endif
            native.Share();
        }
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(false);
        isProcessing = false;
    }

    IEnumerator ShareScreenshot()
    {
        clicksound();
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

        yield return new WaitForEndOfFrame();
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
            /*RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            Camera.main.targetTexture = rt;
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Camera.main.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            Camera.main.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            loadingScreen.SetActive(true);
            byte[] bytes = screenShot.EncodeToJPG();*/

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

            Color[] pix = tex_transparent.GetPixels((int)(Screen.width / 2 - (frameImageWidth) / 2), (int)(Screen.height / 2 - frameImageHeight/2), (int)frameImageWidth, (int)frameImageHeight);
            Texture2D destTex = new Texture2D((int)frameImageWidth, (int)frameImageHeight, TextureFormat.RGBAHalf, false);
            destTex.SetPixels(pix);
            destTex.Apply();

            // Encode the resulting output texture to a byte array then write to the file
            byte[] pngShot = ImageConversion.EncodeToPNG(destTex);
            name = "Screenshot" + timeStamp;
#if UNITY_EDITOR
            path = Application.dataPath + "/Resources/ScreenshotTemp/";


#else
            path = Application.persistentDataPath + "/Resources/ScreenshotTemp/";
#endif
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            System.IO.File.WriteAllBytes(path + name + ".png", pngShot);
            Camera.main.clearFlags = bak_cam_clearFlags;
            Camera.main.targetTexture = bak_cam_targetTexture;
            RenderTexture.active = bak_RenderTexture_active;
            RenderTexture.ReleaseTemporary(render_texture);
            Texture2D.Destroy(tex_transparent);
            Texture2D.Destroy(destTex);
            loadingScreen.SetActive(true);
            string imageName = name + ".png";


            byte[] bytes = transform.GetComponent<NativeShareScript>().LoadImageFromBytes(imageName).EncodeToPNG();
            string filePath = Path.Combine(Application.temporaryCachePath, "PTS.png");
            File.WriteAllBytes(filePath, bytes);
            native.AddFile(filePath);
            native.Share();
            //yield return new WaitForSecondsRealtime(1);
        }
        leftPanel.SetActive(true);
        rightPanel.SetActive(true);
        Frame.enabled = true;
        BGImage.SetActive(true);

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
    }
    public void SaveImageAsBytes(Sprite tempSprite, string imageName)
    {
        byte[] bytes = tempSprite.texture.EncodeToPNG();
#if UNITY_EDITOR
        System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/ScreenshotTemp/" + imageName + ".png", bytes);
#else
        System.IO.File.WriteAllBytes(Application.persistentDataPath+"/Resources/ScreenshotTemp/" + imageName + ".png", bytes);
#endif
    }

    public Texture2D LoadImageFromBytes(string imageName)
    {
        byte[] bytes;
#if UNITY_EDITOR
        bytes = System.IO.File.ReadAllBytes(Application.dataPath + "/Resources/ScreenshotTemp/" + imageName);
#else
        bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Resources/ScreenshotTemp/" + imageName);

#endif
        Texture2D loadTexture = new Texture2D(2, 2, TextureFormat.RGBAHalf, false);

        loadTexture.LoadImage(bytes);
        return loadTexture;
       // Sprite tempSprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height),new Vector2(0.5f, 0.5f), 1);
        //return tempSprite;
    }
    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
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

