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

        #endregion


        private ObjectPool<Target> _targetPool;


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


        protected virtual void Init()
        {

            _targetPool = new ObjectPool<Target>(_targetsRoot);

            _targetPool.UpdateObjects();


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
