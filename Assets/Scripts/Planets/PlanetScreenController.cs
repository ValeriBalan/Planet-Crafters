using UnityEngine;

public class PlanetScreenController : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] PlanetData planet;

    [Header("View")]
    [SerializeField] GameObject screenRoot;
    [SerializeField] PlanetSpawner spawner;

    void Start()
    {
        Open();
    }

    public void Open()
    {
        if (screenRoot != null)
            screenRoot.SetActive(true);

        if (planet != null)
            spawner.Spawn(planet.planetPrefab);
    }

    public void Close()
    {
        if (screenRoot != null)
            screenRoot.SetActive(false);

        spawner.Clear();
    }

    public void OnPlanetClicked()
    {
        if (planet == null) return;
        if (planet.targetSceneIndex < 0) return;

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(planet.targetSceneIndex);
    }
}
