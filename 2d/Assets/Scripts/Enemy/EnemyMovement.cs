using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemySearch))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private EnemySearch _enemySearch;

    private void Awake()
    {
        _enemySearch = GetComponent<EnemySearch>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemySearch.StartDetection().position, _speed * Time.deltaTime);
    }
}
