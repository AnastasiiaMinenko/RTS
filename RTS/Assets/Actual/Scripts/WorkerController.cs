using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : BaseUnit
{
    private float speedMove = 0.8f;
    private int goldAmount = 0;
    private CastleController castleController;
    private MineController mineController;
    private int goldCapacity = 1;
    private Coroutine mineCor;

    public void SetCastle(CastleController castleController)
    {
        this.castleController = castleController;
        TryMineStart();
    }
    public void SetMine(MineController mineController)
    {
        this.mineController = mineController;
        TryMineStart();

        
    }
    private void TryMineStart()
    {
        if (mineController != null && castleController != null)
        {
            GameManager.Data.CoroutineRunner.StopCor(mineCor);
            mineCor = GameManager.Data.CoroutineRunner.StartCor(WhileMove());
        }
        else
        {
            GameManager.Data.CoroutineRunner.StopCor(mineCor);
        }
    }
    public IEnumerator WhileMove()
    {
        while (true)
        {
            if (goldAmount == 0)
            {
                if (((Vector2)transform.position - mineController.Pos).magnitude <= 0.8)
                {
                    goldAmount = mineController.GetGold(goldCapacity);
                }
                else
                {
                    MoveTo(mineController.Pos);
                }
            }
            else
            {
                if (((Vector2)transform.position - castleController.Pos).magnitude <= 2.7)
                {
                    castleController.ReceiveGold(goldAmount);
                    //Debug.Log("4st: " + goldAmount);
                    goldAmount = 0;
                }
                else
                {
                    MoveTo(castleController.Pos);
                }
            }
            yield return null;
        }
    }
    private void MoveTo(Vector2 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedMove * Time.deltaTime);
    }
}





