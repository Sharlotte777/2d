using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private float _timeToSpawn = 5f;
    //[SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private List<Item> _spawnPoints;

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
                if (_spawnPoints[i] is SpawnPoint)
                {
                    SpawnPoint spawnPoint = _spawnPoints[i] as SpawnPoint;
                    spawnPoint.CreateCoin();
                }
                else if (_spawnPoints[i] is SpawnPointOfFirstAidKit)
                {
                    SpawnPointOfFirstAidKit spawnPoint = _spawnPoints[i] as SpawnPointOfFirstAidKit;
                    spawnPoint.CreateFirstAidKit();
                }
            }
        }
    }
}
