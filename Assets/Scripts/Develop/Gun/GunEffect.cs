using Develop.Gun.Interface;
using UnityEngine;
namespace Develop.Gun
{
    public class GunEffect
    {
        public GunEffect(IGunView view )
        {
            _particleSystem = view.MuzzleFlash;
        }

        public void FireEffect(Vector3 input)
        {
            _particleSystem.Play();
        }
        private ParticleSystem _particleSystem;
    }
}

