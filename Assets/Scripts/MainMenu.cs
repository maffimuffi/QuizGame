using System.Collections;
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

    public void Continue()
    {
        gameManager.ContinueGame();
    }

    public void StartNewButton()
    {
        mainmenuScreen.SetActive(false);
        gameScreen.SetActive(true);
        gameManager.gameState = 1;
    }

    public void ContinueButton()
    {
        mainmenuScreen.SetActive(false);
        gameScreen.SetActive(true);
        // Loading the save here!
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
