namespace Runtime
{
    public enum WallSide
    {
        Left,
        Right
    }

    public class WallRunCommand
    {
        public WallRunCommand(WallSide side, float speed, float duration, float rollDegrees)
        {
            Side = side;
            Speed = speed;
            Duration = duration;
            RollDegrees = rollDegrees;
        }

        public WallSide Side { get; private set; }
        public float Speed { get; private set; }
        public float Duration { get; private set; }
        public float RollDegrees { get; private set; }
    }
}
