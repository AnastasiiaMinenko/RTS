using Commands;
using Common.Tools.Data;
using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player 
{    
    private ActiveListData<IUnit> units = new ActiveListData<IUnit>();
    public ActiveListData<IUnit> Units => units;
    public ActiveListData<IUnit>  SelectedUnit = new ActiveListData<IUnit>();
    public ActiveData<int> Gold = new ActiveData<int>();
    
    public ActiveListData<Player> Enemies = new ActiveListData<Player>();
    public string ID;
       
    public void Init()
    {
        SelectedUnit.UpdateEvent += SelectedUnitUpdated;
    }
    public void DeInit()
    {
        SelectedUnit.UpdateEvent -= SelectedUnitUpdated;
        while(units.Count>0)
        {
            CommandExecutor.Execute(new DestroyUnitData { Unit = units[0] });
        }
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
    private void SelectedUnitUpdated(List<IUnit> list)
    {
        if (list.GetUnitByType(UnitType.CASTLE) != null) 
        {           
            GameManager.Data.UIController.SelectionUI.gameObject.SetActive(true);
            GameManager.Data.UIController.SelectionUI.Init(this);
        }
        else
        {           
            GameManager.Data.UIController.SelectionUI.gameObject.SetActive(false);            
        }
    }
    public void AddCastle(CastleController castle)
    {
        castle.GoldReceived.AddListener(val => {
            Gold.Value += val;            
        }); 
    } 
    
}
public static class UnitListExt
{
    public static IUnit GetUnitByType(this List<IUnit> list, UnitType type)
    {
        IUnit result = null;
        /*for (var i = 0; i < list.Count; i++)
        {            
            if (list[i].Type == type)
            {
                result = list[i];
            }
        }*/
        
        result = list.Find(item => item.Type == type);
        
        return result;
    }   
}