using UnityEngine;

namespace LessonsPractices.MiniGames
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Target : MonoBehaviour
    {

        public bool IsTriggered
        {
            get; private set;
        }


        private SpriteRenderer SpriteRenderer
        {
            get
            {

                if(!_spriteRenderer)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }

                return _spriteRenderer;
            }
        }


        [SerializeField, Space]
        private Sprite _winSprite;


        private SpriteRenderer _spriteRenderer;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if(collision.CompareTag("Bullet"))
            {

                IsTriggered = true;

                SpriteRenderer.sprite = _winSprite;
            }
        }
    }
}
