using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<Health>().TakeDamage(10);
        }

        // позже: движение, атака, агро
    }
}
