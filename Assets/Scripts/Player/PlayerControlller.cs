using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();

        if (inputHandler == null)
        {
            Debug.LogError("PlayerInputHandler не найден на объекте Player!");
            enabled = false; // Отключаем скрипт, чтобы не было NullReference
        }
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = inputHandler.MoveInput;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * speed;
    }
}
