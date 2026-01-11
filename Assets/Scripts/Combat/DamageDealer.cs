using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;

    public void DealDamage(GameObject target)
    {
        Health health = target.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
