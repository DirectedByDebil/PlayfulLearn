using Core;
using UnityEngine;

namespace Playables
{

    [RequireComponent (typeof (Animator), typeof(SpriteRenderer))]
    public sealed class PlayerView : MonoBehaviour
    {

        private Animator _animator;

        private SpriteRenderer _spriteRenderer;


        public void Init()
        {

            _spriteRenderer = GetComponent<SpriteRenderer>();

            _animator = GetComponent<Animator>();

            _animator.runtimeAnimatorController = SessionData.SelectedCharacter.AnimatorController;
        }



        public void StartJump()
        {

            _animator.SetBool("Jump", true);
        }


        public void StopJump()
        {

            _animator.SetBool("Jump", false);
        }


        public void ViewMovement(int direction)
        {

            _animator.SetInteger("Movement", direction);


            if(direction > 0)
            {

                _spriteRenderer.flipX = false;

            }
            else if(direction < 0)
            {

                _spriteRenderer.flipX = true;
            }
        }
    }
}
