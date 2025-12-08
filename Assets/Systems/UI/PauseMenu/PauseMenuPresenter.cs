using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuPresenter : MonoBehaviour
{
    [SerializeField] private PauseMenuView view;

    private bool _isOpen;

    private void OnEnable()
    {
        view.OnResumeClicked += HandleResume;
        view.OnMainMenuClicked += HandleMainMenu;
        view.OnExitClicked += HandleExit;

        GameEvents.OnGamePaused += HandlePause;
        GameEvents.OnGameResumed += HandleResumeByEvent;
    }

    private void OnDisable()
    {
        view.OnResumeClicked -= HandleResume;
        view.OnMainMenuClicked -= HandleMainMenu;
        view.OnExitClicked -= HandleExit;

        GameEvents.OnGamePaused -= HandlePause;
        GameEvents.OnGameResumed -= HandleResumeByEvent;
    }

    private void Start()
    {
        view.Hide();
    }

    private void HandlePause()
    {
        _isOpen = true;
        view.Show();
        GameEvents.PauseMenuOpened();
    }

    private void HandleResume()
    {
        if (!_isOpen) return;

        GameManager.Instance.ResumeGame();  
        
    }

    private void HandleResumeByEvent()
    {
        _isOpen = false;
        view.Hide();
        GameEvents.PauseMenuClosed();
    }

    private void HandleMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_menu");
    }

    private void HandleExit()
    {
        Application.Quit();
    }
}
