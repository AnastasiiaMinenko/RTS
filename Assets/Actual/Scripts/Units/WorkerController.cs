using Commands;
using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorkerController : BaseUnit
{
    private CastleController _castleController;
    private MineController mineController;
    public IntEvent CountWorker = new IntEvent();

    public void SetCastle(CastleController castleController)
    {       
        if (_castleController)
        {
            _castleController.IsAlive.UpdateEvent -= IsAlive_UpdateEvent;
        }
        _castleController = castleController;

        if (_castleController)
        {
            _castleController.IsAlive.UpdateEvent += IsAlive_UpdateEvent;
        }
        TryMineStart();
        
    }
    private void IsAlive_UpdateEvent(bool obj)
    {
        SetCastle(null);
    }
    public void SetMine(MineController mineController)
    {
        this.mineController = mineController;
        TryMineStart();
    }
    private void TryMineStart()
    {
        if (mineController != null && _castleController != null)
        {
            SetBeh(new MiningBehData { Unit = this, CastleController = _castleController, MineController = mineController });
        }
        else
        {
            SetBeh(new NoneBehData());
        }
    }
    
    public override void DestroyUnit()
    {
        base.DestroyUnit();

        SetCastle(null);
        mineController = null;
    }
}