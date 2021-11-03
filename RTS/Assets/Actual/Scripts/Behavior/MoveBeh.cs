using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeh : IBeh
{
    private bool isStarted;
    private Coroutine beh;

    private MoveBehData _data;
    public void Start(IBehData data)
    {
        if (!isStarted)
        {
            isStarted = true;
            _data = (MoveBehData)data;
            beh = GameManager.Data.CoroutineRunner.StartCor(CoroutineRunnerExt.Move(_data.transform, _data.startPos, _data.endPos, _data.duration));
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
}
public struct MoveBehData : IBehData
{
    public Transform transform;
    public Vector3 startPos;
    public Vector3 endPos;
    public float duration;
}
