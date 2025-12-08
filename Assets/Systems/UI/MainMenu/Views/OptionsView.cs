using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionsView : MonoBehaviour
{
    [SerializeField] private Button backButton;

    public event Action OnBack;

    private void Awake()
    {
        backButton.onClick.AddListener(() => OnBack?.Invoke());
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
