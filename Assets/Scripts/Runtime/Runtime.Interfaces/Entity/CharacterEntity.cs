using UnityEngine;
using VContainer;
namespace Runtime
{
    public class CharacterEntity
    {

        public bool CanJump()
        {
            return _isGround;
        }

        public void SetIsGround(bool isGround)
        {
            _isGround = isGround;
        }
        private bool _isGround;

    }
}