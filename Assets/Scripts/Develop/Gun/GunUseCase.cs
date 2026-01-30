using Develop.Gun.Interface;
using Develop.Interface;
using UnityEngine;
namespace Develop.Gun
{
    public class GunUseCase : IWeapon
    {

        public GunUseCase(GunEntity entity,
            GunFire fire,
            GunConfig config,
            IGunView view,
            GunEffect effect,
            GunAim aim)
        {
            _entity = entity;
            _fire = fire;
            _config = config;
            _view = view;
            _effect = effect;
            _aim = aim;
           
        }
        public void Init()
        {
            //ここでイベント登録などを行う。
            _fire.OnFire += _effect.FireEffect;
        }
        public void TryFire()
        {
            if (_entity.CanFire())
            {
                _entity.RecordFire(_config.FireRate);
                _fire.Fire(_view.FirePosition, _view.Forward, _config.MaxDistance, _config.PlayerMask);
            }
            else
            {
                //Debug.Log("弾の発射条件を満たしていません"); // This can be noisy, commenting out.
            }
        }
        public void TryAim(bool isAim)
        {
            _entity.SetAiming(isAim);
            _aim.Aim(_entity.IsAiming());
        }
        
        private GunAim _aim;
        private GunEntity _entity;
        private GunFire _fire;
        private GunConfig _config;
        private IGunView _view;
        private GunEffect _effect;
    }
}
