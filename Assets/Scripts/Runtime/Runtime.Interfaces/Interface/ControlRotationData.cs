namespace Runtime
{
    public struct ControlRotationData
    {
        public ControlRotationData(float yaw, float pitch)
        {
            Yaw = yaw;
            Pitch = pitch;
        }

        public float Yaw { get; private set; }
        public float Pitch { get; private set; }
    }
}
