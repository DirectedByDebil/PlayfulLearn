using UnityEngine;
using System.Collections;

namespace Playables
{

    [RequireComponent (typeof (Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {

        public bool CanMove { get; set; }

        public bool CanJump { get; set; }

        public MovementType MovementType
        {
            get => _movementType;

            set
            {

                _movementType = value;


                if(_movementType == MovementType.Platformer)
                {

                    _rigidbody.gravityScale = 1;
                }
                else
                {

                    _rigidbody.gravityScale = 0;
                }
            }
        }


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

        private MovementType _movementType;


        private bool _isOnGround;



        private void Update()
        {

            if(CanJump && MovementType == MovementType.Platformer)
            {

                Jump();
            }
        }


        private void FixedUpdate()
        {

            if(CanMove)
            {

                switch (MovementType)
                {
                    case MovementType.Platformer:

                        MovePlatformer();
                        break;

                 
                    case MovementType.TopDown:

                        MoveTopDown();
                        break;
                }
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {

            if(collision.gameObject.CompareTag("Ground"))
            {

                _rigidbody.velocity /= 2;

                _isOnGround = true;


                _view.StopJump();
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {

            if(collision.gameObject.CompareTag("Ground"))
            {

                _view.StartJump();

                StartCoroutine(GiveCoyotteTime());
            }
        }


        public void Init()
        {
            
            _rigidbody = GetComponent<Rigidbody2D>();

            _view.Init();
        }


        public void MoveToPosition(Vector3 wolrdPoint)
        {

            Vector3 direction = wolrdPoint - transform.position;

            direction.Normalize();

            direction *= _speed * Time.deltaTime;

            direction += transform.position;

            _rigidbody.MovePosition(direction);
        }


        #region Platformer Movement

        private void MovePlatformer()
        {
            
            Vector2 direction = new()
            {

                x = Input.GetAxisRaw("Horizontal") * _speed,

                y = _rigidbody.velocity.y,
            };

            
            if(direction.x.CompareTo(0) != 0)
            {

                _rigidbody.velocity = direction;

            }            
            else if (_isOnGround)
            {

                _rigidbody.velocity *= 0.9f;
            }


            _view.ViewMovement((int)direction.x);
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

        #endregion


        #region Top-Down Movement
        
        private void MoveTopDown()
        {

            Vector2 direction = new ()
            { 
                x = Input.GetAxisRaw("Horizontal"),

                y = Input.GetAxisRaw("Vertical")
            };


            if(direction.x.CompareTo(0) != 0)
            {

                _view.ViewMovement((int)direction.x);
            }
            else
            {

                _view.ViewMovement((int)direction.y);
            }


            direction *= _speed * Time.fixedDeltaTime;

            direction += _rigidbody.position;


            _rigidbody.MovePosition(direction);
        }

        #endregion


        private IEnumerator GiveCoyotteTime()
        {

            yield return new WaitForSeconds(_coyotteTime);

            _isOnGround = false;
        }
    }
}
