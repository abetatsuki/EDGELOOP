using UnityEngine;
[CreateAssetMenu(fileName = "GunConfig", menuName = "Develop/GunConfig")]
public class GunConfig : ScriptableObject
{
    public LayerMask PlayerMask => _playerLayer;
    public float MaxDistance => _maxDistance;

    public int Ammo => _ammo;

    [SerializeField] private int _ammo;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _maxDistance;
}
