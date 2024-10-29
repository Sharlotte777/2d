using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speedOfMovement;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _turnedToTheRight = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void Update()
    {
        string nameOfAxis = "Horizontal";
        KeyCode jumpKey = KeyCode.Space;
        float movement = Input.GetAxis(nameOfAxis);

        _animator.SetFloat("speed",Mathf.Abs(movement));
        _rigidBody.velocity = new Vector2(movement*_speedOfMovement, _rigidBody.velocity.y);

        if (_turnedToTheRight == false && movement > 0)
        {
            Flip();
        }
        else if (_turnedToTheRight == true && movement < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(jumpKey) && _isGrounded)
        {
            _rigidBody.AddForce(new Vector2(_rigidBody.velocity.x, _jumpForce));
        }
    }

    private void Flip()
    {
        int rotationDegrees = 180;
        _turnedToTheRight = !_turnedToTheRight;
        Vector2 rotate = transform.eulerAngles;
        rotate.y += rotationDegrees;
        transform.rotation = Quaternion.Euler(rotate);
    }
}
