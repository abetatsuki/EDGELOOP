

namespace Runtime
{
    public class Enviroment : IApplyEnvironmentState
    {
        private readonly CharacterEntity _characterEntity;

        public Enviroment(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public void ApplyEnvironment(Environment environment)
        {
            _characterEntity.SetIsGround(environment.IsGround);
        }
    }
}
