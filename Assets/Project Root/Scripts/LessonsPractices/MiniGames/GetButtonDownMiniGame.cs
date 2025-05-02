using Playables;
using UnityEngine;

namespace LessonsPractices.MiniGames
{
    public sealed class GetButtonDownMiniGame : TargetMiniGame
    {

        [SerializeField, Space]
        private Player _player;


        public override void StartGame()
        {

            if (!CanStart) return;

            _player.CanJump = true;

            _player.CanMove = true;
        }


        protected override void Init()
        {

            base.Init();


            _player.Init();

            _player.MovementType = MovementType.Platformer;

            _player.CanMove = false;

            _player.CanJump = false;
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


                if(input.Description == "GetButtonDown")
                {

                    if(code == "if(Input.GetButtonDown(\"Jump\"))")
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
