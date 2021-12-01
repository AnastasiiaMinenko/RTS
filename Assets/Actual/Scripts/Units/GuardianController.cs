using Commands;
using System.Collections;
using UnityEngine;

public class GuardianController : BaseUnit
{
    private IUnit enemy;
    private Vector3 defaultPos;
    private State _state;
    public override void Init()
    {
        base.Init();

        defaultPos = transform.position;

        GameManager.Data.CoroutineRunner.StartCor(IsUnitClose());
    }
    private IEnumerator IsUnitClose()
    {
        while (true)
        {
            UpdateState();

            yield return new WaitForSeconds(.5f);
        }
    }
    private enum State
    {
        IDLE,
        AGGRESIVE,
        MOVE
    }
    private void SetState(State state)
    {
        if(_state != state)
        {
            _state = state;
            switch (_state)
            {
                case State.IDLE:
                    SetBeh(new NoneBehData());
                    break;
                case State.AGGRESIVE:
                    SetBeh(new MoveAndAttackBehData { Unit = this, Enemy = enemy, Damage = Damage, AttackSpeed = AttackSpeed, Dist = Dist, IsShot = IsShot });
                    break;
                case State.MOVE:
                    SetBeh(new MoveBehData { transform = transform, startPos = transform.position, endPos = defaultPos, duration = 2 });
                    break;                    
            }
        }
    }
    private void UpdateState()
    {
        var state = State.IDLE;
        if (GetCloseUnit(Owner, Pos, 5) != null)
        {
            state = State.AGGRESIVE;
        }
        else if ((defaultPos - transform.position).magnitude > 1) 
        {
            state = State.MOVE;
        }
        SetState(state);
    }
    private IUnit GetCloseUnit(Player player, Vector2 pos, float closeDist)
    {
        IUnit unit = null;
        if (player.Enemies.Count > 0 && player.Enemies[0].Units.Count > 0)
        {
            var arr = player.Enemies[0].Units;

            for (var i = 0; i < arr.Count; i++)
            {
                if (arr[i].IsAlive.Value)
                {
                    var dist = (pos - arr[i].Pos).sqrMagnitude;
                    if (dist <= closeDist)
                    {
                        unit = arr[i];
                        break;
                    }
                }
            }
        }
        return unit;
    }
}
