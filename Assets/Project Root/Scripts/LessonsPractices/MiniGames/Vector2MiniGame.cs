using UnityEngine;
using System;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{
    public sealed class Vector2MiniGame : MonoBehaviour, IMiniGame
    {

        public event Action<bool, CodeInput> CodeChecked;

        public event Action<bool> IsCompletedChanged;


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

        private bool _hasMistake;


        private IList<Target> _completedTargets;



        private void Awake()
        {

            _targetPool = new ObjectPool<Target>(_targetsRoot);

            _bulletPool = new ObjectPool<Bullet>(_bulletsRoot);


            _targetPool.UpdateObjects();

            _bulletPool.UpdateObjects();


            _completedTargets = new List<Target>(_targetPool.Objects.Count);

            SetTargets();
        }


        private void OnDestroy()
        {

            UnSetTargets();
        }


        public void SetParams(params CodeInput[] inputs)
        {

            foreach(CodeInput input in inputs)
            {

                string code = input.Code;


                if (HasSyntaxMistake(code))
                {

                    CodeChecked?.Invoke(false, input);

                    continue;
                }


                CleanInput(ref code);


                switch (input.Description)
                {

                    case "direction.x":

                        if(TryReadValue(code, "direction.x=", out float x))
                        {

                            _direction.x = x;

                            _direction.Normalize();

                            CodeChecked?.Invoke(true, input);
                        }
                        else
                        {

                            CodeChecked?.Invoke(false, input);
                        }
                        break;


                    case "direction.y":

                        if (TryReadValue(code, "direction.y=", out float y))
                        {

                            _direction.y = y;

                            _direction.Normalize();

                            CodeChecked?.Invoke(true, input);
                        }
                        else
                        {

                            CodeChecked?.Invoke(false, input);
                        }
                        break;
                }
            }
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

            if (_hasMistake) return false;

            return CountIsCompleted();
        }


        private bool CountIsCompleted()
        {

            return _completedTargets.Count == _targetPool.Objects.Count;
        }


        #region Set/Unset Targets

        private void SetTargets()
        {

            foreach(Target target in _targetPool.Objects)
            {

                target.Triggered += OnTargetTriggered;
            }
        }


        private void UnSetTargets()
        {

            foreach (Target target in _targetPool.Objects)
            {

                if(target)
                {

                    target.Triggered -= OnTargetTriggered;
                }
            }
        }

        #endregion


        private void OnTargetTriggered(Target target)
        {

            if(!_completedTargets.Contains(target))
            {

                _completedTargets.Add(target);

            
                if(CountIsCompleted())
                {

                    IsCompletedChanged?.Invoke(true);
                }
            }
        }


        #region Check Input

        private bool HasSyntaxMistake(string code)
        {

            _hasMistake = !code.StartsWith("direction") || !code.EndsWith(';');

            return _hasMistake;
        }


        private void CleanInput(ref string input)
        {

            input = input.Replace(" ", "");

            input = input.Replace(";", "");
        }


        private bool TryReadValue(string input, string odd, out float value)
        {

            value = 0;

            if (input.Contains(odd))
            {

                input = input.Replace(odd, "");


                return float.TryParse(input, out value);
            }

            return false;
        }
        
        #endregion
    }
}
