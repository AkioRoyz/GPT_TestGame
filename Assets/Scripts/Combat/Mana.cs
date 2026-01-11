using UnityEngine;
using System;

public class Mana : MonoBehaviour
{
    [Header("Mana")]
    public int maxMana = 100;
    public int currentMana;

    [Header("References")]
    public Health health; // 🔹 Проверяем, жив ли персонаж

    public event Action OnManaChanged;

    void Start()
    {
        currentMana = maxMana;

        // 🔹 Автопоиск Health, если не назначен вручную
        if (health == null)
            health = GetComponent<Health>();
    }

    /// <summary>
    /// Потратить ману
    /// </summary>
    public void SpendMana(int amount)
    {
        if (IsDead) return;
        if (amount <= 0) return;

        currentMana -= amount;

        if (currentMana < 0)
            currentMana = 0;

        OnManaChanged?.Invoke();
    }

    /// <summary>
    /// Восстановить ману
    /// </summary>
    public void RestoreMana(int amount)
    {
        if (IsDead) return;
        if (amount <= 0) return;

        currentMana += amount;

        if (currentMana > maxMana)
            currentMana = maxMana;

        OnManaChanged?.Invoke();
    }

    /// <summary>
    /// Персонаж мёртв?
    /// </summary>
    private bool IsDead
    {
        get
        {
            return health != null && health.IsDead;
        }
    }

    /// <summary>
    /// Процент маны (0–1)
    /// </summary>
    public float CurrentManaPercent
    {
        get
        {
            if (maxMana <= 0) return 0f;
            return (float)currentMana / maxMana;
        }
    }
}
