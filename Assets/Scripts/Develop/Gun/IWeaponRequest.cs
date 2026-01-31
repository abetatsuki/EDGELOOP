namespace Develop.Gun
{
    public interface IGunRequest
    {
        void OnFireRequest(bool isFiring);
        void OnAimRequest(bool isAim);
        void OnReloadRequest();
        void Update();
    }
}