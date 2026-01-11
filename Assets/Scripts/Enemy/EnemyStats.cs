using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 30;
    public int damage = 5;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();

        if (health != null)
        {
            health.maxHealth = maxHealth;
        }
    }
}
