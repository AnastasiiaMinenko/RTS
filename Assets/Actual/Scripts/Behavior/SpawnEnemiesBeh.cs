using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesBeh : IBeh
{
    private bool isStarted;
    private Coroutine beh;    

    private SpawnEnemiesBehData _data;
    public void Start(IBehData data)
    {
        if (!isStarted)
        {
            isStarted = true;
            _data = (SpawnEnemiesBehData)data;
            beh = GameManager.Data.CoroutineRunner.StartCor(Beh(_data.portalController));
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
    private IEnumerator Beh(PortalController portalController)
    {
        yield return null;
        while (true)
        {
            portalController.SpawnUnit(UnitType.ENEMY, new Vector3(26.39f, 1.3f, 0f), Quaternion.identity.eulerAngles);

            yield return new WaitForSeconds(5f);
        }        
    }    
}
public struct SpawnEnemiesBehData : IBehData
{
    public PortalController portalController;
}
