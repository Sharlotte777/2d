using UnityEngine;

public class SpawnPointOfFirstAidKit : MonoBehaviour
{
    [SerializeField] private FirstAidKit _firstAidKitPrefab;

    private float _size = 2f;

    public void CreateFirstAidKit()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _size);

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.TryGetComponent(out FirstAidKit firstAidKit))
            {
                Destroy(objects[i].gameObject);
            }
        }

        Instantiate(_firstAidKitPrefab, transform.position, Quaternion.identity);
    }
}
