using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _moveSpots;
    [SerializeField] private float _speed;

    private int _currentPoint = 0;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _moveSpots[_currentPoint].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _moveSpots[_currentPoint].position) < 0.2f)
        {
            if (_currentPoint > 0)
            {
                _currentPoint = 0;
            }
            else
            {
                _currentPoint = 1;
            }
        }
    }
}
