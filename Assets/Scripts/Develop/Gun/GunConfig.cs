using UnityEngine;
[CreateAssetMenu(fileName = "GunConfig", menuName = "Develop/GunConfig")]
public class GunConfig : ScriptableObject
{
    public LayerMask PlayerMask => _playerLayer;
    public float MaxDistance => _maxDistance;
    public float AimToSpeed => _aimToSpeed;
    public int Ammo => _ammo;
    public float FireRate => _fireRate;

    [SerializeField] private float _aimToSpeed;
    [SerializeField] private int _ammo;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _fireRate = 10f; // Default to 10 shots per second
}
