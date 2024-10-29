using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private float _timeToSpawn = 2f;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitSpawn = new(_timeToSpawn);

        while (true)
        {
            yield return waitSpawn;

            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = _spawnPoints[i];
                spawnPoint.CreateCoin();
            }
        }
    }
}
