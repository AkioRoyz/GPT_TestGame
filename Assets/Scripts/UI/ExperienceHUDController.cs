using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExperienceHUDController : MonoBehaviour
{
    [Header("References")]
    public PlayerExperience playerExperience;

    [Header("Experience UI")]
    public Image expFill;       // Основная полоса (растёт)
    public Image expFillBack;   // Фон (всегда полный)

    [Header("Animation Settings")]
    public float fillSpeed = 2f;

    private Coroutine expCoroutine;

    void Start()
    {
        if (playerExperience != null)
        {
            playerExperience.OnExperienceChanged += AnimateExperienceBar;

            // 🔹 Инициализация
            if (expFill != null)
                expFill.fillAmount = playerExperience.CurrentExperiencePercent;

            // 🔹 Фон всегда полный
            if (expFillBack != null)
                expFillBack.fillAmount = 1f;
        }
    }

    private void OnDestroy()
    {
        if (playerExperience != null)
            playerExperience.OnExperienceChanged -= AnimateExperienceBar;
    }

    void AnimateExperienceBar()
    {
        if (expFill == null) return;

        float target = playerExperience.CurrentExperiencePercent;

        if (expCoroutine != null)
            StopCoroutine(expCoroutine);

        expCoroutine = StartCoroutine(AnimateFill(expFill, target, fillSpeed));
    }

    IEnumerator AnimateFill(Image bar, float targetFill, float speed)
    {
        float startFill = bar.fillAmount;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            bar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed);
            yield return null;
        }

        bar.fillAmount = targetFill;
    }
}
