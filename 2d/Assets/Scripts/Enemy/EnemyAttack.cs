using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackRadius;
    [SerializeField] private Transform _attackPosition;

    private float _rechargeTime = 3f;
    private float _timeBetweenAttack;
    private int _health = 100;
    private int _damage = 10;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"Враг получил урон, здоровье осталось: {_health}");

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit(PlayerHeal player)
    {
        if (_timeBetweenAttack <= 0)
        {
            player.TakeDamage(_damage);
            _timeBetweenAttack = _rechargeTime;
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRadius);
    }
}
