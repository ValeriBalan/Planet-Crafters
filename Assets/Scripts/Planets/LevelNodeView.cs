using UnityEngine;

public class LevelNodeView : MonoBehaviour
{
    [SerializeField] Renderer[] renderers;
    [SerializeField] Transform topAnchor;
    [SerializeField] Collider hitCollider;
    [SerializeField] Material lockedOverlayMaterial; // optional (leave null if you use only fade)

    MaterialPropertyBlock mpb;

    public int LevelId { get; private set; }
    public HexCoord Coord { get; private set; }
    public LevelBadgeView Badge { get; private set; }

    public void Init(int levelId, HexCoord coord, Material planetMat, LevelBadgeView badgePrefab)
    {
        LevelId = levelId;
        Coord = coord;

        ApplyPlanetMaterial(planetMat);
        EnsureMPB();

        if (badgePrefab != null && topAnchor != null)
        {
            Badge = Instantiate(badgePrefab, topAnchor.position, topAnchor.rotation, topAnchor);
            Badge.transform.localPosition = Vector3.zero;
        }
    }

    public void SetState(LevelState s)
    {
        if (s == null) return;

        if (s.state == LevelStateType.Locked)
        {
            SetFade(0.35f);
            if (Badge != null) Badge.HideAll();
            SetInteractable(false);
        }
        else if (s.state == LevelStateType.Unlocked)
        {
            SetFade(1f);
            if (Badge != null) Badge.ShowUnlocked();
            SetInteractable(true);
        }
        else if (s.state == LevelStateType.InProgress)
        {
            SetFade(1f);
            if (Badge != null) Badge.ShowProgress(s.percent);
            SetInteractable(true);
        }
        else if (s.state == LevelStateType.Completed)
        {
            SetFade(1f);
            if (Badge != null) Badge.ShowStars(s.stars);
            SetInteractable(true);
        }
    }

    void ApplyPlanetMaterial(Material planetMat)
    {
        if (planetMat == null || renderers == null) return;
        for (int i = 0; i < renderers.Length; i++) if (renderers[i] != null) renderers[i].sharedMaterial = planetMat;
    }

    void EnsureMPB() { if (mpb == null) mpb = new MaterialPropertyBlock(); }

    void SetFade(float alpha)
    {
        if (renderers == null) return;
        EnsureMPB();

        for (int i = 0; i < renderers.Length; i++)
        {
            var r = renderers[i];
            if (r == null) continue;

            r.GetPropertyBlock(mpb);

            if (r.sharedMaterial != null && r.sharedMaterial.HasProperty("_BaseColor"))
            {
                var c = r.sharedMaterial.GetColor("_BaseColor");
                c.a = alpha;
                mpb.SetColor("_BaseColor", c);
            }
            else if (r.sharedMaterial != null && r.sharedMaterial.HasProperty("_Color"))
            {
                var c = r.sharedMaterial.GetColor("_Color");
                c.a = alpha;
                mpb.SetColor("_Color", c);
            }

            r.SetPropertyBlock(mpb);
        }
    }

    void SetInteractable(bool on)
    {
        if (hitCollider != null) hitCollider.enabled = on;
    }
}
