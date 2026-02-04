using Develop.Interface;
using UnityEngine;

namespace Develop.Gun
{
    public class GunLookInputSource : ILookInputSource
    {
        public Vector2 LookInput { get; private set; }

        public void SetLookInput(Vector2 input)
        {
            LookInput = input;
        }
    }
}