using Playables;
using UnityEngine;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{
    public sealed class MousePositionMiniGame : TargetMiniGame
    {

        [SerializeField, Space]
        private Player _player;


        [SerializeField, Space, Range(1, 3)]
        private float _platformInterpolationTime;


        [SerializeField, Space]
        private List<Patrol> _movingPlatforms;


        private MovingPlatformsSystem _platforms;

        private Camera _camera;


        private bool _isPlaying;



        private void Update()
        {

            if (!_isPlaying) return;


            Vector3 mousePosition = Input.mousePosition;

            mousePosition.z = -_camera.transform.position.z;


            Vector3 worldPoint = _camera.ScreenToWorldPoint(mousePosition);

            _player.MoveToPosition(worldPoint);
        }


        public override void Init()
        {
            
            base.Init();

            _camera = Camera.main;

            _platforms = new MovingPlatformsSystem(_movingPlatforms);


            _player.Init();
        }


        public override void Unload()
        {

            base.Unload();

            _platforms.StopPlatformMovement();
        }


        public override void StartGame()
        {

            if (!CanStart) return;

            _isPlaying = true;


            _platforms.StartPlatformMovement(_platformInterpolationTime);
        }



        protected override void CheckCodeInput(CodeInput input)
        {

            string code = input.Code;

            if(!IsInputValid(code, "Vector3"))
            {

                InputFailed(input);
            }
            else
            {

                CleanInput(ref code);


                if(input.Description == "MousePosition")
                {

                    if(code == "Vector3mousePos=Input.mousePosition")
                    {

                        InputSucceed(input);

                        LockInput(input);
                    }
                    else
                    {
                        InputFailed(input);
                    }
                }
            }

        }
    }
}
