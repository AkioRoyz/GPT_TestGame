using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetState(GameState.MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrentState == GameState.Playing)
                GameManager.Instance.SetState(GameState.Paused);
            else
                GameManager.Instance.SetState(GameState.Playing);
        }
    }


    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Game state: " + newState);
    }
}
