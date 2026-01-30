using Develop.Gun.Interface;
using UnityEngine;
namespace Develop.Gun
{
    public class GunAim
    {
        public GunAim(IGunView view,float speed)
        {
            _view = view;
            _aimSensitivity = speed;
        }
        public void Aim(bool isAim)
        {
            Vector3 targetPosition = isAim ? _view.AimPosition : _view.DefaultPosition;
            _view.Position = Vector3.SmoothDamp(_view.Position,targetPosition,ref _velocity,1.0f/_aimSensitivity);

        }

        IGunView _view;
        private float _aimSensitivity;
        private Vector3 _velocity;
    }
}