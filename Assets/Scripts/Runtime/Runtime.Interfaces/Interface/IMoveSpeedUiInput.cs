namespace Runtime
{
    public interface IMoveSpeedUiInput
    {
        void Apply(MoveSpeedUiData data);
    }

    public struct MoveSpeedUiData
    {
        public MoveSpeedUiData(float currentSpeed, float runSpeed, float sprintSpeed)
        {
            CurrentSpeed = currentSpeed;
            RunSpeed = runSpeed;
            SprintSpeed = sprintSpeed;
        }

        public float CurrentSpeed { get; private set; }
        public float RunSpeed { get; private set; }
        public float SprintSpeed { get; private set; }
    }
}
