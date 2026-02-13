using UnityEngine;

namespace Runtime
{
    public interface IApplyWallData
    {
        
    }
    public struct WallData
    {
        public WallData(Vector3 wallNormal,Vector3 wallForward)
        {
            _wallNormal = wallNormal;
            _wallForward = wallForward;
        }
        Vector3 _wallNormal;
        Vector3 _wallForward;
    }
}
