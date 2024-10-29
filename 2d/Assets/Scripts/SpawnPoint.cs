using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    public void CreateCoin()
    {
        Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }
}
