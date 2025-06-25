namespace ZUN
{
    public interface IMovement
    {
        float BaseMoveSpeed { get; }
        float CurrentMoveSpeed { get; }

        void Move();
        void ApplySlowEffect(float slowMultiplier);
    }
}