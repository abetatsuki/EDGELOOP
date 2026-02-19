namespace Runtime
{
    public class CameraLookCommand
    {
        public CameraLookCommand(float yaw, float pitch)
        {
            Yaw = yaw;
            Pitch = pitch;
        }

        public float Yaw { get; private set; }
        public float Pitch { get; private set; }
    }
}
