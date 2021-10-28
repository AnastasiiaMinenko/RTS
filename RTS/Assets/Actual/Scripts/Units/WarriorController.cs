using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;
using UnityEngine.SceneManagement;

public class WarriorController : BaseUnit
{    
    private float damage = 3f;
    private IUnit enemy;

    private Animator anim1;
    public void Init()
    {
        anim1 = GetComponentInChildren<Animator>();
        SetEnemy(ReceiveEnemy(Owner, Pos));
    }
    public void SetEnemy(IUnit enemy)
    {
        if (enemy != null)
        {
            enemy.IsAlive.UpdateEvent -= IsAlive_UpdateEvent;
        }

        this.enemy = enemy;

        if (enemy != null)
        {
            GameManager.Data.CoroutineRunner.StopCor(beh);
            beh = GameManager.Data.CoroutineRunner.StartCor(MoveAndAttack());
            enemy.IsAlive.UpdateEvent += IsAlive_UpdateEvent;
        }
    }

    private void IsAlive_UpdateEvent(bool obj)
    {
        SetEnemy(null);
    }

    private IEnumerator MoveAndAttack()
    {
        while (true)
        {
            if (enemy == null)
            {
                SetEnemy(ReceiveEnemy(Owner, Pos));
            }

            if (enemy != null)
            {                
                if ((Pos - enemy.Pos).magnitude > 1)
                {           
                    if(Type==UnitType.WARRIOR)
                    {
                        anim1.SetBool("isFight", true);
                    }                    
                    MoveTo(enemy.Pos);
                }
                else
                {
                    CommandExecutor.Execute(new HitUnitData
                    {
                        Enemy = enemy,
                        Damage = damage
                    });
                    yield return new WaitForSeconds(1f);
                }
            }
            yield return null;
        }
    }
    private void MoveTo(Vector2 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
    }

    private static IUnit ReceiveEnemy(Player player, Vector2 pos)
    {
        IUnit unit = null;
        if (player.Enemies.Count > 0 && player.Enemies[0].Units.Count > 0)
        {
            var arr = player.Enemies[0].Units;
            var minDist = float.MaxValue;

            for (var i = 0; i < arr.Count; i++)
            {                
                if (arr[i].IsAlive.Value)
                {                    
                    var dist = (pos - arr[i].Pos).sqrMagnitude;
                    if (dist < minDist)
                    {                        
                        minDist = dist;
                        unit = arr[i];

                    }
                }
            }
        }
        
        return unit;
    }    
}
