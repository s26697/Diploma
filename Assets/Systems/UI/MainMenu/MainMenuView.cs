using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    public event Action OnPlayPressed;
    public event Action OnOptionsPressed;
    public event Action OnCreditsPressed;
    public event Action OnExitPressed;

    private void Awake()
    {
        playButton.onClick.AddListener(() => OnPlayPressed?.Invoke());
        optionsButton.onClick.AddListener(() => OnOptionsPressed?.Invoke());
        creditsButton.onClick.AddListener(() => OnCreditsPressed?.Invoke());
        exitButton.onClick.AddListener(() => OnExitPressed?.Invoke());
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
