namespace Runtime
{
    public interface ICameraRotationOutput
    {
        void ApplyLook(CameraLookCommand command);
    }
}
