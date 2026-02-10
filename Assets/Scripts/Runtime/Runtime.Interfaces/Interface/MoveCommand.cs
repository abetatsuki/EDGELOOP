namespace Runtime
{
    public class MoveCommand
    {
        public MoveCommand(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
        }

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Speed { get; private set; }
    }
}
