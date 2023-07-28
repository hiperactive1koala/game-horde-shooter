using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MainMenu: Singleton<MainMenu>
{
    public EventHandler OnPlayClicked;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _playButton.onClick.AddListener(() =>
        {
            _mainMenu.SetActive(false);
            OnPlayClicked?.Invoke(this, EventArgs.Empty);
            
        });
        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        GameManager.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState obj)
    {
        if (obj == GameState.MainMenu)
        {
            _mainMenu.SetActive(true);
        }
    }
}