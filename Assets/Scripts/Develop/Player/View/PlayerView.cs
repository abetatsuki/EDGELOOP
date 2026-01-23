using Develop.Interface;
using UnityEngine;

namespace Develop.Player.View
{
    public class PlayerView : MonoBehaviour, IMovableBody
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}