using System;

public interface IMainMenuView
{
    event Action OnPlayPressed;
    event Action OnOptionsPressed;
    event Action OnCreditsPressed;
    event Action OnExitPressed;

    void Show();
    void Hide();
}
