namespace LessonsPractices.MiniGames
{
    public sealed class Vector2MiniGame : BulletMiniGame
    {

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


                switch (input.Description)
                {

                    case "direction.x":

                        if (TryReadValue(code, "direction.x=", out float x))
                        {

                            direction.x = x;

                            direction.Normalize();

                            InputSucceed(input);
                        }
                        else
                        {

                            InputFailed(input);
                        }
                        break;


                    case "direction.y":

                        if (TryReadValue(code, "direction.y=", out float y))
                        {

                            direction.y = y;

                            direction.Normalize();

                            InputSucceed(input);
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
