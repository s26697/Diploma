using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] private GameOverView view;

    private void OnEnable()
    {
        view.Hide();

        GameEvents.OnGameWon += ShowWinScreen;
        GameEvents.OnGameLost += ShowLoseScreen;

        view.MainMenuButton.onClick.AddListener(GoToMainMenu);
        view.ExitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        GameEvents.OnGameWon -= ShowWinScreen;
        GameEvents.OnGameLost -= ShowLoseScreen;

        view.MainMenuButton.onClick.RemoveListener(GoToMainMenu);
        view.ExitButton.onClick.RemoveListener(ExitGame);
    }

    private void ShowWinScreen() =>
        view.Show("You Won!");

    private void ShowLoseScreen() =>
        view.Show("You Lost!");

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_menu");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
