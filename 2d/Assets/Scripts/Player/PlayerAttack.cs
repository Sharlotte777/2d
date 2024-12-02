using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damage;

    private float _rechargeTime = 1f;
    private int _attackKey = 0;
    private float _timeBetweenAttack;

    public void Hit(EnemyAttack enemy)
    {
        if (Input.GetMouseButton(_attackKey))
        {
            if (_timeBetweenAttack <= 0)
            {
                enemy.TakeDamage(_damage);
                _timeBetweenAttack = _rechargeTime;
            }
            else
            {
                _timeBetweenAttack -= Time.deltaTime;
            }
        }
    }
}
