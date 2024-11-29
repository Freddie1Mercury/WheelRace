public class Debuff : Obstacle
{
    protected float _debuffTime = 5;
    protected float _remainingTimeUntilEndDebuff;
    protected bool _debuffIsActive = false;
    protected int _debuffBarIndex = int.MinValue;


    public Debuff()
    {
        _remainingTimeUntilEndDebuff = _debuffTime;
    }
}
