using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    public Coin GetCoin()
    {
        return _coin;
    }
}
