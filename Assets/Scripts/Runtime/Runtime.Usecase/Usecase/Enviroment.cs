

namespace Runtime
{
    public class Environment : IApplyEnvironmentState
    {
        private readonly CharacterEntity _characterEntity;

        public Environment(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public void ApplyEnvironment(EnvironmentInputData environment)
        {
            _characterEntity.SetIsGround(environment.IsGround);
        }
    }
}
