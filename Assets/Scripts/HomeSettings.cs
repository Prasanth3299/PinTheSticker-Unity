using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Platinio;
using UnityEditor;
using Model;

public class HomeSettings : MonoBehaviour
{
    bool isPanelOpen = false;
    public AudioClip buttonSound1;
    public AudioClip UnlockVideoSound;
    public GameObject TutorialPopup;
    public GameObject spinWheelPopUp;
    public GameObject settingsPopUp;
    public GameObject tutorialContent;
    public Text spinWheelInfoText;
    public GameObject tutorialPreviousButton;
    private UIManager uiManager;
    private int count = 0;
    private Animator settingsPopUpAnim, settingsButtonAnimator;
    public GameObject settingButton;
    public Button tutorialsButton, privacyPolicyButton, termsButton;
    public GameObject tutorialPanel, privacyPanel, termsPanel;
    public GameObject tutorialPanelIpad, privacyPanelIpad, termsPanelIpad;

    private List<Shop> shopsList = new List<Shop>();
    private List<Jackpot> payspintList = new List<Jackpot>();
    private List<int> totalList;

    void Start()
    {
       // PlayerPrefs.DeleteAll(); 

        totalList = new List<int>();

        uiManager = transform.GetComponent<UIManager>();
        if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            tutorialsButton.onClick.AddListener(() => StartCoroutine(OnTutorialsButtonIpadClicked()));
            privacyPolicyButton.onClick.AddListener(() => StartCoroutine(OnPrivacyPolicyButtonIpadClicked()));
            termsButton.onClick.AddListener(() => StartCoroutine(OnTermsButtonIpadClicked()));

        }
        else
        {
            tutorialsButton.onClick.AddListener(() => StartCoroutine(OnTutorialsButtonClicked()));
            privacyPolicyButton.onClick.AddListener(() => StartCoroutine(OnPrivacyPolicyButtonClicked()));
            termsButton.onClick.AddListener(() => StartCoroutine(OnTermsButtonClicked()));
        }

        settingsPopUpAnim = settingsPopUp.transform.GetChild(0).GetComponent<Animator>();
        settingsButtonAnimator = settingButton.GetComponent<Animator>();
        //TutorialPopup.SetActive(false);
        shopsList = DatabaseManager.GetShopsValues();

        for (int i = 2; i < shopsList.Count; i++)
        {
            if (shopsList[i].unlockflag == 0)
            {
                totalList.Add(i);
                //unlockShopList.Add(shopsList[i]);
            }
        }

