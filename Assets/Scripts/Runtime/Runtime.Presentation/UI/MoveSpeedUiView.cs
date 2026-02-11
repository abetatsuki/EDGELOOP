using TMPro;
using UnityEngine;

namespace Runtime
{
    public class MoveSpeedUiView : MonoBehaviour, IMoveSpeedUiInput
    {
        [SerializeField] private TextMeshProUGUI _currentSpeedText;
        [SerializeField] private TextMeshProUGUI _runSpeedText;
        [SerializeField] private TextMeshProUGUI _sprintSpeedText;

        public void Apply(MoveSpeedUiData data)
        {
            if (_currentSpeedText != null)
            {
                _currentSpeedText.text = $"Current: {data.CurrentSpeed:0.00}";
            }

            if (_runSpeedText != null)
            {
                _runSpeedText.text = $"Run: {data.RunSpeed:0.00}";
            }

            if (_sprintSpeedText != null)
            {
                _sprintSpeedText.text = $"Sprint: {data.SprintSpeed:0.00}";
            }
        }
    }
}
