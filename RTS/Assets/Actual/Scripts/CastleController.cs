using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleController : BaseUnit
{   
    public IntEvent GoldReceived = new IntEvent();

    public void Init()
    {
        GameManager.Data.CoroutineRunner.StartCor(AddGold());
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
}
