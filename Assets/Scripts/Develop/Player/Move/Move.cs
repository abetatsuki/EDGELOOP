using Develop.Interface;
using UnityEngine;
namespace Develop.Player
{
    public class Move : IMover
    {
        public void OnMove(Vector3 input)
        {
            Debug.Log(input);
        }
    }
}


