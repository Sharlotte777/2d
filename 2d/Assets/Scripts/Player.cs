using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speedOfMovement;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;

    private int _health = 100;
    private int _maxHealth = 100;

    private bool _isGrounded;
    private float _rechargeTime = 1f;
    private float _timeBtwAttack;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _turnedToTheRight = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FirstAidKit firstAidKit))
        {
            if (_health < _maxHealth)
            {
                Heal(firstAidKit);
                Destroy(firstAidKit.gameObject);
            }
        }
        else if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);
        }
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
        int attackKey = 0;
        float movement = Input.GetAxis(nameOfAxis);

        _animator.SetFloat("speed",Mathf.Abs(movement));
        _rigidBody.velocity = new Vector2(movement*_speedOfMovement, _rigidBody.velocity.y);

        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }

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

        if (Input.GetMouseButton(attackKey))
        {
            if (_timeBtwAttack <= 0)
            {
                Attack();
                _timeBtwAttack = _rechargeTime;
            }
            else
            {
                _timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"Игрок получил урон, здоровье осталось: {_health}");
    }

    private void Flip()
    {
        int rotationDegrees = 180;
        _turnedToTheRight = !_turnedToTheRight;
        Vector2 rotate = transform.eulerAngles;
        rotate.y += rotationDegrees;
        transform.rotation = Quaternion.Euler(rotate);
    }

    private void Heal(FirstAidKit firstAidKit)
    {
        if (_health + firstAidKit.RecoveryAmount > _maxHealth) 
        {
            _health = _maxHealth;
        }
        else
        {
            _health += firstAidKit.RecoveryAmount;
        }
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPosition.position, _radius);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPosition.position, _radius);
    }
}
