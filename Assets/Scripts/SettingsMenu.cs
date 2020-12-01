using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public ColorThemeManager colorManager;

    public Sprite musicOnImage;
    public Sprite musicOffImage;
    public Sprite soundOnImage;
    public Sprite soundOffImage;

    public TMP_Text themeText;

    public GameObject settingsScreen;
    public GameObject mainmenuScreen;

    public Button musicButton;
    public Button soundButton;

    public bool musicOn;
    public bool soundOn;

    void Awake()
    {
        themeText = gameObject.transform.Find("ThemeText").GetComponent<TMP_Text>();
        musicButton.image.sprite = musicOnImage;
        soundButton.image.sprite = soundOnImage;
    }

    void Update()
    {
        // Vaihtaa asetus-valikossa olevaa teeman nimen kohtaa
        themeText.text = colorManager.currentTheme.themeName;      
    }
    // Metodi musiikki-nappulalle.
    public void MusicButton()
    {
        // Musiikki on päällä ja nappia painetaan, musiikki laitetaan pois päältä ja kuva vaihtuu.
        if(musicOn)
        {
            musicOn = false;
            AudioManager.Instance.StopSound("GameMusic");
            musicButton.image.sprite = musicOffImage;
        }
        // Musiikki ei ole päällä ja nappia painetaan niin musiikki laitetaan takaisin päälle ja kuva vaihtuu.
        else if(!musicOn)
        {
            musicOn = true;
            AudioManager.Instance.PlaySound("GameMusic");
            musicButton.image.sprite = musicOnImage;
        }
    }
    // Metodi ääni-nappulalle.
    public void SoundButton()
    {
        // Äänet ovat päällä ja nappia painetaan, äänet laitetaan pois päältä ja kuva vaihtuu.
        if (soundOn)
        {
            soundOn = false;
            soundButton.image.sprite = soundOffImage;
        }
        // Äänet eivät ole päällä ja nappia painetaan niin äänet laitetaan takaisin päälle ja kuva vaihtuu.
        else if (!soundOn)
        {
            soundOn = true;
            soundButton.image.sprite = soundOnImage;
        }
    }
    // Metodi, millä päästään asetukset valikosta takaisin päävalikkoon.
    public void BackButton()
    {
        settingsScreen.SetActive(false);
        mainmenuScreen.SetActive(true);
    }
    // Metodi millä saadaan vaihdettua seuraavaan teemaan listalla oikealle osoittavalla nuolella.
    public void NextTheme()
    {
        if(colorManager.themeIndex == (colorManager.themes.Count - 1))
        {
            colorManager.themeIndex = 0;
            colorManager.currentTheme = colorManager.themes[colorManager.themeIndex];
            colorManager.SetTheme();
        }
        else
        {
            colorManager.themeIndex++;
            colorManager.currentTheme = colorManager.themes[colorManager.themeIndex];
            colorManager.SetTheme();
        }
    }
    // Metodi, millä saadaan vaihdettua edelliseen teemaan listalla taaksepäin vasemmalle osoittavalla nuolella.
    public void PreviousTheme()
    {
        if (colorManager.themeIndex == 0)
        {
            colorManager.themeIndex = (colorManager.themes.Count - 1);
            colorManager.currentTheme = colorManager.themes[colorManager.themeIndex];
            colorManager.SetTheme();
        }
        else
        {
            colorManager.themeIndex--;
            colorManager.currentTheme = colorManager.themes[colorManager.themeIndex];
            colorManager.SetTheme();
        }
    }
}
