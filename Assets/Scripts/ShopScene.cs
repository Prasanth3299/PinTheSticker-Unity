using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Model;
using Platinio;

public class ShopScene : MonoBehaviour
{

    public GameObject PanelPopupNoNetwork;
    public GameObject PanelPopupAllMusic;
    public GameObject PanelPopupWholePack;
    public GameObject PanelFacePack;
    public GameObject PanelSpecialStickers;
    public GameObject PanelMessageStickers;
    public GameObject PanelHeadwearPack;
    public GameObject PanelGlitterStickers;
    public GameObject PanelPetStickers;
    public GameObject PanelStylePack;
    public GameObject PanelPopupMusicPack1;
    public GameObject PanelPopupMusicPack2;
    public GameObject PanelPopupMusicPack3;
    public GameObject PanelPurchers;
    public GameObject wholeCategoryPack;
    public Image wholeImageLock;
    public Text wholePriceText;
    public Text wholeStatusText;
    public GameObject allMusicPack;
    public Image allMusicImageLock;
    public Text allMusicPriceText;
    public Text allMusicStatusText;
    public GameObject faceCategoryPack;
    public Image faceImageLock;
    public Text facePriceText;
    public Text faceStatusText;
    public GameObject headwearCategoryPack;
    public Image headwearImageLock;
    public Text headwearPriceText;
    public Text headwearStatusText;
    public GameObject styleCategoryPack;
    public Image styleImageLock;
    public Text stylePriceText;
    public Text styleStatusText;
    public GameObject petCategoryPack;
    public Image petImageLock;
    public Text petPriceText;
    public Text petStatusText;
    public GameObject specialCategoryPack;
    public Image specialImageLock;
    public Text specialPriceText;
    public Text specialStatusText;
    public GameObject messageCategoryPack;
    public Image messageImageLock;
    public Text messagePriceText;
    public Text messageStatusText;
    public GameObject glitterCategoryPack;
    public Image glitterImageLock;
    public Text glitterPriceText;
    public Text glitterStatusText;
    public GameObject music1CategoryPack;
    public Image music1ImageLock;
    public Text music1PriceText;
    public Text music1StatusText;
    public GameObject music2CategoryPack;
    public Image music2ImageLock;
    public Text music2PriceText;
    public Text music2StatusText;
    public GameObject music3CategoryPack;
    public Image music3ImageLock;
    public Text music3PriceText;
    public Text music3StatusText;
    public AudioClip buttonSound1;
    public AudioClip buttonSound2;
    public int PackNumber;
    public GameObject facePackButton;
    public GameObject headwearButton;
    public GameObject stylePackButton;
    public GameObject petStickerButton;
    public GameObject specialStickerButton;
    public GameObject messageStickerButton;
    public GameObject glitterStickerButton;
    public GameObject musicPack1Button;
    public GameObject musicPack2Button;
    public GameObject musicPack3Button;
    public GameObject allMusicPackButton;
    public GameObject wholePackButton;
    public GameObject puncherPackButton;
    public GameObject purchasePopup;
    public Text purchaseInfoText;

