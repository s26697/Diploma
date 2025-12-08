using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;

    public Button MainMenuButton => mainMenuButton;
    public Button ExitButton => exitButton;

    void Start()
    {
        Hide();
    }

    public void Hide() => root.SetActive(false);

    public void Show(string title)
    {
        titleText.text = title;
        root.SetActive(true);
    }
}
