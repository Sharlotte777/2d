using UnityEngine;

public class SpawnPoint : Item
{
    [SerializeField] private Coin _coinPrefab;

    private float _radius = 2f;

    public void CreateCoin()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _radius);

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.TryGetComponent(out Coin coin))
            {
                Destroy(objects[i].gameObject);
            }
        }

        Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }
}
