namespace Runtime
{
    public interface ISlideInputPort
    {
        void Handle(SlideInputData data);
    }

    public struct SlideInputData
    {
        public bool IsPressed;
    }
}
