using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    private int _health = 100;
    private int _maxHealth = 100;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"Игрок получил урон, здоровье осталось: {_health}");

        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Heal(FirstAidKit firstAidKit)
    {
        if (_health < _maxHealth)
        {
            if (_health + firstAidKit.RecoveryAmount > _maxHealth)
            {
                _health = _maxHealth;
            }
            else
            {
                _health += firstAidKit.RecoveryAmount;
            }

            Destroy(firstAidKit.gameObject);
        }
    }
}
