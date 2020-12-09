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
    // Metodi päävalikon Uusi peli-napin toiminnalle millä aloitetaan uusi peli.
    public void StartNewButton()
    {
        gameManager.StartNewGame();
    }
    // Metodi päävalikon Jatka-napin toiminnalle, millä pystyy lataamaan aikaisemman pelin edistymisen.
    public void ContinueButton()
    {
        mainmenuScreen.SetActive(false);
        gameScreen.SetActive(true);
        gameManager.LoadPlayer();
    }
    // Metodi päävalikon asetukset-napin toiminnalle, millä päävalikko suljetaan ja asetus-valikko aukaistaan.
    public void SettingsButton()
    {
        mainmenuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
    // Metodi päävalikon Poistu-napin toiminnalle, millä pelin saa suljettua(pääasiassa mobiililla). 
    public void CloseGameButton()
    {
        Application.Quit();
    }
}
