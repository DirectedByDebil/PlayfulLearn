using UnityEngine;
using System.Collections;

namespace LessonsPractices.MiniGames
{

    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Bullet : MonoBehaviour
    {

        private Rigidbody2D Rigidbody
        {

            get
            {

                if(!_rigidbody)
                {
                    _rigidbody = GetComponent<Rigidbody2D>();
                }

                return _rigidbody;
            }
        }


        private Rigidbody2D _rigidbody;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if(collision.CompareTag("Target"))
            {

                Rigidbody.velocity = Vector3.zero;
            }
        }


        public void OnShot(Vector2 direction, float lifeTime)
        {

            Rigidbody.AddForce(direction, ForceMode2D.Impulse);

            StartCoroutine(HideBullet(lifeTime));
        }


        private IEnumerator HideBullet(float lifeTime)
        {

            yield return new WaitForSeconds(lifeTime);


            Rigidbody.velocity = Vector2.zero;

            gameObject.SetActive(false);
        }
    }
}
