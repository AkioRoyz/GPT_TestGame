using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    public Health health;
    public Mana mana;
    public PlayerExperience experience; // 🔹 ВАЖНО: объявляем ссылку

    [Header("Test Values")]
    public int testDamage = 10;
    public int testManaSpend = 15;
    public int testManaRestore = 10;
    public int testHealthRestore = 10;
    public int testExperienceGain = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (health != null)
                health.TakeDamage(testDamage);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (health != null)
                health.RestoreHealth(testHealthRestore);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mana != null)
                mana.SpendMana(testManaSpend);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (mana != null)
                mana.RestoreMana(testManaRestore);
        }

        // 🔹 ТЕСТ ОПЫТА
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (experience != null)
                experience.AddExperience(testExperienceGain);
        }
    }
}
