using UnityEngine;
using System;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{

    public abstract class TargetMiniGame : MiniGame
    {

        public override event Action<bool> IsCompletedChanged;


        #region Serialized Fields

        [SerializeField, Space]
        private Transform _targetsRoot;


        [SerializeField, Space]
        private Transform _bulletsRoot;


        [SerializeField, Space]
        private Transform _startPosition;


        [SerializeField, Space, Range(0, 8)]
        private float _bulletLifeTime;


        [SerializeField, Space, Range(0, 20)]
        private float _bulletSpeed;
        
        #endregion


        private ObjectPool<Target> _targetPool;

        private ObjectPool<Bullet> _bulletPool;


        protected Vector2 direction;


        private IList<Target> _completedTargets;


        #region Awake/Destroy Methods

        private void Awake()
        {
            
            Init();
        }


        private void OnDestroy()
        {

            UnSetTargets();
        }

        #endregion


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


        protected virtual void Init()
        {

            _targetPool = new ObjectPool<Target>(_targetsRoot);

            _bulletPool = new ObjectPool<Bullet>(_bulletsRoot);


            _targetPool.UpdateObjects();

            _bulletPool.UpdateObjects();


            _completedTargets = new List<Target>(_targetPool.Objects.Count);

            SetTargets();
        }


        protected override bool CountIsCompleted()
        {

            return _completedTargets.Count == _targetPool.Objects.Count;
        }


        #region Set/Unset Targets

        private void SetTargets()
        {

            foreach (Target target in _targetPool.Objects)
            {

                target.Triggered += OnTargetTriggered;
            }
        }


        private void UnSetTargets()
        {

            foreach (Target target in _targetPool.Objects)
            {

                if (target)
                {

                    target.Triggered -= OnTargetTriggered;
                }
            }
        }


        private void OnTargetTriggered(Target target)
        {

            if (!_completedTargets.Contains(target))
            {

                _completedTargets.Add(target);


                if (CountIsCompleted())
                {

                    IsCompletedChanged?.Invoke(true);
                }
            }
        }

        #endregion
    }
}
