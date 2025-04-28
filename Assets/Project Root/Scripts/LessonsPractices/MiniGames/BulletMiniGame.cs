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


        public override void StartGame()
        {

            if (!CanStart) return;


            if (_bulletPool.TryGetObject(out Bullet bullet))
            {

                bullet.transform.position = _startPosition.position;

                bullet.gameObject.SetActive(true);

                bullet.OnShot(direction * _bulletSpeed, _bulletLifeTime);
            }
        }


        protected override void Init()
        {

            base.Init();


            _bulletPool = new ObjectPool<Bullet>(_bulletsRoot);

            _bulletPool.UpdateObjects();
        }
    }
}
