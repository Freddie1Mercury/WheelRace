public class Buff : Obstacle
{
    protected float _buffTime = 5;
    protected float _remainingTimeUntilEndBuff;
    protected bool _buffIsActive = false;
    protected int _buffBarIndex = int.MinValue;

    public Buff()
    {
        _remainingTimeUntilEndBuff = _buffTime;
    }
}
