using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleController : BaseUnit
{   
    public IntEvent GoldReceived = new IntEvent();

    private Animator animCastleClick;
    public void Init()
    {        
        beh = GameManager.Data.CoroutineRunner.StartCor(AddGold());
        animCastleClick = GetComponentInChildren<Animator>();
    }
    private IEnumerator AddGold()
    {
        var delay = new WaitForSeconds(1f);
        while(true)
        {
            yield return delay;
            ReceiveGold(5);
        }        
    }
    public void ReceiveGold(int value)
    {
        GoldReceived.Invoke(value);
    }
    public override void SetIsSelected(bool isSelected)
    {
        base.SetIsSelected(isSelected);

        animCastleClick.SetBool("isClick", isSelected);        
    }
}
