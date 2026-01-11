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

    [Header("Level Up Growth")]
    [Tooltip("Прирост максимального здоровья за уровень")]
    public int healthPerLevel = 10;

    [Tooltip("Прирост максимальной маны за уровень")]
    public int manaPerLevel = 5;

    [Tooltip("Прирост силы атаки за уровень")]
    public int attackPowerPerLevel = 2;

    [Tooltip("Прирост регенерации здоровья (%) за уровень")]
    public float healthRegenPerLevel = 0.2f;

    [Tooltip("Прирост регенерации маны (%) за уровень")]
    public float manaRegenPerLevel = 0.3f;

    private Health health;
    private Mana mana;
    private PlayerExperience experience;

    void Awake()
    {
        health = GetComponent<Health>();
        mana = GetComponent<Mana>();
        experience = GetComponent<PlayerExperience>();

        if (health != null)
            health.maxHealth = maxHealth;

        if (mana != null)
            mana.maxMana = maxMana;

        // 🔹 Подписка на ап уровня
        if (experience != null)
            experience.OnLevelUp += HandleLevelUp;
    }

    void Start()
    {
        // 🔹 ТВОЯ СТАРАЯ ЛОГИКА — БЕЗ ИЗМЕНЕНИЙ
        if (health != null)
            StartCoroutine(HealthRegenRoutine());

        if (mana != null)
            StartCoroutine(ManaRegenRoutine());
    }

    private void OnDestroy()
    {
        if (experience != null)
            experience.OnLevelUp -= HandleLevelUp;
    }

    // 🔹 ОБРАБОТКА ПОВЫШЕНИЯ УРОВНЯ
    private void HandleLevelUp()
    {
        // Увеличиваем характеристики
        maxHealth += healthPerLevel;
        maxMana += manaPerLevel;
        attackPower += attackPowerPerLevel;

        healthRegenPercentPerSecond += healthRegenPerLevel;
        manaRegenPercentPerSecond += manaRegenPerLevel;

        // Обновляем Health и Mana
        if (health != null)
        {
            health.maxHealth = maxHealth;
            health.RestoreHealth(maxHealth); // полное восстановление
        }

        if (mana != null)
        {
            mana.maxMana = maxMana;
            mana.RestoreMana(maxMana); // полное восстановление
        }
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
