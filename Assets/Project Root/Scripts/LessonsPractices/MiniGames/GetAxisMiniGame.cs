using Playables;
using UnityEngine;

namespace LessonsPractices.MiniGames
{
    public sealed class GetAxisMiniGame : TargetMiniGame
    {

        [SerializeField, Space]
        private Player _player;


        public override void Init()
        {
            base.Init();


            _player.Init();

            _player.MovementType = MovementType.TopDown;

            _player.CanMove = false;

            _player.CanJump = false;
        }


        public override void StartGame()
        {

            if (!CanStart) return;

            _player.CanMove = true;
        }


        protected override void CheckCodeInput(CodeInput input)
        {

            string code = input.Code;

            if (!IsInputValid(code, "direction"))
            {

                InputFailed(input);
            }
            else
            {

                CleanInput(ref code);


                switch(input.Description)
                {

                    case "Horizontal":

                        if (code == "direction.x=Input.GetAxis(\"Horizontal\")")
                        {

                            InputSucceed(input);

                            LockInput(input);
                        }
                        else
                        {
                            InputFailed(input);
                        }
                        break;


                    case "Vertical":

                        if (code == "direction.y=Input.GetAxis(\"Vertical\")")
                        {

                            InputSucceed(input);

                            LockInput(input);
                        }
                        else
                        {
                            InputFailed(input);
                        }
                        break;
                }
            }
        }
    }
}
