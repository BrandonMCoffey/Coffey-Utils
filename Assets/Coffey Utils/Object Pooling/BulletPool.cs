using System.ComponentModel;
using UnityEngine;

namespace ObjectPoolSystem
{
    public enum BulletType
    {
        Normal,
        Fast
    }

    public class BulletPool : MonoBehaviour
    {
        [Header("Audio Pool")]
        [SerializeField] private Bullet _normalBullet = null;
        [SerializeField] private Bullet _fastBullet = null;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _initialPoolSize = 4;

        private PoolManager<Bullet> _normalPool = new PoolManager<Bullet>();
        private PoolManager<Bullet> _fastPool = new PoolManager<Bullet>();
        private static string _defaultObjectName = "Bullet Manager";
        private static string _defaultPoolName = "Bullet Pool";

        #region Singleton

        private static BulletPool _instance;

        public static BulletPool Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = new GameObject(_defaultObjectName, typeof(BulletPool)).GetComponent<BulletPool>();
                }
                return _instance;
            }
            private set => _instance = value;
        }

        private void Awake()
        {
            if (_instance == this) return;
            if (_instance == null) {
                transform.SetParent(null);
                Instance = this;
            } else {
                Destroy(gameObject);
            }

            #endregion

            if (_poolParent == null) {
                Transform pool = new GameObject(_defaultPoolName).transform;
                pool.SetParent(transform);
                _poolParent = pool;
            }
            _normalPool.BuildInitialPool(_poolParent, _normalBullet, _initialPoolSize);
            _fastPool.BuildInitialPool(_poolParent, _fastBullet, _initialPoolSize);
        }

        public Bullet GetBullet(BulletType type)
        {
            return type switch
            {
                BulletType.Normal => _normalPool.GetObjectFromPool(),
                BulletType.Fast   => _fastPool.GetObjectFromPool(),
                _                 => throw new InvalidEnumArgumentException("Bullet Type")
            };
        }

        public void ReturnBullet(Bullet bullet)
        {
            switch (bullet.Type) {
                case BulletType.Normal:
                    _normalPool.PutObjectIntoPool(bullet);
                    break;
                case BulletType.Fast:
                    _fastPool.PutObjectIntoPool(bullet);
                    break;
                default:
                    throw new InvalidEnumArgumentException("Bullet Type");
            }
        }
    }
}