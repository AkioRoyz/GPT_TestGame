using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUDController : MonoBehaviour
{
    [Header("References")]
    public Health playerHealth;
    public Mana playerMana;

    [Header("Health UI")]
    public Image hpFill;
    public Image hpFillBack;

    [Header("Mana UI")]
    public Image manaFill;
    public Image manaFillBack;

    [Header("Animation Settings")]
    public float fillSpeed = 2f;
    public float backFillSpeed = 1f;

    private Coroutine healthCoroutine;
    private Coroutine healthBackCoroutine;

    private Coroutine manaCoroutine;
    private Coroutine manaBackCoroutine;

    void Start()
    {
        // 🔹 Подписка на здоровье
        if (playerHealth != null)
        {
            playerHealth.OnDamaged += AnimateHealthBar;
            playerHealth.OnHealed += AnimateHealthBar;
        }

        // 🔹 Подписка на ману
        if (playerMana != null)
            playerMana.OnManaChanged += AnimateManaBar;

        // 🔹 Инициализация UI
        if (playerHealth != null)
        {
            if (hpFill != null) hpFill.fillAmount = playerHealth.CurrentHealthPercent;
            if (hpFillBack != null) hpFillBack.fillAmount = playerHealth.CurrentHealthPercent;
        }

        if (playerMana != null)
        {
            if (manaFill != null) manaFill.fillAmount = playerMana.CurrentManaPercent;
            if (manaFillBack != null) manaFillBack.fillAmount = playerMana.CurrentManaPercent;
        }

    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDamaged -= AnimateHealthBar;
            playerHealth.OnHealed -= AnimateHealthBar;
        }

        if (playerMana != null)
            playerMana.OnManaChanged -= AnimateManaBar;

    }

    // =======================
    // 🔹 HEALTH
    // =======================
    void AnimateHealthBar()
    {
        float target = playerHealth.CurrentHealthPercent;

        if (healthCoroutine != null) StopCoroutine(healthCoroutine);
        if (healthBackCoroutine != null) StopCoroutine(healthBackCoroutine);

        healthCoroutine = StartCoroutine(AnimateFill(hpFill, target, fillSpeed));
        healthBackCoroutine = StartCoroutine(AnimateFill(hpFillBack, target, backFillSpeed));
    }

    // =======================
    // 🔹 MANA
    // =======================
    void AnimateManaBar()
    {
        float target = playerMana.CurrentManaPercent;

        if (manaCoroutine != null) StopCoroutine(manaCoroutine);
        if (manaBackCoroutine != null) StopCoroutine(manaBackCoroutine);

        manaCoroutine = StartCoroutine(AnimateFill(manaFill, target, fillSpeed));
        manaBackCoroutine = StartCoroutine(AnimateFill(manaFillBack, target, backFillSpeed));
    }


    // =======================
    // 🔹 COMMON ANIMATION
    // =======================
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
