using UnityEngine;


namespace LessonsPractices.MiniGames
{
    public abstract class BulletMiniGame : TargetMiniGame
    {

        #region Serialized Fields

        [SerializeField, Space]
        private Transform _bulletsRoot;


        [SerializeField, Space]
        private Transform _startPosition;


        [SerializeField, Space, Range(0, 8)]
        private float _bulletLifeTime;


        [SerializeField, Space, Range(0, 20)]
        private float _bulletSpeed;

        #endregion


        private ObjectPool<Bullet> _bulletPool;


        protected Vector2 direction;


        public override void Init()
        {

            base.Init();


            _bulletPool = new ObjectPool<Bullet>(_bulletsRoot);

            _bulletPool.UpdateObjects();
        }


        protected void Shoot()
        {

            if (_bulletPool.TryGetObject(out Bullet bullet))
            {

                bullet.transform.position = _startPosition.position;

                bullet.gameObject.SetActive(true);

                bullet.OnShot(direction * _bulletSpeed, _bulletLifeTime);
            }
        }


        #region Gun Rotation

        protected void SetGunRotation(Vector3 worldPoint)
        {

            Vector3 lookingDir = worldPoint - _startPosition.position;

            float angle = Mathf.Atan2(lookingDir.y, lookingDir.x) * Mathf.Rad2Deg + 225;


            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            _startPosition.rotation = rotation;
        }


        protected void SetGunDirection(Vector3 worldPoint)
        {

            float angle = Mathf.Atan2(worldPoint.y, worldPoint.x) * Mathf.Rad2Deg + 225;


            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            _startPosition.rotation = rotation;
        }

        #endregion


        protected void SetDirection(Vector3 worldPoint)
        {

            direction = worldPoint - _startPosition.position;

            direction.Normalize();
        }
    }
}