using Develop.Gun.Interface;
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
            GunAim aim,
            GunAnimController anim)
        {
            _entity = entity;
            _fire = fire;
            _config = config;
            _view = view;
            _effect = effect;
            _aim = aim;
            _animController = anim;
            _reloadUseCase = new ReloadUseCase(_entity, _config.ReloadTime, anim);
            Init();
        }
        public void Init()
        {
            //ここでイベント登録などを行う。
            _fire.OnFire += _effect.FireEffect;
            _fire.OnHit += _effect.HitEffect;
            _reloadUseCase.OnReloadComplete += _entity.Reload;
        }
        public void TryFire()
        {
            if (_entity.CanFire())
            {
                _entity.RecordFire(Time.time);
                _fire.Fire(_view.FirePosition, _view.Forward, _config.MaxDistance, _config.PlayerMask);
                Debug.Log("弾を発射しました");
            }
            else
            {
                Debug.Log("弾の発射条件を満たしていません");
            }
        }
        public void TryAim(bool isAim)
        {
            _entity.SetAiming(isAim);
            _animController.SetAimBool(_entity.IsAiming());
        }
       

        public void TryReload()
        {
            _reloadUseCase.StartReload();
        }

        public void Update()
        {
               
            _reloadUseCase.Update();
        }

        private GunAnimController _animController;
        private GunAim _aim;
        private GunEntity _entity;
        private GunFire _fire;
        private GunConfig _config;
        private IGunView _view;
        private GunEffect _effect;
        private ReloadUseCase _reloadUseCase;
    }
}
