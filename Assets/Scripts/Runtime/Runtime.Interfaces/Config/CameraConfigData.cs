namespace Runtime
{
    public struct CameraConfigData
    {
        public CameraConfigData(float lookSpeed, float maxPitch)
        {
            LookSpeed = lookSpeed;
            MaxPitch = maxPitch;
        }

        public float LookSpeed { get; private set; }
        public float MaxPitch { get; private set; }
    }
}
