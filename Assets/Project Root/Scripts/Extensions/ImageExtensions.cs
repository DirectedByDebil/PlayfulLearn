using UnityEngine;

namespace Extensions
{
    public static class ImageExtensions
    {

        public static bool TryCreateSprite(string fileName, 
            
            out Sprite sprite)
        {

            if (FileExtensions.TryReadBytes(fileName, out byte[] bytes))
            {

                Texture2D texture = new(2, 2);

                texture.LoadImage(bytes);


                sprite = Sprite.Create(texture,

                    new Rect(0f, 0f, texture.width, texture.height),

                    new Vector2(0.5f, 0.5f));


                return true;
            }


            sprite = null;

            return false;
        }

    }
}
