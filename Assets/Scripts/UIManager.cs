using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform canvas;
    public GameObject homeScreen;
    public GameObject chooseFaceScreen;
    public GameObject chooseMusicScreen;
    public GameObject chooseMusicScreenIPhone;
    public GameObject gameScreen;
    public GameObject gameScreenIPhone;
    public GameObject gameScreenIPad;
    public GameObject galleryScreen;
    public GameObject shopScreen;
    public GameObject shopScreenIpad;
    public GameObject spinWheelScreen;
    public GameObject freeSpinWheelScreen;
    public GameObject paidSpinWheelScreen;
    public GameObject viewScreen;
    public GameObject galleryScreenContent;
    public AudioClip deleteSound;
    private float screenWidth;
    private void Start()
    {
        screenWidth = canvas.rect.width;
        if (Mathf.Approximately(2.2f, (float)System.Math.Round((double)(this.canvas.rect.width / this.canvas.rect.height), 1)))
        {
            gameScreen = gameScreenIPhone;
            chooseMusicScreen = chooseMusicScreenIPhone;
        }
        else if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(this.canvas.rect.width / this.canvas.rect.height), 1)))
        {
            gameScreen = gameScreenIPad;
            shopScreen = shopScreenIpad;
        }
        UpdateInitialPositionsForAllScreens();
        //Set scroll rect grid layout padding for gallery screen content
        galleryScreenContent.GetComponent<GridLayoutGroup>().padding.left = (int)((screenWidth / 2) - (galleryScreenContent.GetComponent<GridLayoutGroup>().cellSize.x / 2));
        galleryScreenContent.GetComponent<GridLayoutGroup>().padding.right = galleryScreenContent.GetComponent<GridLayoutGroup>().padding.left;
    }

    void UpdateInitialPositionsForAllScreens()
    {
        chooseFaceScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        chooseMusicScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        gameScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        galleryScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        shopScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        spinWheelScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        viewScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
        
    }

    public void ChooseHomeScreen(string id)
    {
        StartCoroutine(CallChooseHomeScreen(id));
    }
    IEnumerator CallChooseHomeScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            homeScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            homeScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            homeScreen.SetActive(false);
        }
        else
        {
            homeScreen.SetActive(true);
            homeScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            homeScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/

        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            homeScreen.SetActive(true);
            homeScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms); 
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            homeScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms); 
            yield return new WaitForSeconds(0.7f);
            homeScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            homeScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            homeScreen.SetActive(false);
        }
    }
    public void ChooseFaceScreen(string id)
    {
        StartCoroutine(CallChooseFaceScreen(id));
    }
    IEnumerator CallChooseFaceScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            chooseFaceScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            chooseFaceScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            chooseFaceScreen.SetActive(false);
        }
        else
        {
            chooseFaceScreen.SetActive(true);
            chooseFaceScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            chooseFaceScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/

        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            chooseFaceScreen.SetActive(true);
            chooseFaceScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            chooseFaceScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            chooseFaceScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            chooseFaceScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            chooseFaceScreen.SetActive(false);
        }
        galleryScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0);
    }
    public void ChooseMusicScreen(string id)
    {
        StartCoroutine(CallChooseMusicScreen(id));
    }
    IEnumerator CallChooseMusicScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            chooseMusicScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            chooseMusicScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            chooseMusicScreen.SetActive(false);
        }
        else
        {
            chooseMusicScreen.SetActive(true);
            chooseMusicScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            chooseMusicScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/

        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            chooseMusicScreen.SetActive(true);
            chooseMusicScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            chooseMusicScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            chooseMusicScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            chooseMusicScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            chooseMusicScreen.SetActive(false);
        }
    }
    public void ChooseGameScreen(string id)
    {
        StartCoroutine(CallChooseGameScreen(id));
    }
    IEnumerator CallChooseGameScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            gameScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            gameScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f); 
            gameScreen.SetActive(false);
        }
        else
        {
            gameScreen.SetActive(true);
            gameScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            gameScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/
        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            gameScreen.SetActive(true);
            gameScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            gameScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            gameScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            gameScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            gameScreen.SetActive(false);
        }
    }
    public void ChooseGalleryScreen(string id)
    {
        StartCoroutine(CallChooseGalleryScreen(id));
    }
    IEnumerator CallChooseGalleryScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            galleryScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            galleryScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            galleryScreen.SetActive(false);
        }
        else
        {
            galleryScreen.SetActive(true);
            galleryScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            galleryScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/
        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            galleryScreen.SetActive(true);
            galleryScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            galleryScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            galleryScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            galleryScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            galleryScreen.SetActive(false);
        }
    }
    public void ChooseShopScreen(string id)
    {
        StartCoroutine(CallChooseShopScreen(id));
    }
    IEnumerator CallChooseShopScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            shopScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            shopScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            shopScreen.SetActive(false);
        }
        else
        {
            shopScreen.SetActive(true);
            shopScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            shopScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/
        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            shopScreen.SetActive(true);
            shopScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            shopScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            shopScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            shopScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            shopScreen.SetActive(false);
        }
    }

    public void ChooseSpinWheelScreen(string id)
    {
        StartCoroutine(CallChooseSpinWheelScreen(id));
    }
    IEnumerator CallChooseSpinWheelScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            spinWheelScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            spinWheelScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            spinWheelScreen.SetActive(false);
        }
        else
        {
            spinWheelScreen.SetActive(true);
            spinWheelScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            spinWheelScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/
        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            spinWheelScreen.SetActive(true);
            spinWheelScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            spinWheelScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            spinWheelScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            spinWheelScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            spinWheelScreen.SetActive(false);
        }
    }

    public void ChooseViewScreen(string id)
    {
        StartCoroutine(CallChooseViewScreen(id));
    }
    IEnumerator CallChooseViewScreen(string id)
    {
        /*if ((id != "LeftToCenter") && (id != "RightToCenter"))
        {
            viewScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            viewScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(1f);
            viewScreen.SetActive(false);
        }
        else
        {
            viewScreen.SetActive(true);
            viewScreen.transform.GetComponent<DOTweenAnimation>().DORestartById(id);
            viewScreen.transform.GetComponent<DOTweenAnimation>().DOPlayById(id);
            yield return new WaitForSeconds(0f);
        }*/
        TweenParams tParms = new TweenParams().SetEase(DG.Tweening.Ease.Unset);
        if ((id == "LeftToCenter") || (id == "RightToCenter"))
        {
            viewScreen.SetActive(true);
            viewScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
        }
        else if (id == "CenterToRight")
        {
            viewScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            viewScreen.SetActive(false);
        }
        else if (id == "CenterToLeft")
        {
            viewScreen.transform.GetComponent<RectTransform>().DOAnchorPosX(-screenWidth, 0.7f).SetAs(tParms);
            yield return new WaitForSeconds(0.7f);
            viewScreen.SetActive(false);
        }
    }
}
