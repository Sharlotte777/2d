using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _moveSpots;
    [SerializeField] private float _speed;

    private int _currentPoint = 0;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _moveSpots[_currentPoint].position, _speed * Time.deltaTime);

        _currentPoint = ++_currentPoint % _moveSpots.Length;
    }
}
