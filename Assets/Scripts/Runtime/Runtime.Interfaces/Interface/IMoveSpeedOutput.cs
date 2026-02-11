namespace Runtime
{
    public interface IMoveSpeedOutput
    {
        void Publish(MoveSpeedData data);
    }

    public struct MoveSpeedData
    {
        public MoveSpeedData(float currentSpeed, float runSpeed, float sprintSpeed)
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
