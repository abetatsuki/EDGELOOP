using UnityEngine;

namespace Develop.Interface
{
    public interface IMovableBody
    {
        Vector3 Position { get; set; }
        Vector3 Velocity { get; set; }

        float LinearDamping { get; set; }
    }
}
