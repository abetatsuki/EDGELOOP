namespace Develop.Gun
{
    public interface IGunRequest
    {
        void OnFireRequest();
        void OnAimRequest(bool isAim);
        void Update();
    }
}