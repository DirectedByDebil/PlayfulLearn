using UnityEngine;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{
    public sealed class Vector2MiniGame : MonoBehaviour, IMiniGame
    {

        [SerializeField, Space]
        private Transform _targetsRoot;


        [SerializeField, Space]
        private Transform _bulletsRoot;


        [SerializeField, Space]
        private Transform _muzzle;


        [SerializeField, Space, Range(0, 8)]
        private float _bulletLifeTime;


        [SerializeField, Space, Range(0, 20)]
        private float _bulletSpeed;


        private ObjectPool<Target> _targetPool;

        private ObjectPool<Bullet> _bulletPool;


        private Vector2 _direction;


        private void Awake()
        {

            _targetPool = new ObjectPool<Target>(_targetsRoot);

            _bulletPool = new ObjectPool<Bullet>(_bulletsRoot);


            _targetPool.UpdateObjects();

            _bulletPool.UpdateObjects();
        }


        public void SetParams()
        {
            _direction = Vector2.right;
        }


        public void StartGame()
        {

            if(_bulletPool.TryGetObject(out Bullet bullet))
            {

                bullet.transform.position = _muzzle.position;

                bullet.gameObject.SetActive(true);

                bullet.OnShot(_direction * _bulletSpeed, _bulletLifeTime);
            }
        }


        public bool IsCompleted()
        {

            List<Target> completedTargets = _targetPool.Objects.FindAll((target) => target.IsTriggered);

            return completedTargets.Count == _targetPool.Objects.Count;
        }
    }
}
