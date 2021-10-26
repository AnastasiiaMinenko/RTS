using Commands;
using Common.Tools.Data;
using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{    
    private ActiveListData<IUnit> units = new ActiveListData<IUnit>();
    public ActiveListData<IUnit> Units => units;
    public ActiveData<IUnit>  SelectedUnit = new ActiveData<IUnit>();
    public ActiveData<int> Gold = new ActiveData<int>();
    public ActiveListData<Player> Enemies = new ActiveListData<Player>();
    public string ID;
    
    public void Init()
    {
        SelectedUnit.UpdateEvent += SelectedUnitUpdated;
    }
    public IUnit GetUnitByType (UnitType type)
    {
        IUnit res = null;
        foreach (var unit in units)
        {
            if(unit.Type == type )
            {
                res = unit;
                break;
            }
        }
        return res;
    }
    private void SelectedUnitUpdated(IUnit obj)
    {
        if (obj != null && obj.Type == Commands.UnitType.CASTLE) 
        {
            //buildingCreatingUI.SetActive(true);
            GameManager.Data.UIController.SelectionUI.gameObject.SetActive(true);
            GameManager.Data.UIController.SelectionUI.Init(this);
            Time.timeScale = 0f;
        }
        else
        {
            //buildingCreatingUI.SetActive(false);
            GameManager.Data.UIController.SelectionUI.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void AddCastle(CastleController castle)
    {
        castle.GoldReceived.AddListener(val => {
            Gold.Value += val;            
        }); 
    }
}
