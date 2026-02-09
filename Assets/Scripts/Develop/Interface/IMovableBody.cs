using UnityEngine;

namespace Develop.Interface
{
    public interface IMovableBody
    {
        Vector3 Position { get; set; }
        Vector3 Velocity { get; set; }
        Rigidbody Rigidbody { get; }

        float LinearDamping { get; set; }
        public Quaternion CameraQuaternion {  get; set; }
        public Vector3  Forward { get;}
        public Quaternion PlayerQuaternion { get; set; }

        float ColliderHeight { get; set; } // 追加
        float ColliderCenterY { get; set; } // 追加
        float DefaultColliderHeight { get; } // 追加
        float DefaultColliderCenterY { get; } // 追加
    }
}
