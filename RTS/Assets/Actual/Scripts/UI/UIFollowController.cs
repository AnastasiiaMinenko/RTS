using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowController : MonoBehaviour
{
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
    }    

    void Update()
    {             
        Vector3 screenPos = Camera.main.WorldToScreenPoint(item.position);
        uiItem.anchoredPosition = new Vector2(screenPos.x, (screenPos.y - containerHeight) + offsetY);
    }
}