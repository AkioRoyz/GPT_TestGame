using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Update()
    {
        panel.SetActive(GameManager.Instance.CurrentState == GameState.MainMenu);
    }

    public void OnStartButton()
    {
        GameManager.Instance.SetState(GameState.Playing);
    }
}
