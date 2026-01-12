using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health = 20;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} получил урон: {amount}, осталось: {health}");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
