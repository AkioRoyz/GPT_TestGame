using UnityEngine;
using System;

public class PlayerExperience : MonoBehaviour
{
    [Header("Level")]
    public int currentLevel = 1;

    [Header("Experience")]
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;

    [Tooltip("Множитель роста опыта на следующий уровень")]
    public float experienceGrowthMultiplier = 1.2f;

    // 🔹 События (ВАЖНО для UI и эффектов)
    public event Action OnExperienceChanged;
    public event Action OnLevelUp;

    /// <summary>
    /// Добавить опыт (квесты, предметы, зоны и т.д.)
    /// </summary>
    public void AddExperience(int amount)
    {
        if (amount <= 0) return;

        currentExperience += amount;
        OnExperienceChanged?.Invoke();

        // Проверяем ап уровня (может быть несколько сразу)
        while (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExperience -= experienceToNextLevel;
        currentLevel++;

        experienceToNextLevel = Mathf.RoundToInt(
            experienceToNextLevel * experienceGrowthMultiplier
        );

        OnLevelUp?.Invoke();
        OnExperienceChanged?.Invoke();
    }

    /// <summary>
    /// Процент заполнения полоски опыта (0–1)
    /// </summary>
    public float CurrentExperiencePercent
    {
        get
        {
            if (experienceToNextLevel <= 0) return 0f;
            return (float)currentExperience / experienceToNextLevel;
        }
    }
}
