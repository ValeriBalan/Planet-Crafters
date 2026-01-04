using UnityEngine;

public class PlanetClickToScene : MonoBehaviour
{
    [SerializeField] private int sceneNum = 6;
    [SerializeField] private int planetId = 1;

    private void OnMouseDown()
    {
        PlanetSession.SelectedPlanetId = planetId;

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneNum);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNum);
    }
}
