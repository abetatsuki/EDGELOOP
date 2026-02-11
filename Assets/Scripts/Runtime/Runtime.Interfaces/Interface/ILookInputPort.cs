namespace Runtime
{
    public interface ILookInputPort
    {
        void Handle(LookInputData data);
    }

    public struct LookInputData
    {
        public float X;
        public float Y;
    }
}
