using Develop.Gun.Interface;
using Develop.Interface; // Added for ILookInputSource
using UnityEngine;
namespace Develop.Gun
{
    public class GunUseCase : IWeapon
    {
        private readonly GunEntity _entity;
        private readonly GunFire _fire;
        private readonly GunConfig _config;
        private readonly IGunView _view;
        private readonly GunEffect _effect;
        private readonly GunAim _aim;
        private readonly GunAnimController _animController;
        private readonly ReloadUseCase _reloadUseCase;
        private readonly GunSwayHandler _swayHandler;
        private readonly ILookInputSource _lookInputSource; // New field
        
        private readonly Quaternion _initialRotation;

        public GunUseCase(GunEntity entity,
            GunFire fire,
            GunConfig config,
            IGunView view,
            GunEffect effect,
            GunAim aim,
            GunAnimController anim,
            ILookInputSource lookInputSource) // Modified constructor
        {
            _entity = entity;
            _fire = fire;
            _config = config;
            _view = view;
            _effect = effect;
            _aim = aim;
            _animController = anim;
            _reloadUseCase = new ReloadUseCase(_entity, _config.ReloadTime, anim);
            _swayHandler = new GunSwayHandler(_config);
            _lookInputSource = lookInputSource; // Store the input source
            
            _initialRotation = _view.Rotation;
            
            Init();
        }
        
        // Removed SetPresenter method
        
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
            // Removed null check for _presenter as it's no longer directly referenced
            var newRotation = _swayHandler.CalculateSway(_lookInputSource.LookInput, _initialRotation, _view.Rotation, Time.deltaTime);
            _view.TargetSwayRotation = newRotation;
            
            _reloadUseCase.Update();
        }
    }
}
