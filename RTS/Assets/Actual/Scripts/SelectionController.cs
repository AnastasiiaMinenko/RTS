using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] private Material white;
    [SerializeField] private Material grey;

    [HideInInspector] public bool currSelected = false;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ClickSprite();
    }

    public void ClickSprite()
    {
        if(currSelected == false)
        {
            spriteRenderer.material = white;
        }
        else
        {
            spriteRenderer.material = grey;
        }
        
    }
}
