using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;
    public event Action OnExitClicked;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
        exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
    }

    public void Show() => root.SetActive(true);
    public void Hide() => root.SetActive(false);
}
