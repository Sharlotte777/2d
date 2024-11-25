using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfFirstAidKits : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private float _timeToSpawn = 5f;
    [SerializeField] private List<SpawnPointOfFirstAidKit> _spawnPoints;

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
                SpawnPointOfFirstAidKit spawnPoint = _spawnPoints[i];
                spawnPoint.CreateFirstAidKit();
            }
        }
    }
}
