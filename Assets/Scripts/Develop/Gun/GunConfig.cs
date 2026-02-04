using UnityEngine;
[CreateAssetMenu(fileName = "GunConfig", menuName = "Develop/GunConfig")]
public class GunConfig : ScriptableObject
{
    public LayerMask PlayerMask => _playerLayer;
    public float MaxDistance => _maxDistance;
    public float AimToSpeed => _aimToSpeed;
    public int MaxAmmo => _maxAmmo;
    public float FireRate => _fireRate;
    public float ReloadTime => _reloadTime;
    
    public float SwayAmount => _swayAmount;
    public float SwaySmooth => _swaySmooth;
    public float MaxSway => _maxSway;

    [SerializeField] private float _aimToSpeed;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _fireRate = 10f; // Default to 10 shots per second
    [SerializeField] private float _reloadTime = 1.5f; // Default reload time
    
    [Header("Sway Settings")]
    [SerializeField] private float _swayAmount = 2f;
    [SerializeField] private float _swaySmooth = 8f;
    [SerializeField] private float _maxSway = 5f;
}
