using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

    private PlayerInputHandler inputHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SetState(GameState.MainMenu);

        // »щем PlayerInputHandler в сцене
        inputHandler = FindFirstObjectByType<PlayerInputHandler>();

        if (inputHandler != null)
        {
            inputHandler.OnPausePressed += TogglePause;
        }
        else
        {
            Debug.LogError("GameManager: PlayerInputHandler не найден в сцене!");
        }
    }

    private void OnDestroy()
    {
        if (inputHandler != null)
        {
            inputHandler.OnPausePressed -= TogglePause;
        }
    }

    private void TogglePause()
    {
        if (CurrentState == GameState.Playing)
        {
            Time.timeScale = 0f;
            SetState(GameState.Paused);
        }
        else if (CurrentState == GameState.Paused)
        {
            Time.timeScale = 1f;
            SetState(GameState.Playing);
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Game state: " + newState);
    }
}
