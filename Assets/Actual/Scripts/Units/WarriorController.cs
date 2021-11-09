public class WarriorController : BaseUnit
{
    public override void Init()
    {
        base.Init();

        SetEnemy(null);
    }
    public void SetEnemy(IUnit enemy)
    {
        SetBeh(new MoveAndAttackBehData { Unit = this, Enemy = enemy, Damage = Damage, AttackSpeed = AttackSpeed, Dist = Dist, IsShot = IsShot });
    }
}
