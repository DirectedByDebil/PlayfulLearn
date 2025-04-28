using UnityEngine;
using System.Collections;

namespace Playables
{

    [RequireComponent (typeof (Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {

        public bool CanMove { get; set; }

        public bool CanJump { get; set; }


        #region Serialized Fields

        [SerializeField, Space, Range(0, 20)]
        private float _speed;

        [SerializeField, Space, Range(0, 20)]
        private float _jumpHeight;

        [SerializeField, Space, Range(0, 1)]
        private float _coyotteTime;


        [SerializeField, Space]
        private PlayerView _view;

        #endregion


        private Rigidbody2D _rigidbody;


        private bool _isOnGround;



        private void Update()
        {

            if(CanJump)
            {

                Jump();
            }
        }


        private void FixedUpdate()
        {

            if(CanMove)
            {

                Move();
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {

            _rigidbody.velocity /= 2;

            _isOnGround = true;


            _view.StopJump();
        }


        private void OnCollisionExit2D(Collision2D collision)
        {

            _view.StartJump();

            StartCoroutine(GiveCoyotteTime());
        }


        public void Init()
        {
            
            _rigidbody = GetComponent<Rigidbody2D>();

            _view.Init();
        }


        private void Move()
        {
            
            Vector2 newPosition = new()
            {

                x = Input.GetAxisRaw("Horizontal") * _speed,

                y = _rigidbody.velocity.y,
            };

            
            if(newPosition.x.CompareTo(0) != 0)
            {

                _rigidbody.velocity = newPosition;

            }            
            else if (_isOnGround)
            {

                _rigidbody.velocity *= 0.9f;
            }


            _view.ViewMovement((int)newPosition.x);
        }


        private void Jump()
        {

            if(_isOnGround && Input.GetButtonDown("Jump"))
            {

                Vector2 direction = Vector2.up * _jumpHeight;

                _rigidbody.AddForce(direction, ForceMode2D.Impulse);

                _isOnGround = false;


                _view.StartJump();
            }
        }


        private IEnumerator GiveCoyotteTime()
        {

            yield return new WaitForSeconds(_coyotteTime);

            _isOnGround = false;
        }
    }
}
