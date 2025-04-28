using UnityEngine;
using System;

namespace LessonsPractices.MiniGames
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Target : MonoBehaviour
    {

        public event Action<Target> Triggered;


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
            
            if(collision.CompareTag("Bullet") || collision.CompareTag("Player"))
            {

                IsTriggered = true;

                SpriteRenderer.sprite = _winSprite;

                Triggered?.Invoke(this);
            }
        }
    }
}
