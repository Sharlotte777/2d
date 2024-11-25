using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _moveSpots;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackRadius;
    [SerializeField] private Transform _attackPosition;

    private int _currentPoint = 0;
    private float _radiusToFollow = 3f;
    private float _rechargeTime = 3f;
    private float _timeBtwAttack;
    private int _health = 100;
    private int _damage = 10;

    private void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        Transform objectToFollow = _moveSpots[_currentPoint];
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _radiusToFollow);

        if (transform.position == objectToFollow.position)
        {
            _currentPoint = ++_currentPoint % _moveSpots.Length;
        }

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.TryGetComponent(out Player player))
            {
                objectToFollow = player.transform;

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

        transform.position = Vector2.MoveTowards(transform.position, objectToFollow.position, _speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"Враг получил урон, здоровье осталось: {_health}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRadius);
    }

    private void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRadius);

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
            }
        }
    }
}
