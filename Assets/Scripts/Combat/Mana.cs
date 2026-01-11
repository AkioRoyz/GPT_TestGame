using UnityEngine;
using System;

public class Mana : MonoBehaviour
{
    public int maxMana = 100;
    public int currentMana;

    public event Action OnManaChanged;

    void Start()
    {
        currentMana = maxMana;
    }

    public void SpendMana(int amount)
    {
        currentMana -= amount;
        if (currentMana < 0)
            currentMana = 0;

        OnManaChanged?.Invoke();
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
            currentMana = maxMana;

        OnManaChanged?.Invoke();
    }

    public float CurrentManaPercent
    {
        get
        {
            if (maxMana <= 0) return 0f;
            return (float)currentMana / maxMana;
        }
    }
}
