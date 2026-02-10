using UnityEngine;
using VContainer;

namespace Runtime
{
    public class CharacterDataAdaptor : MonoBehaviour
    {
        [Inject] private CharacterConfig _characterConfig;
        [Inject] private ICharacterConfigPort _characterConfigPort;


        private void Awake()
        {
            SetCharacterConfig();
        }
        private void SetCharacterConfig()
        {
            CharacterConfigData characterConfigData = new CharacterConfigData(
                _characterConfig.JumpPower,
                _characterConfig.MoveSpeed
            );
            _characterConfigPort.SetCharacterConfig(characterConfigData);
        }
    }
}