        if (PlayerPrefs.GetString("SystTime").Length == 0)
        {
            //print("Spin wheel");
            if (totalList.Count != 0)
            {
                payspintList = DatabaseManager.GetPaySpinItems();
                if (payspintList.Count != 0)
                {

                    PlayerPrefs.SetInt("ButtonEnabled", 1);
                    spinWheelPopUp.SetActive(true);
                    spinWheelInfoText.text = "Free spin wheel is ready. \nPress ok to go to spin wheel";
                }
                else
                {
                    PlayerPrefs.SetInt("ButtonEnabled", 0);

                }
            }
            else
            {
                PlayerPrefs.SetInt("ButtonEnabled", 0);

            }
        }
        else
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("SystTime"));
            long temp = Convert.ToInt64(tempTime.ToBinary());
            DateTime oldDate = DateTime.FromBinary(temp);
            oldDate = oldDate.AddDays(1);
            TimeSpan timeDifference = oldDate.Subtract(currentDate);
            //print(timeDifference.Hours + " : " + timeDifference.Minutes + " : " + timeDifference.Seconds);
            if ((timeDifference.Seconds > -1 && timeDifference.Hours < 24))
            {
                PlayerPrefs.SetInt("ButtonEnabled", 0);
                spinWheelPopUp.SetActive(false);
            }
            else
            {
                if (totalList.Count != 0)
                {
                    payspintList = DatabaseManager.GetPaySpinItems();
                    if (payspintList.Count != 0)
                    {

                        PlayerPrefs.SetInt("ButtonEnabled", 1);
                        spinWheelPopUp.SetActive(true);
                        spinWheelInfoText.text = "Free spin wheel is ready. \nPress ok to go to spin wheel";
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ButtonEnabled", 0);

                    }
                }
                else
                {
                    PlayerPrefs.SetInt("ButtonEnabled", 0);

                }
            }
            /*if ((System.DateTime.Now - date).Seconds < 40)
            {
                PlayerPrefs.SetInt("ButtonEnabled", 1);
                spinWheelPopUp.SetActive(true);
                spinWheelInfoText.text = "Free spin wheel is ready. \nPress ok to go to spin wheel";
            }
            else
            {
                PlayerPrefs.SetInt("ButtonEnabled", 0);
                spinWheelPopUp.SetActive(false);
            }*/
        }
        Advertisements.Instance.Initialize();
    }
    private void OnEnable()
    {
        //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }

    public void OnOkSpinButtonClicked()
    {
        clicksound();
        spinWheelPopUp.SetActive(false);
        //uiManager.spinWheelScreen.SetActive(true);
        transform.GetComponent<Wheel>().SpinWheelUpdate();
        uiManager.freeSpinWheelScreen.SetActive(true);
        uiManager.paidSpinWheelScreen.SetActive(false);
        //uiManager.homeScreen.SetActive(false);
        uiManager.ChooseHomeScreen("CenterToRight");
        uiManager.ChooseSpinWheelScreen("LeftToCenter");

    }
    public void OnCancelSpinButtonClicked()
    {
        clicksound();
        spinWheelPopUp.SetActive(false);
    }

    public void TutorialButtonPressed()
    {

        OnSettingsButtonClicked();

        clicksound();
        //Debug.Log("TutorialButtonPressed");
        // TutorialPopup.SetActive(true);
        //transform.GetComponent<GearMenu>().SwitchOff();

    }

    public void onTutorialCloseButtonClicked()
    {
        clicksound();
        print(count);
        while (count != 0)
        {
            //tutorialPreviousButton.GetComponent<Button>().onClick.Invoke();
            count--;
        }
    }

    public void onTutorialPreviousButtonClicked()
    {
        clicksound();
        if (count == 0)
            count = 3;
        else
            count--;
    }

    public void onTutorialNextButtonClicked()
    {
        clicksound();
        if (count == 3)
            count = 0;
        else
            count++;
    }

    public void OnShareButtonClicked()
    {
        OnSettingsButtonClicked();

        //transform.GetComponent<GearMenu>().SwitchOff();
        clicksound();
        Debug.Log("Share");
        //transform.GetComponent<NativeShareScript>().ShareBtnPress();
        //Making changes to share only link
        transform.GetComponent<NativeShareScript>().LinkShareBtnPress();
    }
    public void AboutUsButtonPressed()
    {
        OnSettingsButtonClicked();

        //transform.GetComponent<GearMenu>().SwitchOff();
        clicksound();
        StartCoroutine(OnAboutUsButtonClicked());
        Debug.Log("AboutUsButtonPressed");
    }

    public IEnumerator OnAboutUsButtonClicked()
    {
        yield return new WaitForSeconds(0.7f);
        Application.OpenURL("https://www.revolutiongamesindia.com/index.php/company/");

    }

    public void PrivacyButtonPressed()
    {
        OnSettingsButtonClicked();

        //transform.GetComponent<GearMenu>().SwitchOff();
        clicksound();
        Debug.Log("PrivacyButtonPressed");
        //Application.OpenURL("https://www.revolutiongamesindia.com/index.php/contact/");

    }
    public void TermsAndConditionButtonPressed()
    {
        OnSettingsButtonClicked();

        //transform.GetComponent<GearMenu>().SwitchOff();
        clicksound();
        Debug.Log("TermsAndConditionButtonPressed");
        //Application.OpenURL("https://www.revolutiongamesindia.com/index.php/contact/");
    }


    public void ContactUsButtonPressed()
    {
        OnSettingsButtonClicked();
        //transform.GetComponent<GearMenu>().SwitchOff();
        clicksound();
        Debug.Log("ContactUsButtonPressed");
        Application.OpenURL("https://www.revolutiongamesindia.com/index.php/contact/");
    }

    public void SendEmail()
    {
        //transform.GetComponent<GearMenu>().SwitchOff();
        OnSettingsButtonClicked();
        clicksound();
        StartCoroutine(OnContactUsButtonClicked());
    }

    public IEnumerator OnContactUsButtonClicked()
    {
        yield return new WaitForSeconds(0.7f);
        string email = "support@revolutiongames.com";
        string subject = MyEscapeURL("Support : Sticker Game");


        Application.OpenURL("mailto:" + email + "?subject=" + subject);

    }

    string MyEscapeURL(string URL)
    {
        return WWW.EscapeURL(URL).Replace("+", "%20");
    }

    public void BackButtonPressed()
    {

        clicksound();
        //SceneManager.LoadScene("HomeScene");
        uiManager.homeScreen.SetActive(true);
        //uiManager.spinWheelScreen.SetActive(false);
    }
    public void CloseButton()
    {
        // TutorialPopup.SetActive(false);
    }

    public void clicksound()
    {
        int soundFlag = PlayerPrefs.GetInt("sound");                           //Music and sound

        if (soundFlag == 0)
        {
            AudioManager.Instance.PlaySound(buttonSound1);
        }


    }

    public void OnSettingsButtonClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioManager.Instance.PlaySound(UnlockVideoSound);
        }
        StartCoroutine(PlayerSettingsAnimation());
    }

    public IEnumerator PlayerSettingsAnimation()
    {
       
        if (!settingsPopUp.activeSelf)
        {
            settingsPopUp.SetActive(true);
            settingsButtonAnimator.SetBool("SettingButtonAnimation", true);
            settingsPopUpAnim.SetBool("Open", true);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            settingsButtonAnimator.SetBool("SettingButtonAnimation", false);
            settingsPopUpAnim.SetBool("Open", false);
            yield return new WaitForSeconds(0.5f);   
            settingsPopUp.SetActive(false);
        }
    }

    //Ipad button onclick functions
    public IEnumerator OnTutorialsButtonIpadClicked()
    {
        yield return new WaitForSeconds(0.7f);
        tutorialPanelIpad.GetComponent<Popup>().Toggle();
    }

    public IEnumerator OnPrivacyPolicyButtonIpadClicked()
    {
        yield return new WaitForSeconds(0.7f);
        privacyPanelIpad.GetComponent<Popup>().Toggle();

    }

    public IEnumerator OnTermsButtonIpadClicked()
    {
        yield return new WaitForSeconds(0.7f);
        termsPanelIpad.GetComponent<Popup>().Toggle();

    }

    //non-Ipad button onclick functions
    public IEnumerator OnTutorialsButtonClicked()
    {
        yield return new WaitForSeconds(0.7f);
        tutorialPanel.GetComponent<Popup>().Toggle();
       
    }

    public IEnumerator OnPrivacyPolicyButtonClicked()
    {
        yield return new WaitForSeconds(0.7f);
        privacyPanel.GetComponent<Popup>().Toggle();

    }

    public IEnumerator OnTermsButtonClicked()
    {
        yield return new WaitForSeconds(0.7f);
        termsPanel.GetComponent<Popup>().Toggle();

    }
}
