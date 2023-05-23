using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
//public class DragHandler : MonoBehaviour,  IDragHandler
{
    public static GameObject itemBeingDragged;

    public Image DeleteImage;
    public Image DeleteText;
    public Image MirrorBaseImage;
    RectTransform deleteText;
    RectTransform deleteImage;
    Vector3 startPosition;
    Vector3 centerPosition;
    private bool resetRegionAndroid;
    Transform startParent;
    //public Canvas PauseCanvas;

    public UIManager uiManager;
    private AudioClip deleteSound;
    private double stickerResetRadius = 0;

    void Start()
    {
        resetRegionAndroid = false;
        uiManager = GameObject.Find("ScriptManager").GetComponent<UIManager>();
        deleteSound = uiManager.deleteSound;
        if (Mathf.Approximately(1.3f, (float)System.Math.Round((double)(uiManager.canvas.rect.width / uiManager.canvas.rect.height), 1)))
        {
            stickerResetRadius = 3.2;
        }
        else
        {
            resetRegionAndroid = true;
            stickerResetRadius = 4.3;
        }

        // PauseCanvas = GameObject.Find("Play Area Canvas").GetComponent<Canvas>();
        DeleteText = GameObject.Find("Delete Text ").GetComponent<Image>();     
        DeleteImage = GameObject.Find("Delete Image").GetComponent<Image>();
        MirrorBaseImage = GameObject.Find("Mirror Base Image").GetComponent<Image>();
        deleteText = DeleteText.GetComponent<RectTransform>();
        deleteImage = DeleteImage.GetComponent<RectTransform>();
        Debug.Log(DeleteText);
        DeleteText.enabled = false;
        DeleteImage.enabled = false;
        centerPosition = transform.position;
    }
    


        public void OnBeginDrag(PointerEventData eventData)
     {
         itemBeingDragged = gameObject;

         startPosition = transform.position;
         startParent = transform.parent;
        DeleteText.enabled = false;
        DeleteImage.enabled = false;
        print(Vector2.Distance(startPosition, transform.position));

    }
    



    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        //transform.position = eventData.position;
        Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y,10);
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        DeleteText.enabled = true;
        DeleteImage.enabled = true;


    }

    public void OnPointerDown(PointerEventData eventData)
     {
         transform.SetAsLastSibling();
     }



     public void OnEndDrag(PointerEventData eventData)
     {
         itemBeingDragged = null;
        DeleteText.enabled = false;
        DeleteImage.enabled = false;

        if (transform.parent != startParent)
         {
             transform.position = startPosition;
         }
        
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        print("Y value" + transform.position.y);
           
        //print(Vector2.Distance(Camera.main.WorldToScreenPoint(transform.position), MirrorBaseImage.transform.position));
        /*if ((viewportPos.x > 30 && viewportPos.x < 190) && (viewportPos.y > 60 && viewportPos.y < 180))
    {
        Destroy(gameObject);
    }*/
        /*if((viewportPos.x > 0.6f && viewportPos.x < 0.9f) && (viewportPos.y > 0.8f && viewportPos.y < 0.9f))
        {
            Destroy(gameObject);
        }*/
        //to remove delete issue in game screen
        /* if ((viewportPos.x > 0.65f && viewportPos.x < 0.8f) && (viewportPos.y > 0.8f && viewportPos.y < 0.95f))
         {
             Destroy(gameObject);
         }
         else if ((viewportPos.x > 0.7f || viewportPos.x < 0.3f) || (viewportPos.y > 0.8f || viewportPos.y < 0.2f))
         {
             //Destroy(gameObject);
             transform.position = Vector3.zero;
         }*/

        if (RectTransformUtility.RectangleContainsScreenPoint(deleteText, transform.position))
        {

            if (PlayerPrefs.GetInt("sound") == 0)
            {
                AudioManager.Instance.PlaySound(deleteSound);
            }
            Destroy(gameObject);
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(deleteImage, transform.position))
        {
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                AudioManager.Instance.PlaySound(deleteSound);
            }
            Destroy(gameObject);
        }
        /*else if ((viewportPos.x > 0.8f || viewportPos.x < 0.2f) || (viewportPos.y > 0.9f || viewportPos.y < 0.15f))
        {
            //Destroy(gameObject);
            transform.position = Vector3.zero;
        }*/
        if (resetRegionAndroid == true)
        {
            if (Vector2.Distance(centerPosition, transform.position) > stickerResetRadius)
            {
                transform.position = Vector3.zero;
            }
        }
        else if (Vector2.Distance(centerPosition, transform.position)> stickerResetRadius||transform.position.y<-2.8f)
        { 
            transform.position = Vector3.zero;
        }
        
    }
   
}


