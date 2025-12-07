using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPresenter
{
    private readonly IMainMenuView view;
    private readonly OptionsView optionsView;
    private readonly CreditsView creditsView;

    public MainMenuPresenter(IMainMenuView view, OptionsView optionsView, CreditsView creditsView)
    {
        this.view = view;
        this.optionsView = optionsView;
        this.creditsView = creditsView;

        Subscribe();
    }

    private void Subscribe()
    {
        view.OnPlayPressed += StartGame;
        view.OnOptionsPressed += OpenOptions;
        view.OnCreditsPressed += OpenCredits;
        view.OnExitPressed += ExitGame;

        optionsView.OnBack += BackToMain;
        creditsView.OnBack += BackToMain;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Gameplay"); 
    }

    private void OpenOptions()
    {
        optionsView.Show();
        view.Hide();
    }

    private void OpenCredits()
    {
        creditsView.Show();
        view.Hide();
    }

    private void BackToMain()
    {
        optionsView.Hide();
        creditsView.Hide();
        view.Show();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
