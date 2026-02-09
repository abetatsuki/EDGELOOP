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
    
    public Vector3 RecoilRotationAmount => _recoilRotationAmount;
    public Vector3 RecoilPositionAmount => _recoilPositionAmount;
    public float RecoilSpeed => _recoilSpeed;
    public float RecoilReturnSpeed => _recoilReturnSpeed;

    public Vector3 MaxRecoilRotation => _maxRecoilRotation;
    public Vector3 MaxRecoilPosition => _maxRecoilPosition;

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
    
    [Header("Recoil Settings")]
    [SerializeField] private Vector3 _recoilRotationAmount = new Vector3(2f, 0.5f, 0f);
    [SerializeField] private Vector3 _recoilPositionAmount = new Vector3(0f, 0f, -0.1f);
    [SerializeField] private float _recoilSpeed = 10f;
    [SerializeField] private float _recoilReturnSpeed = 5f;

    [Header("Recoil Limits")]
    [SerializeField] private Vector3 _maxRecoilRotation = new Vector3(20f, 10f, 0f);
    [SerializeField] private Vector3 _maxRecoilPosition = new Vector3(0f, 0f, -0.5f);
}
