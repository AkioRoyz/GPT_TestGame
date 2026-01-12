using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float aimRadius = 5f; // Радиус поиска цели при автоприцеле

    private PlayerInputHandler inputHandler;
    private PlayerStats playerStats;

    private Vector2 lastMoveDirection = Vector2.right;
    private Transform currentTarget;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        playerStats = GetComponent<PlayerStats>();

        if (inputHandler == null)
            Debug.LogError("PlayerInputHandler not found on Player!");
        if (playerStats == null)
            Debug.LogError("PlayerStats not found on Player!");
    }

    private void OnEnable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnAttackPressed += PerformAttack;
            inputHandler.OnAimTarget += HandleAimInput;
        }
    }

    private void OnDisable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnAttackPressed -= PerformAttack;
            inputHandler.OnAimTarget -= HandleAimInput;
        }
    }

    private void Update()
    {
        // Сохраняем направление движения
        Vector2 moveInput = inputHandler != null ? inputHandler.MoveInput : Vector2.zero;
        if (moveInput.sqrMagnitude > 0.01f)
            lastMoveDirection = moveInput.normalized;

        // Если удерживаем автоприцел, ищем ближайшую цель
        if (inputHandler != null && inputHandler.IsAiming)
        {
            FindClosestTarget();
        }
        else
        {
            currentTarget = null;
        }

        // 🔹 Постоянное обновление направления attackPoint
        if (attackPoint != null)
        {
            if (currentTarget != null)
                attackPoint.right = (currentTarget.position - attackPoint.position).normalized;
            else
                attackPoint.right = lastMoveDirection;
        }
    }

    private void HandleAimInput(bool isPressed)
    {
        if (inputHandler != null)
            inputHandler.SetAiming(isPressed); // 🔹 безопасно через метод

        if (!isPressed)
            currentTarget = null; // сброс цели
    }

    private void FindClosestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            aimRadius,
            enemyLayer
        );

        float closestDist = Mathf.Infinity;
        Transform closest = null;

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable =
                hit.GetComponent<IDamageable>() ??
                hit.GetComponentInParent<IDamageable>() ??
                hit.GetComponentInChildren<IDamageable>();

            if (damageable != null)
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = hit.transform;
                }
            }
        }

        currentTarget = closest;
    }

    private void PerformAttack()
    {
        Debug.Log("PERFORM ATTACK CALLED");

        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint is NOT assigned!");
            return;
        }

        // 🔹 Направление атаки уже обновляется в Update, можно оставить

        int damage = playerStats.attackPower;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable =
                hit.GetComponent<IDamageable>() ??
                hit.GetComponentInParent<IDamageable>() ??
                hit.GetComponentInChildren<IDamageable>();

            if (damageable != null)
            {
                Debug.Log($"HIT DAMAGEABLE: {hit.name}, Damage: {damage}");
                damageable.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        // 🔹 Радиус автоприцела
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aimRadius);
    }
}
