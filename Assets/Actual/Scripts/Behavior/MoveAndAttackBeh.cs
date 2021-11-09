using Commands;
using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndAttackBeh : IBeh
{
    private bool isStarted;
    private Coroutine beh;

    private IUnit _unit;
    private IUnit _enemy;

    private MoveAndAttackBehData _data;
    public void Start(IBehData data)
    {

        if (!isStarted)
        {
            isStarted = true;
            _data = (MoveAndAttackBehData)data;
            _unit = _data.Unit;
            SetEnemy(_data.Enemy != null ? _data.Enemy : ReceiveEnemy(_unit.Owner, _unit.Pos));
        }
    }
    public void Stop()
    {
        if (isStarted)
        {
            isStarted = false;
            SetEnemy(null);
            _unit = null;
        }
    }
    public void SetEnemy(IUnit enemy)
    {
        GameManager.Data.CoroutineRunner.StopCor(beh);
        if (_enemy != null)
        {
            _enemy.IsAlive.UpdateEvent -= IsAlive_UpdateEvent;
        }

        _enemy = enemy;

        if (_enemy != null)
        {
            beh = GameManager.Data.CoroutineRunner.StartCor(Beh(_unit, _enemy, _data.Damage, _data.AttackSpeed, _data.Dist, _data.IsShot));
            _enemy.IsAlive.UpdateEvent += IsAlive_UpdateEvent;
        }
    }
    private void IsAlive_UpdateEvent(bool isVal)
    {
        SetEnemy(_data.Enemy != null ? _data.Enemy : ReceiveEnemy(_unit.Owner, _unit.Pos));
    }
    private IEnumerator Beh(IUnit unit, IUnit enemy, ActiveData<float> damage, ActiveData<float> attackSpeed, ActiveData<float> dist, ActiveData<bool> isShot)
    {
        var unitTransform = unit.Transform;

        while (true)
        {
            if (enemy == null)
            {
                SetEnemy(ReceiveEnemy(unit.Owner, unit.Pos));
            }
            if (enemy != null)
            {
                if ((unit.Pos - enemy.Pos).magnitude > dist.Value)
                {
                    MoveTo(unitTransform, enemy.Pos, unit.MoveSpeed);
                }
                else
                {
                    if (isShot.Value)
                    {
                        DoShot(unitTransform, enemy.Transform);                        
                    }
                    
                    /*if (unit.Type == UnitType.WARRIOR)
                    {
                        enemy.SetAnimBool("isFight", true);
                    }
                    else if (unit.Type == UnitType.ARCHER)
                    {
                        enemy.SetAnimBool("isAttack", true);
                    }*/
                    yield return new WaitForSeconds(attackSpeed.Value / 2);

                    CommandExecutor.Execute(new HitUnitData
                    {
                        Enemy = enemy,
                        Damage = damage.Value                        
                    });
                    yield return new WaitForSeconds(attackSpeed.Value / 2);
                }
            }
            yield return null;
        }
    }
    private void MoveTo(Transform transform, Vector2 target, float speedMove)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedMove * Time.deltaTime);
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
    public static ObjectPool ObjectPool = new ObjectPool();
    
    private static void DoShot(Transform unitTransform, Transform enemyTransform)
    {
        
        var gameObject = ObjectPool.GetObject("Prefabs/Shot");
        gameObject.transform.position = unitTransform.position;
        
        GameManager.Data.CoroutineRunner.StartCor(CoroutineRunnerExt.Move(gameObject.transform, unitTransform.position, enemyTransform.position, .5f, gameObject, DestroyObj));
              
    }
    private static void DestroyObj(object gameObject)
    {
        ObjectPool.ReturnObject((GameObject)gameObject);
    }
}

public struct MoveAndAttackBehData : IBehData
{
    public IUnit Unit;
    public IUnit Enemy;
    public ActiveData<float> Damage;
    public ActiveData<float> AttackSpeed;
    public ActiveData<float> Dist;
    public ActiveData<bool> IsShot;
}

public class ObjectPool
{
    private Dictionary<string, List<GameObject>> dict = new Dictionary<string, List<GameObject>>();
    public GameObject GetObject(string path)
    {
        GameObject result = null;

        var items = dict.ContainsKey(path) ? dict[path] : null;
        if (items==null)
        {
            items = new List<GameObject>();

            dict.Add(path, items);
        }

        for (var i = 0; i < items.Count; i++)
        {
            if (!items[i].activeSelf)
            {
                result = items[i];
                break;
            }
        }
        if (result == null)
        {
            var prefab = Resources.Load<GameObject>(path);
            var gameObject = GameObject.Instantiate(prefab, GameManager.Data.GameField);
            items.Add(gameObject);
            result = gameObject;
        }
        result.SetActive(true);

        return result;
    }
    public void ReturnObject(GameObject item)
    {
        item.SetActive(false);
    }
}
