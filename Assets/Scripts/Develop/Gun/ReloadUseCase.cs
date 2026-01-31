using System;
using UnityEngine;

namespace Develop.Gun
{
    public class ReloadUseCase
    {
        private readonly GunEntity _entity;
        private readonly float _reloadTime;
        private float _reloadEndTime;

        public event Action OnReloadComplete;

        public ReloadUseCase(GunEntity entity, float reloadTime)
        {
            _entity = entity;
            _reloadTime = reloadTime;
        }

        public void StartReload()
        {
            if (_entity.CanReload())
            {
                _entity.SetReloading(true);
                _reloadEndTime = Time.time + _reloadTime;
            }
        }

        public void Update()
        {
            if (_entity.IsReloading())
            {
                if (Time.time >= _reloadEndTime)
                {
                    CompleteReload();
                }
            }
        }

        private void CompleteReload()
        {
            _entity.SetReloading(false);
            OnReloadComplete?.Invoke();
        }
    }
}