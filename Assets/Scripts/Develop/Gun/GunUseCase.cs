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
            GunEffect effect)
        {
            _entity = entity;
            _fire = fire;
            _config = config;
            _view = view;
            _effect = effect;
        }
        public void Init()
        {
            //ここでイベント登録などを行う。
            _fire.OnFire += _effect.FireEffect;
        }
        public void TryFire()
        {
            if(_entity.IsFiring())
            {
                _entity.DecreaseAmmo();
                _fire.Fire(_view.FirePosition,_view.Forward,_config.MaxDistance,_config.PlayerMask);
            }
            else
            {
                Debug.Log("弾の発射条件を満たしていません");
            }
        }
        private GunEntity _entity;
        private GunFire _fire;
        private GunConfig _config;
        private IGunView _view;
        private GunEffect _effect;
    }
}
