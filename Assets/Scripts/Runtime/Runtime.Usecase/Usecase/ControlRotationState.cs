namespace Runtime
{
    public class ControlRotationState : IControlRotationReader, IControlRotationWriter
    {
        private ControlRotationData _data;

        public ControlRotationData GetControlRotation()
        {
            return _data;
        }

        public void SetControlRotation(ControlRotationData data)
        {
            _data = data;
        }
    }
}
