using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuView mainMenu;
    [SerializeField] private OptionsView options;
    [SerializeField] private CreditsView credits;

    private MainMenuPresenter presenter;

    private void Start()
    {
        presenter = new MainMenuPresenter(mainMenu, options, credits);

        mainMenu.Show();
        options.Hide();
        credits.Hide();
    }
}
