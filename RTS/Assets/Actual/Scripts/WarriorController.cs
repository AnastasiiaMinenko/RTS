using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : BaseUnit
{
    private float speedMove = 1f;
    private Coroutine fightCor;
    private bool isMoving;
    private bool isAttacking;

    private IUnit enemy;
    public void Init()
    {
        
        StartCoroutine(MoveAndAttack());
    }
    
    public void SetEnemy(IUnit enemy)
    {
        this.enemy = enemy;

    }
   


    public IEnumerator MoveAndAttack()
    {
        while (true)
        {
            if(enemy == null&& Owner.Enemies.Count>0&& Owner.Enemies[0].Units.Count>0)
            {               
                var arr = Owner.Enemies[0].Units;
                var minDist = float.MaxValue;
                for(var i = 0; i<arr.Count; i++)
                {
                    var dist = (Pos - arr[i].Pos).sqrMagnitude;
                    if(dist<minDist )
                    {
                        minDist = dist;
                        enemy = arr[i];
                        
                    }
                }
            }
           
            if (enemy != null) 
            {
                if((Pos - enemy.Pos).magnitude > 1)
                {                    
                    MoveTo(enemy.Pos);
                }
                else
                {
                    enemy.ReceiveDamage(0.5f);
                    yield return new WaitForSeconds(1f);
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
