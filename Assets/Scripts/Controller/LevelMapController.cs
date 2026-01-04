using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMapController : MonoBehaviour
{
    [Header("Static Layout")]
    [SerializeField] PlanetLevelMapConfig mapConfig;

    [Header("Planet Visuals")]
    [SerializeField] PlanetVisualLibrary planetVisualLibrary;

    [Header("Prefabs")]
    [SerializeField] LevelNodeView hexNodePrefab;
    [SerializeField] LevelBadgeView badgePrefab;

    [Header("Scene")]
    [SerializeField] Transform mapRoot;
    [SerializeField] int gameplaySceneIndex = 7;

    readonly List<LevelNodeView> spawned = new List<LevelNodeView>();


    void Start()
    {
        if (mapRoot == null) mapRoot = transform;

        int planetId = PlanetSession.SelectedPlanetId != 0 ? PlanetSession.SelectedPlanetId : mapConfig.planetId;
        Material planetMat = planetVisualLibrary != null ? planetVisualLibrary.GetMaterial(planetId) : null;

        SpawnMap(planetMat);

        // TEMP: fake progress until server exists
        ApplyFakeProgress();
    }

    void SpawnMap(Material planetMat)
    {
        Vector3 size = mapConfig.hexSize;
        float edgeY = mapConfig.edgeY;

        for (int i = 0; i < mapConfig.nodes.Count; i++)
        {
            var n = mapConfig.nodes[i];
            var coord = new HexCoord(n.q, n.r);

            Vector3 pos = HexGridMath.AxialToWorldXY(coord, size, edgeY,mapConfig.centerHex);

            var node = Instantiate(hexNodePrefab, mapRoot);
            node.transform.localPosition = pos;
            node.Init(n.levelId, coord, planetMat, badgePrefab);

            spawned.Add(node);
        }
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;

        var node = hit.transform.GetComponentInParent<LevelNodeView>();
        if (node == null) return;

        // If collider disabled for locked nodes, this only triggers for interactable nodes
        GameSession.SelectedLevelId = node.LevelId;
        SceneManager.LoadScene(gameplaySceneIndex);
    }

    void ApplyFakeProgress()
    {
        // Example rules: 1 completed 3 stars, 2 in progress 75%, 3 unlocked, rest locked
        for (int i = 0; i < spawned.Count; i++)
        {
            int levelId = spawned[i].LevelId;
            var s = new LevelState();

            if (levelId == 1) { s.state = LevelStateType.Completed; s.stars = 3; }
            else if (levelId == 2) { s.state = LevelStateType.InProgress; s.percent = 75; }
            else if (levelId == 3) { s.state = LevelStateType.Unlocked; }
            else { s.state = LevelStateType.Locked; }

            spawned[i].SetState(s);
        }
    }

    float ComputeRadiusFromPrefab(LevelNodeView prefab)
    {
        // We measure the rendered width of the prefab in local space
        var rens = prefab.GetComponentsInChildren<Renderer>(true);
        if (rens == null || rens.Length == 0) return 1f;

        Bounds b = rens[0].bounds;
        for (int i = 1; i < rens.Length; i++) b.Encapsulate(rens[i].bounds);

        // bounds are in WORLD units, but in prefab mode they should still be valid enough.
        // We convert "flat width" to "radius". For flat-top hex, flat-to-flat ~= 2 * radius.
        float flatWidth = b.size.x;

        // If your hex is rotated such that width is in Y instead of X, switch to b.size.y.
        return flatWidth * 0.5f;
    }


    void Clear()
    {
        for (int i = 0; i < spawned.Count; i++)
            if (spawned[i] != null) Destroy(spawned[i].gameObject);
        spawned.Clear();
    }
}
