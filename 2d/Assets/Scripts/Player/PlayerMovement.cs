using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSearch))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedOfMovement;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private PlayerSearch _search;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _turnedToTheRight = true;
    private string _nameOfAxis = "Horizontal";
    private KeyCode _jumpKey = KeyCode.Space;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _search = GetComponent<PlayerSearch>();
    }

    private void Update()
    {
        float movement = Input.GetAxis(_nameOfAxis);

        _animator.SetFloat("speed", Mathf.Abs(movement));
        _rigidBody.velocity = new Vector2(movement * _speedOfMovement, _rigidBody.velocity.y);

        if (_turnedToTheRight == false && movement > 0)
        {
            Flip();
        }
        else if (_turnedToTheRight == true && movement < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(_jumpKey) && _isGrounded)
        {
            _rigidBody.AddForce(new Vector2(_rigidBody.velocity.x, _jumpForce));
        }

        _search.StartDetection();
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
