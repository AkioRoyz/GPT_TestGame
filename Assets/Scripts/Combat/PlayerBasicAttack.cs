using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    private PlayerInputHandler inputHandler;
    private PlayerStats stats;

    private Vector2 lastMoveDirection = Vector2.right;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        stats = GetComponent<PlayerStats>();

        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint не назначен в PlayerBasicAttack!");
        }
    }

    private void OnEnable()
    {
        if (inputHandler != null)
            inputHandler.OnAttackPressed += PerformAttack;
    }

    private void OnDisable()
    {
        if (inputHandler != null)
            inputHandler.OnAttackPressed -= PerformAttack;
    }

    private void Update()
    {
        // Обновляем последнее направление движения
        Vector2 moveInput = inputHandler.MoveInput;
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
            RotateAttackPoint();
        }
    }

    private void RotateAttackPoint()
    {
        attackPoint.localPosition = lastMoveDirection * 0.5f;
    }

    private void PerformAttack()
    {
        Debug.Log("PERFORM ATTACK CALLED");

        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        if (attackPoint == null)
            return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(stats.attackPower);
            }
        }
    }

    // Визуализация радиуса атаки в Scene
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
