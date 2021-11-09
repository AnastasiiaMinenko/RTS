using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGoldBeh : IBeh
{
    public IntEvent GoldReceived = new IntEvent();

    private bool isStarted;
    private Coroutine beh;    

    private AddGoldBehData _data;
    public void Start(IBehData data)
    {
        if (!isStarted)
        {
            isStarted = true;
            _data = (AddGoldBehData)data;
            beh = GameManager.Data.CoroutineRunner.StartCor(Beh(_data.castleController));
        }        
    }
    public void Stop()
    {
        if (isStarted)
        {
            isStarted = false;
            GameManager.Data.CoroutineRunner.StopCor(beh);
        }
    }
    private IEnumerator Beh(CastleController castleController)
    {
        var delay = new WaitForSeconds(1f);
        while (true)
        {
            yield return delay;
            castleController.ReceiveGold(5);
        }
    }    
}
public struct AddGoldBehData : IBehData
{
    public CastleController castleController;
}
