namespace Runtime
{
    public interface IDashInputPort
    {
        void Handle(DashInputData data);
    }

    public struct DashInputData
    {
        public bool IsPressed;
    }
}
