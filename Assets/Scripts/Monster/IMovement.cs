namespace ZUN
{
    public interface IMovement
    {
        float MoveSpeed { get; set; }

        void Move();
    }
}