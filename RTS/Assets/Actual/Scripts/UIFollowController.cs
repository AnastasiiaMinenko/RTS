using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowController : MonoBehaviour
{
    /*private HealthBarUI healthBar;
    private Player player;

    private GameObject healthPanel;

    private void Update()
    {
        
    }
    Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

    //Находится ли объект перед камерой
    Vector3 cameraRelative = Camera.main.transform.InverseTransformPoint(transform.position);

    Vector3 uiElementPosition = Camera.WorldToScreenPoint(healthBar);
    Vector3 coolNewWorldPosition = Camera.main.ScreenToWorldPoint(uiElementPosition);


    RectTransform myRect = GetComponent<HealthBarUI>(); 
    Vector2 myPositionOnScreen = Camera.main.WorldToScreenPoint(myOwner);
    Canvas copyOfMainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

    //Находится ли объект перед камерой
    Vector3 cameraRelative = Camera.main.transform.InverseTransformPoint(transform.position);*/



    //}


    private RectTransform uiItem;
    private Transform item;
    private float offsetY;
    private float containerHeight;

    public void Init(RectTransform uiItem, Transform item, float offsetY, float containerHeight)
    {
        this.uiItem = uiItem;       
        this.item = item;
        this.offsetY = offsetY;
        this.containerHeight = containerHeight;
        //healthPanel = Instantiate(healthPrefab) as GameObject;        
        //healthPanel.transform.SetParent(canvas.transform, false);
    }    

    void Update()
    {             
        Vector3 screenPos = Camera.main.WorldToScreenPoint(item.position);
        //Debug.Log(screenPos+";"+uiItem.name);
        uiItem.anchoredPosition = new Vector2(screenPos.x, (screenPos.y - containerHeight) + offsetY);
        //Debug.Log(uiItem.anchoredPosition);
    }
}