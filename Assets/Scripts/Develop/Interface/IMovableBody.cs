using UnityEngine;

namespace Develop.Interface
{
    public interface IMovableBody
    {
        Vector3 Position { get; set; }
        Vector3 Velocity { get; set; }

        float LinearDamping { get; set; }
        public Quaternion CameraQuaternion {  get; set; }
        public Vector3  Forward { get;}
        public Quaternion PlayerQuaternion { get; set; }
    }
}
