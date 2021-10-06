using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : BaseUnit
{    
    public void Init()
    {        
        StartCoroutine(SpawnEnemy());
    }

    public IEnumerator SpawnEnemy()
    {
        while (true)
        {      
            SpawnUnit(UnitType.ENEMY, new Vector3(26.39f, 1.3f, 0f), Quaternion.identity.eulerAngles);          
            
            yield return new WaitForSeconds(10f);
        }
    }
    private void SpawnUnit(UnitType type, Vector2 pos, Vector3 rot)
    {
        CommandExecutor.Execute(new SpawnUnitData
        {
            Type = type,
            Pos = pos,
            Rot = rot,
            Player= Owner
        });        
    }
}
