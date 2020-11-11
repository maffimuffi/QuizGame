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
        themeText.text = colorManager.currentTheme.themeName;
        
    }

    public void MusicButton()
    {
        if(musicOn)
        {
            musicOn = false;
            AudioManager.Instance.StopSound("GameMusic");
            musicButton.image.sprite = musicOffImage;
            //musicButton.image.color = Color.red;
        }
        else if(!musicOn)
        {
            musicOn = true;
            AudioManager.Instance.PlaySound("GameMusic");
            musicButton.image.sprite = musicOnImage;
            //musicButton.image.color = Color.green;
        }
    }

    public void SoundButton()
    {
        if(soundOn)
        {
            soundOn = false;
            soundButton.image.sprite = soundOffImage;
            //soundButton.image.color = Color.red;
        }
        else if(!soundOn)
        {
            soundOn = true;
            soundButton.image.sprite = soundOnImage;
            //soundButton.image.color = Color.green;
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
