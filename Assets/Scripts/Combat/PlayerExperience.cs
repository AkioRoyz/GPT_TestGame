using UnityEngine;
using System;

public class PlayerExperience : MonoBehaviour
{
    [Header("Level")]
    public int level = 1;

    [Header("Experience")]
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    // 🔹 НОВОЕ СОБЫТИЕ
    public event Action OnXPChanged;

    // 🔹 СУЩЕСТВУЮЩЕЕ СОБЫТИЕ
    public event Action<int> OnLevelUp;

    public void AddExperience(int amount)
    {
        if (amount <= 0) return;

        currentXP += amount;

        // 🔹 уведомляем UI СРАЗУ
        OnXPChanged?.Invoke();

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();

            // 🔹 XP изменился после сброса
            OnXPChanged?.Invoke();
        }
    }

    private void LevelUp()
    {
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);
        OnLevelUp?.Invoke(level);
    }

    public float CurrentXPPercent
    {
        get
        {
            if (xpToNextLevel <= 0) return 0f;
            return (float)currentXP / xpToNextLevel;
        }
    }
}
