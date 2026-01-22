using Develop.Interface;
using UnityEngine;
namespace Develop.Player
{
    public class MovementService : IMover
    {
        public void OnMove(Vector3 input)
        {
            Debug.Log(input);
        }
    }
}


