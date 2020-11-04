﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // Scripts
    public GameManager gameManager;

    // GameObjects
    public GameObject mainmenuScreen;
    public GameObject gameScreen;
    public GameObject settingsScreen;

    void Awake()
    {
        mainmenuScreen.SetActive(true);
        gameScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void StartNewButton()
    {
        gameManager.StartNewGame();
    }

    public void ContinueButton()
    {
        mainmenuScreen.SetActive(false);
        gameScreen.SetActive(true);
        gameManager.LoadPlayer();
    }

    public void SettingsButton()
    {
        mainmenuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void CloseGameButton()
    {
        Application.Quit();
    }
}
