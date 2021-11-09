public class CastleController : BaseUnit
{
    public IntEvent GoldReceived = new IntEvent();

    public override void Init()
    {
        base.Init();

        SetBeh(new AddGoldBehData { castleController = this });
    }
    public void ReceiveGold(int value)
    {
        GoldReceived.Invoke(value);
    }
    public override void SetIsSelected(bool isSelected)
    {
        base.SetIsSelected(isSelected);

        SetAnimBool("isClick", isSelected);
    }
}
