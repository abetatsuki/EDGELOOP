
using Develop.GUn;
using UnityEngine;
namespace Develop.Gun
{
    public class GunPresenter : IGunRequest
    {
        public GunPresenter(GunEntity entity)
        {
            _entity = entity;
        }

        public void OnFireRequest()
        {
            
        }

        private GunEntity _entity;
        private GunUseCase _useCase;
    }

}