    public GameObject PanelPopupNoNetworkIpad;
    public GameObject PanelPopupAllMusicIpad;
    public GameObject PanelPopupWholePackIpad;
    public GameObject PanelFacePackIpad;
    public GameObject PanelSpecialStickersIpad;
    public GameObject PanelMessageStickersIpad;
    public GameObject PanelHeadwearPackIpad;
    public GameObject PanelGlitterStickersIpad;
    public GameObject PanelPetStickersIpad;
    public GameObject PanelStylePackIpad;
    public GameObject PanelPopupMusicPack1Ipad;
    public GameObject PanelPopupMusicPack2Ipad;
    public GameObject PanelPopupMusicPack3Ipad;
    public GameObject PanelPurchersIpad;
    public GameObject wholeCategoryPackIpad;
    public Image wholeImageLockIpad;
    public Text wholePriceTextIpad;
    public Text wholeStatusTextIpad;
    public GameObject allMusicPackIpad;
    public Image allMusicImageLockIpad;
    public Text allMusicPriceTextIpad;
    public Text allMusicStatusTextIpad;
    public GameObject faceCategoryPackIpad;
    public Image faceImageLockIpad;
    public Text facePriceTextIpad;
    public Text faceStatusTextIpad;
    public GameObject headwearCategoryPackIpad;
    public Image headwearImageLockIpad;
    public Text headwearPriceTextIpad;
    public Text headwearStatusTextIpad;
    public GameObject styleCategoryPackIpad;
    public Image styleImageLockIpad;
    public Text stylePriceTextIpad;
    public Text styleStatusTextIpad;
    public GameObject petCategoryPackIpad;
    public Image petImageLockIpad;
    public Text petPriceTextIpad;
    public Text petStatusTextIpad;
    public GameObject specialCategoryPackIpad;
    public Image specialImageLockIpad;
    public Text specialPriceTextIpad;
    public Text specialStatusTextIpad;
    public GameObject messageCategoryPackIpad;
    public Image messageImageLockIpad;
    public Text messagePriceTextIpad;
    public Text messageStatusTextIpad;
    public GameObject glitterCategoryPackIpad;
    public Image glitterImageLockIpad;
    public Text glitterPriceTextIpad;
    public Text glitterStatusTextIpad;
    public GameObject music1CategoryPackIpad;
    public Image music1ImageLockIpad;
    public Text music1PriceTextIpad;
    public Text music1StatusTextIpad;
    public GameObject music2CategoryPackIpad;
    public Image music2ImageLockIpad;
    public Text music2PriceTextIpad;
    public Text music2StatusTextIpad;
    public GameObject music3CategoryPackIpad;
    public Image music3ImageLockIpad;
    public Text music3PriceTextIpad;
    public Text music3StatusTextIpad;
    public int PackNumberIpad;
    public GameObject facePackButtonIpad;
    public GameObject headwearButtonIpad;
    public GameObject stylePackButtonIpad;
    public GameObject petStickerButtonIpad;
    public GameObject specialStickerButtonIpad;
    public GameObject messageStickerButtonIpad;
    public GameObject glitterStickerButtonIpad;
    public GameObject musicPack1ButtonIpad;
    public GameObject musicPack2ButtonIpad;
    public GameObject musicPack3ButtonIpad;
    public GameObject allMusicPackButtonIpad;
    public GameObject wholePackButtonIpad;
    public GameObject puncherPackButtonIpad;
    public GameObject purchasePopupIpad;
    public Text purchaseInfoTextIpad;
    List<Shop> shopsList = new List<Shop>();
    int selectedShop;
    private UIManager uiManager;
    public string fromScreenName = "";
    void Start()
    {
        uiManager = transform.GetComponent<UIManager>();

        if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            PanelPopupNoNetwork = PanelPopupNoNetworkIpad;
            PanelPopupAllMusic = PanelPopupAllMusicIpad;
            PanelPopupWholePack = PanelPopupWholePackIpad;
            PanelFacePack = PanelFacePackIpad;
            PanelSpecialStickers = PanelSpecialStickersIpad;
            PanelMessageStickers = PanelMessageStickersIpad;
            PanelHeadwearPack = PanelHeadwearPackIpad;
            PanelGlitterStickers = PanelGlitterStickersIpad;
            PanelPetStickers = PanelPetStickersIpad;
            PanelStylePack = PanelStylePackIpad;
            PanelPopupMusicPack1 = PanelPopupMusicPack1Ipad;
            PanelPopupMusicPack2 = PanelPopupMusicPack2Ipad;
            PanelPopupMusicPack3 = PanelPopupMusicPack3Ipad;
            PanelPurchers = PanelPurchersIpad;
            wholeCategoryPack = wholeCategoryPackIpad;
            wholeImageLock = wholeImageLockIpad;
            wholePriceText = wholePriceTextIpad;
            wholeStatusText = wholeStatusTextIpad;
            allMusicPack = allMusicPackIpad;
            allMusicImageLock = allMusicImageLockIpad;
            allMusicPriceText = allMusicPriceTextIpad;
            allMusicStatusText = allMusicStatusTextIpad;
            faceCategoryPack = faceCategoryPackIpad;
            faceImageLock = faceImageLockIpad;
            facePriceText = facePriceTextIpad;
            faceStatusText = faceStatusTextIpad;

            headwearCategoryPack = headwearCategoryPackIpad;
            headwearImageLock = headwearImageLockIpad;
            headwearPriceText = headwearPriceTextIpad;
            headwearStatusText = headwearStatusTextIpad;
            styleCategoryPack = styleCategoryPackIpad;
            styleImageLock = styleImageLockIpad;
            stylePriceText = stylePriceTextIpad;
            styleStatusText = styleStatusTextIpad;
            petCategoryPack = petCategoryPackIpad;
            petImageLock = petImageLockIpad;
            petPriceText = petPriceTextIpad;
            petStatusText = petStatusTextIpad;
            specialCategoryPack = specialCategoryPackIpad;
            specialImageLock = specialImageLockIpad;
            specialPriceText = specialPriceTextIpad;
            specialStatusText = specialStatusTextIpad;
            messageCategoryPack = messageCategoryPackIpad;
            messageImageLock = messageImageLockIpad;
            messagePriceText = messagePriceTextIpad;
            messageStatusText = messageStatusTextIpad;
            glitterCategoryPack = glitterCategoryPackIpad;
            glitterImageLock = glitterImageLockIpad;
            glitterPriceText = glitterPriceTextIpad;
            glitterStatusText = glitterStatusTextIpad;
            music1CategoryPack = music1CategoryPackIpad;
            music1ImageLock = music1ImageLockIpad;
            music1PriceText = music1PriceTextIpad;
            music1StatusText = music1StatusTextIpad;
            music2CategoryPack = music2CategoryPackIpad;
            music2ImageLock = music2ImageLockIpad;
            music2PriceText = music2PriceTextIpad;
            music2StatusText = music2StatusTextIpad;
            music3CategoryPack = music3CategoryPackIpad;
            music3ImageLock = music3ImageLockIpad;
            music3PriceText = music3PriceTextIpad;
            music3StatusText = music3StatusTextIpad;
            PackNumber = PackNumberIpad;
            facePackButton = facePackButtonIpad;
            headwearButton = headwearButtonIpad;
            stylePackButton = stylePackButtonIpad;
            petStickerButton = petStickerButtonIpad;
            specialStickerButton = specialStickerButtonIpad;
            messageStickerButton = messageStickerButtonIpad;
            glitterStickerButton = glitterStickerButtonIpad;
            musicPack1Button = musicPack1ButtonIpad;
            musicPack2Button = musicPack2ButtonIpad;
            musicPack3Button = musicPack3ButtonIpad;
            allMusicPackButton = allMusicPackButtonIpad;
            wholePackButton = wholePackButtonIpad;
            puncherPackButton = puncherPackButtonIpad;
            purchasePopup = purchasePopupIpad;
            purchaseInfoText = purchaseInfoTextIpad;
        }
        //fetchFromDb();
    }
    public void fetchFromDb()
    {
        //DatabaseManager.InitializeDB();
        shopsList = DatabaseManager.GetShopsValues();

        //Debug.Log(shopsList.Count);

        if (shopsList[0].unlockflag == 1)
        {
            /*//hide lock button
            GameObject.Find("Image Whole Category Pack").transform.Find("Image Lock").GetComponent<Image>().enabled = false;

            //hide price
            GameObject.Find("Image Whole Category Pack").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Whole Category Pack").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Whole Category Pack").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Whole Category Pack").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            wholePriceText.transform.GetComponentInParent<Button>().enabled = false;
            wholeImageLock.enabled = false;
            wholePriceText.enabled = false;
            wholeStatusText.text = "Purchased";
            Vector3 pos = wholeStatusText.transform.localPosition;
            //wholeStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);

        }



        if (shopsList[1].unlockflag == 1)
        {
            /*GameObject.Find("Image All Music").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image All Music").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image All Music").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image All Music").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image All Music").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            allMusicPriceText.transform.GetComponentInParent<Button>().enabled = false;
            allMusicImageLock.enabled = false;
            allMusicPriceText.enabled = false;
            allMusicStatusText.text = "Purchased";
            Vector3 pos = allMusicStatusText.transform.localPosition;
            //allMusicStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }


        if (shopsList[2].unlockflag == 1)
        {
            /*GameObject.Find("Image Face Feature Pack").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Face Feature Pack").transform.Find("Button Buy1").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Face Feature Pack").transform.Find("Button Buy1").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Face Feature Pack").transform.Find("Button Buy1").transform.Find("Text").localPosition;
            GameObject.Find("Image Face Feature Pack").transform.Find("Button Buy1").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            facePriceText.transform.GetComponentInParent<Button>().enabled = false;
            faceImageLock.enabled = false;
            facePriceText.enabled = false;
            faceStatusText.text = "Purchased";
            Vector3 pos = faceStatusText.transform.localPosition;
            //faceStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }
        if (shopsList[3].unlockflag == 1)
        {
            /*GameObject.Find("Image Headwear Pack").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Headwear Pack").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Headwear Pack").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Headwear Pack").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Headwear Pack").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            headwearPriceText.transform.GetComponentInParent<Button>().enabled = false;
            headwearImageLock.enabled = false;
            headwearPriceText.enabled = false;
            headwearStatusText.text = "Purchased";
            Vector3 pos = headwearStatusText.transform.localPosition;
            //headwearStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }



        if (shopsList[4].unlockflag == 1)
        {
            /*GameObject.Find("Image Style Pack").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Style Pack").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Style Pack").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Style Pack").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Style Pack").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            stylePriceText.transform.GetComponentInParent<Button>().enabled = false;
            styleImageLock.enabled = false;
            stylePriceText.enabled = false;
            styleStatusText.text = "Purchased";
            Vector3 pos = styleStatusText.transform.localPosition;
            //styleStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }


        if (shopsList[5].unlockflag == 1)
        {
            /*GameObject.Find("Image Pet Stickers").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Pet Stickers").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Pet Stickers").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Pet Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Pet Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            petPriceText.transform.GetComponentInParent<Button>().enabled = false;
            petImageLock.enabled = false;
            petPriceText.enabled = false;
            petStatusText.text = "Purchased";
            Vector3 pos = petStatusText.transform.localPosition;
            //petStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }

        if (shopsList[6].unlockflag == 1)
        {
            /*GameObject.Find("Image Special Stickers").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Special Stickers").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Special Stickers").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Special Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Special Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            specialPriceText.transform.GetComponentInParent<Button>().enabled = false;
            specialImageLock.enabled = false;
            specialPriceText.enabled = false;
            specialStatusText.text = "Purchased";
            Vector3 pos = specialStatusText.transform.localPosition;
            //specialStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }


        if (shopsList[7].unlockflag == 1)
        {
            /*GameObject.Find("Image Message Stickers").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Message Stickers").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Message Stickers").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Message Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Message Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            messagePriceText.transform.GetComponentInParent<Button>().enabled = false;
            messageImageLock.enabled = false;
            messagePriceText.enabled = false;
            messageStatusText.text = "Purchased";
            Vector3 pos = messageStatusText.transform.localPosition;
            //messageStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }




        if (shopsList[8].unlockflag == 1)
        {
            /*GameObject.Find("Image Glitter Stickers").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Glitter Stickers").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Glitter Stickers").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Glitter Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Glitter Stickers").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            glitterPriceText.transform.GetComponentInParent<Button>().enabled = false;
            glitterImageLock.enabled = false;
            glitterPriceText.enabled = false;
            glitterStatusText.text = "Purchased";
            Vector3 pos = glitterStatusText.transform.localPosition;
            //glitterStatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }

        if (shopsList[9].unlockflag == 1)
        {
            /*GameObject.Find("Image Music Pack1").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Music Pack1").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Music Pack1").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Music Pack1").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Music Pack1").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            music1PriceText.transform.GetComponentInParent<Button>().enabled = false;
            music1ImageLock.enabled = false;
            music1PriceText.enabled = false;
            music1StatusText.text = "Purchased";
            Vector3 pos = music1StatusText.transform.localPosition;
            //music1StatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }

        if (shopsList[10].unlockflag == 1)
        {
            /*GameObject.Find("Image Music Pack2").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Music Pack2").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Music Pack2").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Music Pack2").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Music Pack2").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            music2PriceText.transform.GetComponentInParent<Button>().enabled = false;
            music2ImageLock.enabled = false;
            music2PriceText.enabled = false;
            music2StatusText.text = "Purchased";
            Vector3 pos = music2StatusText.transform.localPosition;
            //music2StatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }

        if (shopsList[11].unlockflag == 1)
        {
            /*GameObject.Find("Image Music Pack3").transform.Find("Image Lock").GetComponent<Image>().enabled = false;
            GameObject.Find("Image Music Pack3").transform.Find("Button Buy").transform.Find("Text (1)").GetComponent<Text>().enabled = false;

            //change text to purchased
            GameObject.Find("Image Music Pack3").transform.Find("Button Buy").transform.Find("Text").GetComponent<Text>().text = "Purchased";
            Vector3 pos = GameObject.Find("Image Music Pack3").transform.Find("Button Buy").transform.Find("Text").localPosition;
            GameObject.Find("Image Music Pack3").transform.Find("Button Buy").transform.Find("Text").localPosition = new Vector3(pos.x, pos.y - 20, pos.z);*/
            music3PriceText.transform.GetComponentInParent<Button>().enabled = false;
            music3ImageLock.enabled = false;
            music3PriceText.enabled = false;
            music3StatusText.text = "Purchased";
            Vector3 pos = music3StatusText.transform.localPosition;
            //music3StatusText.transform.localPosition = new Vector3(pos.x, pos.y - 20, pos.z);
        }
    }
    public void ButtonFacepackClicked()
    {


        selectedShop = 3;
        clicksound();
        PanelFacePack.SetActive(true);
        if (shopsList[2].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("PanelFacePack").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("PanelFacePack").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            facePackButton.SetActive(false);
        }

    }
    public void ButtonCloseFacepackClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelFacePack.SetActive(false);
    }


    public void ButtonSpecialStickersClicked()
    {
        selectedShop = 7;
        clicksound();
        PanelSpecialStickers.SetActive(true);
        if (shopsList[6].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Special Stickersk").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Special Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            specialStickerButton.SetActive(false);
        }
    }
    public void ButtonCloseSpecialStickersClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelSpecialStickers.SetActive(false);
    }


    public void ButtonMessageStickersClicked()
    {
        selectedShop = 8;
        clicksound();
        PanelMessageStickers.SetActive(true);
        if (shopsList[7].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Message Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Message Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            messageStickerButton.SetActive(false);
        }
    }
    public void ButtonCloseMessageStickersClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelMessageStickers.SetActive(false);
    }


    public void ButtonHeadWearClicked()
    {
        selectedShop = 4;
        clicksound();
        PanelHeadwearPack.SetActive(true);
        if (shopsList[3].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Headwear Pack").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Headwear Pack").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            headwearButton.SetActive(false);
        }
    }
    public void ButtonCloseHeadWearClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelHeadwearPack.SetActive(false);
    }


    public void ButtonBuyStylePackClicked()
    {
        selectedShop = 5;
        clicksound();
        PanelStylePack.SetActive(true);
        if (shopsList[4].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Style Pack").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Style Pack").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            stylePackButton.SetActive(false);
        }
    }
    public void ButtonCloseStylePackClicked()
    {
        // clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelStylePack.SetActive(false);
    }


    public void ButtonPetStickersClicked()
    {
        selectedShop = 6;
        clicksound();
        PanelPetStickers.SetActive(true);

        if (shopsList[5].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Pet Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Pet Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            petStickerButton.SetActive(false);
        }
    }
    public void ButtonClosePetStickersClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelPetStickers.SetActive(false);
    }

    public void ButtonGlittersStickersClicked()
    {
        selectedShop = 9;
        clicksound();
        PanelGlitterStickers.SetActive(true);
        if (shopsList[8].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Glitter Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Glitter Stickers").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            glitterStickerButton.SetActive(false);
        }
    }
    public void ButtonCloseGlittersStickersClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelGlitterStickers.SetActive(false);
    }


    public void ButtonBuyMusicPack1Clicked()
    {
        selectedShop = 10;
        clicksound();
        PanelPopupMusicPack1.SetActive(true);
        if (shopsList[9].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Popup Music Pack 1").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Popup Music Pack 1").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            musicPack1Button.SetActive(false);
        }

    }
    public void ButtonCloseMusicPack1Clicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelPopupMusicPack1.SetActive(false);
    }


    public void ButtonMusicPack2Clicked()
    {
        selectedShop = 11;
        clicksound();
        PanelPopupMusicPack2.SetActive(true);
        if (shopsList[10].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Popup Music Pack 2").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Popup Music Pack 2").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            musicPack2Button.SetActive(false);
        }

    }
    public void ButtonCloseMusicPack2Clicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelPopupMusicPack2.SetActive(false);
    }


    public void ButtonBuyMusicPack3Clicked()
    {
        selectedShop = 12;
        clicksound();
        PanelPopupMusicPack3.SetActive(true);
        if (shopsList[11].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Popup Music Pack 3").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Popup Music Pack 3").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            musicPack3Button.SetActive(false);
        }


    }
    public void ButtonCloseMusicPack3Clicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelPopupMusicPack3.SetActive(false);
    }


    public void ButtonBuyAllMusicClicked()
    {
        selectedShop = 2;
        clicksound();
        PanelPopupAllMusic.SetActive(true);
        if (shopsList[1].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Popup All Music").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Popup All Music").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            allMusicPackButton.SetActive(false);
        }


    }
    public void ButtonCloseAllMusicClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelPopupAllMusic.SetActive(false);
    }

    public void ButtonBuyWholePackClicked()
    {

        selectedShop = 1;
        clicksound();
        PanelPopupWholePack.SetActive(true);
        if (shopsList[0].unlockflag == 1)
        {

            //hide popup buy button
            //GameObject.Find("Panel Popup Whole Pack").transform.Find("NosePackPopup").transform.Find("Button Ok").GetComponent<Image>().enabled = false;
            //GameObject.Find("Panel Popup Whole Pack").transform.Find("NosePackPopup").transform.Find("Button Ok").transform.Find("Text (2)").GetComponent<Text>().enabled = false;
            wholePackButton.SetActive(false);
        }


    }
    public void ButtonCloseWholePackClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Okbuttonpressed");
        PanelPopupWholePack.SetActive(false);
    }



    public void BackButtonClicked()
    {
        //clicksound();
        ClickSoundBack();
        Debug.Log("Backbuttonpressed");
        //SceneManager.LoadScene("HomeScene");
        purchasePopup.SetActive(false);
        //uiManager.homeScreen.SetActive(true);
       // uiManager.ChooseHomeScreen("RightToCenter");
        //uiManager.shopScreen.SetActive(false);
        if (fromScreenName == "")
        {
            uiManager.ChooseHomeScreen("RightToCenter");
            uiManager.ChooseShopScreen("CenterToLeft");
        }
        else if (fromScreenName == "game")
        {
            int musicFlag = PlayerPrefs.GetInt("music");                           //Music and sound

            if (musicFlag == 0)
            {
                print("GameData.SelectedMusicIndex : " + GameData.SelectedMusicIndex);
                if (GameData.SelectedMusicIndex >= 1)
                {
                    AudioManager.Instance.PlayMusic(transform.GetComponent<GameScripts>().musicarray[GameData.SelectedMusicIndex - 1]);
                }
                else
                {
                    AudioManager.Instance.PlayMusic(transform.GetComponent<GameScripts>().musicarray[0]);
                }
            }
            transform.GetComponent<GameScripts>().UnlockAllgameDetails();
            uiManager.ChooseGameScreen("RightToCenter");
            uiManager.ChooseShopScreen("CenterToLeft");
        }
        else if (fromScreenName == "music")
        {
            transform.GetComponent<ChooseMusic>().UnlockPurchasedScrolls();
            uiManager.ChooseMusicScreen("RightToCenter");
            uiManager.ChooseShopScreen("CenterToLeft");
        }

    }
    public void NextButtonClicked()
    {
        clicksound();
        Debug.Log("nextbuttonpressed");


    }
    public void PreviousButtonClicked()
    {
        clicksound();
        Debug.Log("previousbuttonpressed");


    }


    public void clicksound()
    {
        int soundFlag = PlayerPrefs.GetInt("sound");                           //Music and sound

        if (soundFlag == 0)
        {
             AudioManager.Instance.PlaySound(buttonSound1);
        }


    }
    public void ClickSoundBack()
    {
        int soundFlag = PlayerPrefs.GetInt("sound");                           //Music and sound

        if (soundFlag == 0)
        {
            print("Ok button clicked");
            AudioManager.Instance.PlaySound(buttonSound2);
        }

    }


    public void BuyWholePackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 0;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 0;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }

    }
    public void BuyAllMusicButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 1;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 1;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }

    }
    public void BuyFacePackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 2;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 2;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }

    }
    
    public void BuyHeadPackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 3;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 3;
            PanelPurchers.GetComponent<Popup>().Toggle();
        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyStylePackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 4;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 4;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyPetPackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 5;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 5;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuySepcialPackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 6;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 6;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyAllMessgePackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 7;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 7;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyGlittersackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 8;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 8;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyMusic1PackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 9;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 9;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyMusic2PackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 10;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/

        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 10;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
    }
    public void BuyMusic3PackButtonClicked()
    {
        clicksound();
        /*StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                print("NETWORK");
                PackNumber = 11;
                PanelPurchers.SetActive(true);
            }
            else
            {
                print("NO NETWORK");
                PanelPopupNoNetwork.SetActive(true);
            }

        }));*/
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            print("NETWORK");
            PackNumber = 11;
            PanelPurchers.GetComponent<Popup>().Toggle();

        }
        else
        {
            print("NO NETWORK");
            PanelPopupNoNetwork.SetActive(true);
        }
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

    public void NoNetworkOKButtonClicked()
    {
        ClickSoundBack();
        PanelPopupNoNetwork.SetActive(false);
    }

    public void PurcherseOKButtonClicked()
    {
        print("success");
        ClickSoundBack();
        StartCoroutine(ItemPurchase());
        
    }

    public IEnumerator ItemPurchase()
    {
        yield return new WaitForSeconds(0.2f);

        Shop shop = shopsList[PackNumber];
        //SetShopPurchaseSucess(shop);
        //SceneManager.LoadScene("ShopScene");
        //uiManager.shopScreen.SetActive(true);
        PanelPopupAllMusic.SetActive(false);
        PanelFacePack.SetActive(false);
        PanelSpecialStickers.SetActive(false);
        PanelMessageStickers.SetActive(false);
        PanelHeadwearPack.SetActive(false);
        PanelGlitterStickers.SetActive(false);
        PanelPetStickers.SetActive(false);
        PanelStylePack.SetActive(false);
        PanelPopupMusicPack1.SetActive(false);
        PanelPopupMusicPack2.SetActive(false);
        PanelPopupMusicPack3.SetActive(false);
        //PanelPurchers.SetActive(false);
        PanelPopupWholePack.SetActive(false);
        purchasePopup.SetActive(false);
        //fetchFromDb();
        switch (PackNumber)
        {
            case 0:
                transform.GetComponent<IAPManager>().OnWholePackClicked();
                break;
            case 1:
                transform.GetComponent<IAPManager>().OnAllMusicClicked();
                break;
            case 2:
                transform.GetComponent<IAPManager>().OnFacePackClicked();
                break;
            case 3:
                transform.GetComponent<IAPManager>().OnHeadwearPackClicked();
                break;
            case 4:
                transform.GetComponent<IAPManager>().OnStylePackClicked();
                break;
            case 5:
                transform.GetComponent<IAPManager>().OnPetStickersClicked();
                break;
            case 6:
                transform.GetComponent<IAPManager>().OnSpecialStickersClicked();
                break;
            case 7:
                transform.GetComponent<IAPManager>().OnMessageStickersClicked();
                break;
            case 8:
                transform.GetComponent<IAPManager>().OnGlitterStickersClicked();
                break;
            case 9:
                transform.GetComponent<IAPManager>().OnMusicPack1Clicked();
                break;
            case 10:
                transform.GetComponent<IAPManager>().OnMusicPack2Clicked();
                break;
            case 11:
                transform.GetComponent<IAPManager>().OnMusicPack3Clicked();
                break;
        }
    }
    public void OnPurchaseConfirmed()
    {
        clicksound();
        Shop shop = shopsList[PackNumber];
        SetShopPurchaseSucess(shop);
        //SceneManager.LoadScene("ShopScene");
        uiManager.shopScreen.SetActive(true);
        fetchFromDb();
        purchasePopup.SetActive(true);
        purchaseInfoText.text = "Your purchase is successfull";
    }

    public void OnPurchaseFailed()
    {
        purchasePopup.SetActive(true);
        purchaseInfoText.text = "Your purchase is unsuccessfull.\n Please try again later";
    }

    public void PurcherseCancelButtonClicked()
    {
        //clicksound();
        ClickSoundBack();
        //PanelPurchers.SetActive(false);
    }

    private void SetShopPurchaseSucess(Shop shop)
    {
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

}
