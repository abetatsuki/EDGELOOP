using Runtime;
using UnityEngine;

public class Enviroment : IApplyEnvironmentState
{
    public void ApplyEnvironment(Environment environment)
    {
        _characterEntity.SetIsGround(environment.IsGround);
    }
    private CharacterEntity _characterEntity;
}
