using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingScene : MonoBehaviour
{
    public GameObject videoPlayer;
    public GameObject gameSplash;
    public Image loadingBar;
    public RectTransform loadingText;
    AsyncOperation asyncOperation;
    bool isReadyToLoad = false;
    void Awake()
    {
        loadingBar.fillAmount = 0;

        videoPlayer.GetComponent<VideoPlayer>().aspectRatio = VideoAspectRatio.FitOutside;
        videoPlayer.GetComponent<VideoPlayer>().Play();
        videoPlayer.SetActive(true);
        gameSplash.SetActive(false);
        string dbName = "PinTheSticker.db";

        string pathDB = System.IO.Path.Combine(Application.persistentDataPath, dbName);

        string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath, dbName);

        //if DB does not exist in persistent data folder (folder "Documents" on iOS) or source DB is newer then copy it
        if (!System.IO.File.Exists(pathDB) || (System.IO.File.GetLastWriteTimeUtc(sourcePath) > System.IO.File.GetLastWriteTimeUtc(pathDB)))
        {
#if UNITY_EDITOR || UNITY_ANDROID
            if (sourcePath.Contains("://"))
            {
                // Android  
                WWW www = new WWW(sourcePath);

                while (!www.isDone) {; }
                // you may want to put a timer or catch in the above!!
                if (string.IsNullOrEmpty(www.error))
                {
                    System.IO.File.WriteAllBytes(pathDB, www.bytes);

                }
                else
                {
                    // CanExQuery = false;

                }

            }
            else
            {

            }
#else
            
              {
                  // Mac, Windows, Iphone

                  //validate the existens of the DB in the original folder (folder "streamingAssets")
                  if (System.IO.File.Exists(sourcePath))
                  {

                      //copy file - alle systems except Android
                      System.IO.File.Copy(sourcePath, pathDB, true);

                  }
                  else
                  {
                     // CanExQuery = false;
                      Debug.Log("ERROR: the file DB named " + dbName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
                  }

              }
#endif
        }
        
        StartCoroutine(ShowNextScreen());
    }

    IEnumerator ShowNextScreen()
    {

        
        //print("Initializing db");
        yield return new WaitForSeconds((float)videoPlayer.GetComponent<VideoPlayer>().length);

        DatabaseManager.InitializeDB();
        videoPlayer.SetActive(false);
        gameSplash.SetActive(true);
        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(gameSplash.transform.GetComponent<RectTransform>().rect.width / gameSplash.transform.GetComponent<RectTransform>().rect.height), 1)))
        {
           
            loadingText.anchoredPosition = new Vector2(67.605f, 15f);

        }

            yield return new WaitForSeconds(1f);
        StartCoroutine(LoadNextScene());
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadNextScene()
    {
        
        loadingBar.fillAmount = 1;
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadSceneAsync("MainScene");
        asyncOperation.allowSceneActivation = true;
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        asyncOperation = SceneManager.LoadSceneAsync("MainScene");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            loadingBar.fillAmount = asyncOperation.progress;
            //Output the current progress

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                loadingBar.fillAmount = 1;
                isReadyToLoad = true;
            }

            yield return null;
        }
    }
}