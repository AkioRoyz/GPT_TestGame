using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public event Action OnDamaged;
    public event Action OnDied;

    // 🔹 НОВОЕ СОБЫТИЕ
    public event Action OnHealed;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        OnDamaged?.Invoke();

        if (IsDead)
            OnDied?.Invoke();
    }

    // 🔹 НОВЫЙ МЕТОД ВОССТАНОВЛЕНИЯ ЗДОРОВЬЯ
    public void RestoreHealth(int amount)
    {
        if (IsDead) return;
        if (amount <= 0) return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHealed?.Invoke();
    }

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }

    public float CurrentHealthPercent
    {
        get
        {
            if (maxHealth <= 0) return 0f;
            return (float)currentHealth / maxHealth;
        }
    }
}
