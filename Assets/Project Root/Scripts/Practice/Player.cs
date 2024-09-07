using UnityEngine;

namespace Practice
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private Rigidbody2DSettings _settings;

        [SerializeField, Range(10, 40)] private int SPEED;
        [SerializeField, Range(1000, 5000)] private int JUMP_FORCE;
        [SerializeField, Range(1,3)] private int _amountOfJumps;

        private int _currentJumps;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _currentJumps = _amountOfJumps;

            _settings.GravityChangedEvent += delegate (float value) { _rigidbody2D.gravityScale = value; };
            _settings.DragChangedEvent += delegate (float value) { _rigidbody2D.drag = value; };
            _settings.AngularDragChangedEvent += delegate (float value) { _rigidbody2D.angularDrag = value; };
        
            _settings.Initialization();
        }
        private void Update()
        {
            float dirX = Input.GetAxis("Horizontal");

            Vector2 direction = new Vector2(dirX, 0);
            direction *= SPEED * Time.deltaTime;

            _rigidbody2D.AddForce(direction, ForceMode2D.Impulse);

            if(Input.GetButtonDown("Jump") && _currentJumps > 0)
            {
                _rigidbody2D.AddForce(JUMP_FORCE * Time.deltaTime * Vector2.up, ForceMode2D.Impulse);
                _currentJumps--;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                _currentJumps = _amountOfJumps;
            }
        }
    }
}