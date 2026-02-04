using Develop.Interface;
using UnityEngine;

namespace Develop.Test
{
    /// <summary>
    /// キー入力でPlayerへの攻撃をシミュレートし、被弾Impulseをテストするクラス
    /// </summary>
    public class AttackTest : MonoBehaviour
    {
        [SerializeField, Tooltip("攻撃対象のPlayerオブジェクト")]
        private GameObject _targetPlayer;
        
        [SerializeField, Tooltip("テスト攻撃のダメージ量")]
        private int _damage = 10;

        private IDamageable _damageable;

        void Start()
        {
            if (_targetPlayer != null)
            {
                // 対象からIDamageableインターフェースを取得
                _damageable = _targetPlayer.GetComponentInChildren<IDamageable>();
                if (_damageable == null)
                {
                    Debug.LogError("AttackTest: 対象のPlayerにIDamageableコンポーネントが見つかりません。", _targetPlayer);
                }
            }
            else
            {
                Debug.LogError("AttackTest: 攻撃対象のPlayerが設定されていません。", this);
            }
        }

        void Update()
        {
            if (_damageable == null) return;

            // キー入力で攻撃方向をシミュレート
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 前からの攻撃をシミュレート
                Vector3 attackDirection = -_targetPlayer.transform.forward;
                _damageable.TakeDamage(_damage, attackDirection);
                Debug.Log("Test Attack: Front");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 後ろからの攻撃をシミュレート
                Vector3 attackDirection = _targetPlayer.transform.forward;
                _damageable.TakeDamage(_damage, attackDirection);
                Debug.Log("Test Attack: Back");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 左からの攻撃をシミュレート
                Vector3 attackDirection = -_targetPlayer.transform.right;
                _damageable.TakeDamage(_damage, attackDirection);
                Debug.Log("Test Attack: Left");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                // 右からの攻撃をシミュレート
                Vector3 attackDirection = _targetPlayer.transform.right;
                _damageable.TakeDamage(_damage, attackDirection);
                Debug.Log("Test Attack: Right");
            }
        }
    }
}
