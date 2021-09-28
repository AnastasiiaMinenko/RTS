using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleController : BaseUnit
{
    public IntEvent GoldReceived = new IntEvent();
    public void ReceiveGold(int value)
    {
        GoldReceived.Invoke(value);
    }
}
