using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
public class EnemySearch : MonoBehaviour
{
    [SerializeField] private Transform[] _moveSpots;

    private int _currentPoint = 0;
    private float _radiusToFollow = 3f;
    private EnemyAttack _enemyAttack;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    public Transform StartDetection()
    {
        Transform objectToFollow = _moveSpots[_currentPoint];
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _radiusToFollow);

        if (transform.position == objectToFollow.position)
        {
            _currentPoint = ++_currentPoint % _moveSpots.Length;
        }

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.TryGetComponent(out PlayerHeal player))
            {
                objectToFollow = player.transform;

                _enemyAttack.Hit(player);
            }
        }

        return objectToFollow;
    }
}
