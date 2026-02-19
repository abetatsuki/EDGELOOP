namespace Runtime
{
    public enum SlideMode
    {
        Stand,
        Crouch,
        Slide
    }

    public class SlideMotionCommand
    {
        public SlideMotionCommand(SlideMode mode, float x, float y, float power)
        {
            Mode = mode;
            X = x;
            Y = y;
            Power = power;
        }

        public SlideMode Mode { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Power { get; private set; }
    }
}
