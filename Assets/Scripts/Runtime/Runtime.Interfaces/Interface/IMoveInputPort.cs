namespace Runtime
{
    public interface IMoveInputPort
    {
        void Handle(MoveInputData data);
    }

    public struct MoveInputData
    {
        public float X;
        public float Y;
    }
}
