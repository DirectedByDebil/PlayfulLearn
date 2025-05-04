using Playables;
using UnityEngine;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{
    public sealed class FireMiniGame : BulletMiniGame
    {

        [SerializeField, Space, Range(1, 3)]
        private float _platformInterpolationTime;


        [SerializeField, Space]
        private List<Patrol> _movingPlatforms;


        private MovingPlatformsSystem _platforms;


        private Camera _camera;


        private bool _isPlaying;

        private string _fireButton;


        private void Update()
        {

            if (!_isPlaying) return;

            Vector3 screenPoint = Input.mousePosition;

            Vector3 worldPoint = _camera.ScreenToWorldPoint(screenPoint);


            SetGunRotation(worldPoint);

            SetDirection(worldPoint);


            if(Input.GetButtonDown(_fireButton))
            {

                Shoot();
            }
        }


        public override void Init()
        {

            base.Init();

            _camera = Camera.main;

            _platforms = new MovingPlatformsSystem(_movingPlatforms);
        }


        public override void StartGame()
        {

            if (!CanStart) return;


            _isPlaying = true;

            _platforms.StartPlatformMovement(_platformInterpolationTime);
        }


        public override void Unload()
        {

            base.Unload();

            _platforms.StopPlatformMovement();
        }


        protected override void CheckCodeInput(CodeInput input)
        {

            string code = input.Code;


            if(!IsInputValid(code, "if(", ")"))
            {

                InputFailed(input);
            }
            else
            {
                
                CleanInput(ref code);


                if(input.Description == "Fire")
                {

                    string[] lines = code.Split('\"');


                    if(lines.Length == 3 &&
                        lines[0] == "if(Input.GetButtonDown(" &&
                        lines[2] == "))" &&

                        (lines[1] == "Fire1" ||
                        lines[1] == "Fire2" ||
                        lines[1] == "Fire3"))
                    {
                        
                        _fireButton = lines[1];

                        InputSucceed(input);

                    }
                    else InputFailed(input);

                }
            }
        }
    }
}