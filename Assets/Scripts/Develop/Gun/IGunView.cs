using UnityEngine;

namespace Develop.Gun.Interface
{
    public interface IGunView
    {
        Vector3 Position { get; set; }
        Vector3 FirePosition {  get; set; }

        Vector3 Forward { get; }
        Quaternion Rotation { get; set; }
        ParticleSystem ParticleSystem { get; }
    }
}
