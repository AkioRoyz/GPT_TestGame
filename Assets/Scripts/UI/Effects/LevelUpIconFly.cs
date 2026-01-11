using UnityEngine;

public class LevelUpIconFly : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float lifetime;
    private float timer;

    public void Init(Vector3 dir, float moveSpeed, float lifeTime)
    {
        direction = dir;
        speed = moveSpeed;
        lifetime = lifeTime;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
