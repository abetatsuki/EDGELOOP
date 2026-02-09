using UnityEngine;

namespace Develop.Gun.Interface
{
    public interface IGunView
    {
        Vector3 Position { get; set; }
        Vector3 FirePosition {  get; set; }

        Vector3 Forward { get; }
        Quaternion Rotation { get; set; }
        Quaternion SwayRotation { get; set; }
        Vector3 SwayPosition { get; set; }
        Quaternion RecoilRotation { get; set; } // 追加
        Vector3 RecoilPosition { get; set; } // 追加
        ParticleSystem MuzzleFlash { get; }
        GameObject BulletHolePrefab { get; }

        Vector3 AimPosition { get; set; }
        Vector3 DefaultPosition { get; set; }
        Animator GunAnimator { get; }
    }
}

