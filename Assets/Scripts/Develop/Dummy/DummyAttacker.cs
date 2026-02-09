using Develop;
using Develop.Player.View;
using UnityEngine;

public class DummyAttacker : MonoBehaviour
{
    
    private  PlayerView playerView;
    private void Start()
    {
        playerView = FindAnyObjectByType<PlayerView>();
        Damage();
    }
    
    private void Damage()
    {
       DamageUtility.ApplyDamage(playerView, 10);

    }
}
