using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHeal))]
public class PlayerSearch : MonoBehaviour
{
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private float _radius;

    private PlayerAttack _playerAttack;
    private PlayerHeal _playerHeal;

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHeal = GetComponent<PlayerHeal>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item))
        {
            if (item is FirstAidKit)
            {
                FirstAidKit firstAidKit = item as FirstAidKit;
                _playerHeal.Heal(firstAidKit);
            }
            else if(item is Coin)
            {
                Coin coin = item as Coin;
                Destroy(coin.gameObject);
            }
        }
    }

    public void StartDetection()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPosition.position, _radius);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.TryGetComponent(out EnemyAttack enemy))
            {
                _playerAttack.Hit(enemy);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPosition.position, _radius);
    }
}
