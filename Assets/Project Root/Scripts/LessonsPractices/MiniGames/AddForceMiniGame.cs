using UnityEngine;

namespace LessonsPractices.MiniGames
{
    public sealed class AddForceMiniGame : BulletMiniGame
    {


        public override void Init()
        {

            base.Init();

            direction = new Vector2(1, 1);
        }


        public override void StartGame()
        {

            if (!CanStart) return;

            Shoot();
        }


        protected override void CheckCodeInput(CodeInput input)
        {

            string code = input.Code;


            if(!IsInputValid(code, "rigidbody.AddForce"))
            {

                InputFailed(input);
            }
            else
            {

                CleanInput(ref code);


                if(input.Description == "AddForce")
                {

                    if(code == "rigidbody.AddForce(direction,forceMode)")
                    {

                        InputSucceed(input);
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
