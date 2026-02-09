using Develop.Gun.Interface;
﻿using Develop.Interface;
﻿using UnityEngine;
﻿namespace Develop.Gun
﻿{
﻿    public class GunUseCase : IWeapon
﻿    {
﻿        private readonly GunEntity _entity;
﻿        private readonly GunFire _fire;
﻿        private readonly GunConfig _config;
﻿        private readonly IGunView _view;
﻿        private readonly GunEffect _effect;
﻿        private readonly GunAim _aim;
﻿        private readonly GunAnimController _animController;
﻿        private readonly ReloadUseCase _reloadUseCase;
﻿        private readonly GunSwayHandler _swayHandler;
﻿        private readonly GunRecoilHandler _recoilHandler;
﻿        private readonly ILookInputSource _lookInputSource; 
﻿        
﻿        public GunUseCase(GunEntity entity,
﻿            GunFire fire,
﻿            GunConfig config,
﻿            IGunView view,
﻿            GunEffect effect,
﻿            GunAim aim,
﻿            GunAnimController anim,
﻿            ILookInputSource lookInputSource,
﻿            GunRecoilHandler recoilHandler)
﻿        {
﻿            _entity = entity;
﻿            _fire = fire;
﻿            _config = config;
﻿            _view = view;
﻿            _effect = effect;
﻿            _aim = aim;
﻿            _animController = anim;
﻿            _reloadUseCase = new ReloadUseCase(_entity, _config.ReloadTime, anim);
﻿            _swayHandler = new GunSwayHandler(_config);
﻿            _lookInputSource = lookInputSource; 
﻿            _recoilHandler = recoilHandler;
﻿            
﻿            Init();
﻿        }
﻿        
﻿        public void Init()
﻿        {
﻿            _fire.OnFire += _effect.FireEffect;
﻿            _fire.OnHit += _effect.HitEffect;
﻿            _reloadUseCase.OnReloadComplete += _entity.Reload;
﻿        }
﻿        public void TryFire()
﻿        {
﻿            if (_entity.CanFire())
﻿            {
﻿                _entity.RecordFire(Time.time);
﻿                _fire.Fire(_view.FirePosition, _view.Forward, _config.MaxDistance, _config.PlayerMask);
﻿                _recoilHandler.ApplyRecoil();
﻿                Debug.Log("弾を発射しました");
﻿            }
﻿            else
﻿            {
﻿                Debug.Log("弾の発射条件を満たしていません");
﻿            }
﻿        }
﻿        public void TryAim(bool isAim)
﻿        {
﻿            _entity.SetAiming(isAim);
﻿            _animController.SetAimBool(_entity.IsAiming());
﻿        }
﻿       
﻿        public void TryReload()
﻿        {
﻿            _reloadUseCase.StartReload();
﻿        }
﻿
﻿        public void Update()
﻿        {
﻿            _recoilHandler.Update(); // Update recoil
﻿            
﻿            // SwayHandlerは現在のSwayRotationを基準にSwayのターゲットを計算
﻿            var swayRotationOffset = _swayHandler.CalculateSway(_lookInputSource.LookInput, _view.SwayRotation, Time.deltaTime);
﻿            
﻿            // SwayをSwayTransformに適用
﻿            _view.SwayRotation = swayRotationOffset;
﻿            _view.SwayPosition = Vector3.zero; // Swayは位置を動かさないので、原点 (0,0,0) に戻す

﻿            // リコイルのオフセットを取得
﻿            Quaternion recoilRotationOffset = _recoilHandler.GetRecoilRotationOffset();
﻿            Vector3 recoilPositionOffset = _recoilHandler.GetRecoilPositionOffset();

﻿            // RecoilをRecoilTransformに適用
﻿            _view.RecoilRotation = recoilRotationOffset;
﻿            _view.RecoilPosition = recoilPositionOffset;
﻿            
﻿            _reloadUseCase.Update();
﻿        }
﻿    }
﻿}
