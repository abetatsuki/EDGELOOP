using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class MoveSpeedUiAdaptor : MonoBehaviour, IMoveSpeedOutput
    {
        public void Publish(MoveSpeedData data)
        {
            IReadOnlyList<IMoveSpeedUiInput> uiInputs = RuntimeServiceRegistry.MoveSpeedUiInputs;
            if (uiInputs == null)
            {
                return;
            }

            MoveSpeedUiData uiData = new MoveSpeedUiData(
                data.CurrentSpeed,
                data.RunSpeed,
                data.SprintSpeed);

            for (int i = 0; i < uiInputs.Count; i++)
            {
                uiInputs[i].Apply(uiData);
            }
        }
    }
}
