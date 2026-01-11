using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;

    [Tooltip("Процент восстановления здоровья в секунду")]
    public float healthRegenPercentPerSecond = 1f;

    [Header("Mana")]
    public int maxMana = 50;

    [Tooltip("Процент восстановления маны в секунду")]
    public float manaRegenPercentPerSecond = 2f;

    [Header("Combat")]
    public int attackPower = 10;

    private Health health;
    private Mana mana;

    void Awake()
    {
        health = GetComponent<Health>();
        mana = GetComponent<Mana>();

        if (health != null)
            health.maxHealth = maxHealth;

        if (mana != null)
            mana.maxMana = maxMana;
    }

    void Start()
    {
        // 🔹 ТВОЯ СТАРАЯ ЛОГИКА — БЕЗ ИЗМЕНЕНИЙ
        if (health != null)
            StartCoroutine(HealthRegenRoutine());

        if (mana != null)
            StartCoroutine(ManaRegenRoutine());
    }

    // 🔹 ПАССИВНОЕ ВОССТАНОВЛЕНИЕ ЗДОРОВЬЯ
    private IEnumerator HealthRegenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (health == null) continue;
            if (health.IsDead) continue;

            int regenAmount = Mathf.RoundToInt(
                health.maxHealth * (healthRegenPercentPerSecond / 100f)
            );

            if (regenAmount > 0)
                health.RestoreHealth(regenAmount);
        }
    }

    // 🔹 ПАССИВНОЕ ВОССТАНОВЛЕНИЕ МАНЫ
    private IEnumerator ManaRegenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (mana == null) continue;

            int regenAmount = Mathf.RoundToInt(
                mana.maxMana * (manaRegenPercentPerSecond / 100f)
            );

            if (regenAmount > 0)
                mana.RestoreMana(regenAmount);
        }
    }
}
