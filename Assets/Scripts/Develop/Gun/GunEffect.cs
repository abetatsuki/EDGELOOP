using Develop.Gun.Interface;
using UnityEngine;

namespace Develop.Gun
{
    public class GunEffect
    {
        public GunEffect(IGunView view)
        {
            _muzzleFlash = view.MuzzleFlash;
            _bulletHolePrefab = view.BulleHolePrefab;
        }

        /// <summary>
        /// Plays muzzle flash effect.
        /// </summary>
        public void FireEffect()
        {
            _muzzleFlash.Play();
        }

        /// <summary>
        /// Spawns bullet hole effect at hit position and sticks it to the target.
        /// </summary>
        public void HitEffect(Transform hitTransform, Vector3 hitPoint, Vector3 hitNormal)
        {
            Vector3 position = hitPoint + hitNormal * 0.01f;

            // Rotate quad so it faces away from the surface
            Quaternion rotation = Quaternion.LookRotation(-hitNormal);

            GameObject bulletHole = Object.Instantiate(
                _bulletHolePrefab,
                position,
                rotation);

            // Optional: auto-destroy after time
            Object.Destroy(bulletHole, 10f);
        }

        private readonly ParticleSystem _muzzleFlash;
        private readonly GameObject _bulletHolePrefab;
    }
}
