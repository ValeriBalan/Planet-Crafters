using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBadgeView : MonoBehaviour
{
    [Header("Progress")]
    [SerializeField] GameObject progressRoot;
    [SerializeField] Image progressRing;
    [SerializeField] TMP_Text progressText;

    [Header("Stars")]
    [SerializeField] GameObject starsRoot;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    public void HideAll()
    {
        if (progressRoot != null) progressRoot.SetActive(false);
        if (starsRoot != null) starsRoot.SetActive(false);
    }

    public void ShowUnlocked()
    {
        HideAll();
    }

    public void ShowProgress(int percent)
    {
        if (starsRoot != null) starsRoot.SetActive(false);
        if (progressRoot != null) progressRoot.SetActive(true);

        int p = Mathf.Clamp(percent, 0, 100);
        if (progressText != null) progressText.text = p + "%";
        if (progressRing != null) progressRing.fillAmount = Mathf.Clamp01(p / 100f);
    }

    public void ShowStars(int stars)
    {
        if (progressRoot != null) progressRoot.SetActive(false);
        if (starsRoot != null) starsRoot.SetActive(true);

        int s = Mathf.Clamp(stars, 0, 3);
        if (star1 != null) star1.SetActive(s >= 1);
        if (star2 != null) star2.SetActive(s >= 2);
        if (star3 != null) star3.SetActive(s >= 3);
    }
}
