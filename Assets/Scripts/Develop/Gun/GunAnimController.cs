using UnityEditor.Animations;
using UnityEngine;
namespace Develop.Gun
{
    public class GunAnimController
    {
        public GunAnimController(Animator controller)
        {
            _controller = controller;
        }

        public void SetReloadBool(bool reload)
        {
            Debug.Log(reload);
            _controller.SetBool("Reload", reload);
        }
        public void SetAimBool(bool aim)
        {
            _controller.SetBool("Aim", aim);
        }

        private Animator _controller;
    }
}