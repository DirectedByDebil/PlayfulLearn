using UnityEngine;

namespace LessonsPractices.MiniGames
{
    public sealed class AddForceMiniGame : TargetMiniGame
    {

        protected override void Init()
        {

            base.Init();

            direction = new Vector2(1, 1);
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
