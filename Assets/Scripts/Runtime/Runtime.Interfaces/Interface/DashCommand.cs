namespace Runtime
{
    public class DashCommand
    {
        public DashCommand(float x, float y, float power)
        {
            X = x;
            Y = y;
            Power = power;
        }

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Power { get; private set; }
    }
}
