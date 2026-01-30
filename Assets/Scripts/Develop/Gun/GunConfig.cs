using UnityEngine;
[CreateAssetMenu(fileName = "GunConfig", menuName = "Develop/GunConfig")]
public class GunConfig : ScriptableObject
{
    public LayerMask PlayerMask => _playerLayer;
    public float MaxDistance => _maxDistance;
    public float AimToSpeed => _aimToSpeed;
    public int Ammo => _ammo;

    [SerializeField] private float _aimToSpeed;
    [SerializeField] private int _ammo;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _maxDistance;
}
