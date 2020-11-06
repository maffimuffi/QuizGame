using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public ColorThemeManager colorManager;

    public TMP_Text themeText;

    public GameObject settingsScreen;
    public GameObject mainmenuScreen;

    public Button musicButton;
    public Button soundButton;

    public bool musicOn;
    public bool soundOn;

    void Awake()
    {
        if (musicOn)
        {
            musicButton.image.color = new Color32(108, 255, 150, 255);
        }
        else if (!musicOn)
        {
            musicButton.image.color = new Color32(255, 107, 107, 255);
        }
        if (soundOn)
        {
            soundButton.image.color = new Color32(108, 255, 150, 255);
        }
        else if (!soundOn)
        {
            soundButton.image.color = new Color32(255, 107, 107, 255);
        }
    }

    void Update()
    {
        themeText.text = "Teema: " + colorManager.themeIndex;
    }

    public void MusicButton()
    {
        if(musicOn)
        {
            musicOn = false;
            AudioManager.Instance.StopSound("GameMusic");
            musicButton.image.color = Color.red;
        }
        else if(!musicOn)
        {
            musicOn = true;
            AudioManager.Instance.PlaySound("GameMusic");
            musicButton.image.color = Color.green;
        }
    }

    public void SoundButton()
    {
        if(soundOn)
        {
            soundOn = false;
            soundButton.image.color = Color.red;
        }
        else if(!soundOn)
        {
            soundOn = true;
            soundButton.image.color = Color.green;
        }
    }

    public void BackButton()
    {
        settingsScreen.SetActive(false);
        mainmenuScreen.SetActive(true);
    }

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
