namespace Runtime
{
    public class WallRunCameraCommand
    {
        public WallRunCameraCommand(bool isWallRunning, float tiltSign)
        {
            IsWallRunning = isWallRunning;
            TiltSign = tiltSign;
        }

        public bool IsWallRunning { get; private set; }
        public float TiltSign { get; private set; }
    }
}
