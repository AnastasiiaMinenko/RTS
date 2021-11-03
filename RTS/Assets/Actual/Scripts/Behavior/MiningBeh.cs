using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBeh : IBeh
{
    private bool isStarted;
    private Coroutine beh;

    private MiningBehData _data;
    public void Start(IBehData data)
    {        
        if (!isStarted)
        {
            isStarted = true;
            _data = (MiningBehData)data;
            beh = GameManager.Data.CoroutineRunner.StartCor(Beh(_data.Unit, _data.CastleController, _data.MineController));
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
    private IEnumerator Beh(IUnit unit, CastleController castleController, MineController mineController)
    {
        //TODO FIXITPLS
        int goldAmount = 0;
        int goldCapacity = 1;

        var unitTransform = unit.Transform;
        while (true)
        {
            if (goldAmount == 0)
            {
                if (((Vector2)unitTransform.position - mineController.Pos).magnitude <= 0.8)
                {
                    unit.SetAnimBool("isMining", true);
                    yield return new WaitForSeconds(1f);
                    goldAmount = mineController.GetGold(goldCapacity);
                }
                else
                {
                    unit.SetAnimBool("isMining", false);
                    MoveTo(unitTransform, mineController.Pos, unit.MoveSpeed);
                }
            }
            else
            {
                if (((Vector2)unitTransform.position - castleController.Pos).magnitude <= 2.7)
                {
                    castleController.ReceiveGold(goldAmount);
                    goldAmount = 0;
                }
                else
                {
                    unit.SetAnimBool("isMining", false);
                    MoveTo(unitTransform, castleController.Pos, unit.MoveSpeed);
                }
            }
            yield return null;
        }
    }
    private void MoveTo(Transform transform, Vector2 target, float speedMove)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedMove * Time.deltaTime);
    }
}

public struct MiningBehData : IBehData
{
    public CastleController CastleController;
    public MineController MineController;

    public IUnit Unit;
}
