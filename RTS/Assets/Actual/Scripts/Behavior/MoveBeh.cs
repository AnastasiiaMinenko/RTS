using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeh : IBeh
{
    private bool isStarted;
    private Coroutine beh;

    public void Start(IBehData data)
    {
        if (!isStarted)
        {
            isStarted = true;
        }
    }
    public void Stop()
    {
        if (isStarted)
        {
            isStarted = false;
        }
    }
    private IEnumerator Beh()
    {
        yield return null;
    }

}
public struct MoveBehData : IBehData
{

}
