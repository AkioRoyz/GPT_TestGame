using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Update()
    {
        panel.SetActive(GameManager.Instance.CurrentState == GameState.Paused);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameManager.Instance.SetState(GameState.Paused);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameState.Playing);
    }
}
