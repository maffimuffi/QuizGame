using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // Scripts
    public GameManager gameManager;

    // GameObjects
    public GameObject menuScreen;
    public GameObject gameScreen;


    public void StartGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        gameManager.gameState = 1;
    }

    public void ContinueGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void Settings()
    {

    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
