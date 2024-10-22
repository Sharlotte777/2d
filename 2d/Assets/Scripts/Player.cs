using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _turnedToTheRight = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");
        _animator.SetFloat("speed",Mathf.Abs(movement));
        _rigidBody.velocity = new Vector2(movement*_speed, _rigidBody.velocity.y);

        if (_turnedToTheRight == false && movement > 0)
        {
            Flip();
        }
        else if (_turnedToTheRight == true && movement < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidBody.AddForce(new Vector2(_rigidBody.velocity.x, _jumpForce));
        }
    }

    private void Flip()
    {
        _turnedToTheRight = !_turnedToTheRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
