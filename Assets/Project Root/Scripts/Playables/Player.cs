using UnityEngine;
using System;
using System.Collections;

namespace Playables
{
    public class Player : MonoBehaviour
    {

        public event Action HasGrounded;

        public event Action StoppingRunning;


        #region Serialized Fields

        [SerializeField, Space, Range(0, 20)]
        private float _speed;

        [SerializeField, Space, Range(0, 20)]
        private float _jumpHeight;

        [SerializeField, Space, Range(0, 1)]
        private float _coyotteTime;

        #endregion


        private Rigidbody2D _rigidbody;

        private MovementType _movementType;


        private bool _isRunning;

        private bool _isOnGround;

        private float _elapsedTimeRunning;


        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.CompareTag("Ground"))
            {

                _rigidbody.velocity /= 2;

                _isOnGround = true;

                HasGrounded?.Invoke();
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {

            if (collision.gameObject.CompareTag("Ground"))
            {

                StartCoroutine(GiveCoyotteTime());
            }
        }


        public void Init()
        {

            _rigidbody = GetComponent<Rigidbody2D>();
        }


        public void StopMovement()
        {

            _rigidbody.velocity = Vector3.zero;

            StopAllCoroutines();

            _isRunning = false;
        }


        public void MoveToPosition(Vector3 worldPoint, float interpolationTime)
        {

            if (!_isRunning)
            {

                _isRunning = true;

                _elapsedTimeRunning = 0;


                StartCoroutine(Move(transform.position, worldPoint, interpolationTime));
            }
        }


        public void Jump()
        {

            if (_isOnGround)
            {

            }
                Vector2 direction = Vector2.up * _jumpHeight;

                _rigidbody.AddForce(direction, ForceMode2D.Impulse);

                _isOnGround = false;
        }


        private IEnumerator GiveCoyotteTime()
        {

            yield return new WaitForSeconds(_coyotteTime);

            _isOnGround = false;
        }


        private IEnumerator Move(Vector3 from, Vector3 to, float interpolationTime)
        {

            while(_elapsedTimeRunning < interpolationTime)
            {

                _elapsedTimeRunning += Time.deltaTime;

                float diff = _elapsedTimeRunning / interpolationTime;


                transform.position = Vector3.Lerp(from, to, diff);

                yield return null;
            }

            _isRunning = false;

            StoppingRunning?.Invoke();
        }
    }
}
