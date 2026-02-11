using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Runtime
{
    public class MoveSpeedUiAdaptor : MonoBehaviour, IMoveSpeedOutput
    {
        [Inject] private IReadOnlyList<IMoveSpeedUiInput> _uiInputs;

        public void Publish(MoveSpeedData data)
        {
            MoveSpeedUiData uiData = new MoveSpeedUiData(
                data.CurrentSpeed,
                data.RunSpeed,
                data.SprintSpeed);

            for (int i = 0; i < _uiInputs.Count; i++)
            {
                _uiInputs[i].Apply(uiData);
            }
        }
    }
}
