namespace Runtime
{
    public interface IRunSpeedInputPort
    {
        void Handle(RunSpeedInputData data);
    }

    public struct RunSpeedInputData
    {
        public float Delta;
    }
}
