using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DanielLochner.Assets.SimpleScrollSnap;

public class ChooseFaceScrollView : MonoBehaviour {

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    public int TOTAL_FACE = 25;


	
	void Start () {

        
        Debug.Log("Start");
        for (int index = 1; index <= TOTAL_FACE; index++)
        {
         //   Debug.Log(index);
            generateItem(index);
        }
      
        scrollView.normalizedPosition = new Vector2(0.0f, 0.0f);
        
    }

   public void ResetFaceScroll()
    {
        /*int count = scrollView.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            // Debug.Log(i);
            Destroy(scrollView.transform.GetChild(i).gameObject);
        }
        for (int index = 1; index <= TOTAL_FACE; index++)
        {
            //   Debug.Log(index);
            generateItem(index);
        }*/
        scrollView.normalizedPosition = new Vector2(0.0f, 0.0f);
    }
	
	
	void Update () {

     
	}

    public void OnValueChanged(Vector2 value){
        Debug.Log(value);
    }

    void generateItem(int itemNumber)
    {
        string filename = "PTS_Home_Small_" + (itemNumber);
        Sprite newSprite = Resources.Load<Sprite>(filename);

        GameObject scrollItemOb = Instantiate(scrollItemPrefab);
        scrollItemOb.transform.SetParent(scrollContent.transform, false);
        scrollItemOb.transform.Find("ChooseFaceButton").gameObject.GetComponent<Button>().image.sprite = newSprite;
        scrollItemOb.transform.Find("ChooseFaceButton").gameObject.GetComponent<Button>().onClick.AddListener(() => onClickFunction(itemNumber));
        //adding a name to identify the button
        scrollItemOb.transform.Find("ChooseFaceButton").gameObject.GetComponent<Button>().name = filename;
    }

    void onClickFunction(int itemNumber)
    {
        if (itemNumber > transform.GetComponent<ChooseFaceScrollActions>().nSelectedFaceIndex)
        {
            int count = itemNumber - transform.GetComponent<ChooseFaceScrollActions>().nSelectedFaceIndex;
            for (int i = 0; i < count; i++)
            {
                transform.GetComponent<ChooseFaceScrollActions>().NextButtonClicked();
            }
        }
        else if (itemNumber < transform.GetComponent<ChooseFaceScrollActions>().nSelectedFaceIndex)
        {
            int count = transform.GetComponent<ChooseFaceScrollActions>().nSelectedFaceIndex - itemNumber;
            for (int i = 0; i < count; i++)
            {
                transform.GetComponent<ChooseFaceScrollActions>().PreviousButtonClicked();
            }
        }
        scrollView.GetComponent<SimpleScrollSnap>().GoToPanel(itemNumber-1);
    }

    
}
