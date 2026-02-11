namespace Runtime
{
    public class CameraLookCommand
    {
        public CameraLookCommand(float yawDelta, float pitch)
        {
            YawDelta = yawDelta;
            Pitch = pitch;
        }

        public float YawDelta { get; private set; }
        public float Pitch { get; private set; }
    }
}
