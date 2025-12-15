using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] Transform anchor;
    GameObject current;

    public void Spawn(GameObject prefab)
    {
        Clear();
        if (prefab == null) return;

        current = Instantiate(prefab, anchor.position, anchor.rotation, anchor);
    }

    public void Clear()
    {
        if (current != null)
            Destroy(current);

        current = null;
    }
}
