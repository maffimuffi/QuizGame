using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public GameObject settingsScreen;
    public GameObject mainmenuScreen;

    public Button musicButton;
    public Button soundButton;

    public bool musicOn;
    public bool soundOn;

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

    void Awake()
    {
        if(musicOn)
        {
            musicButton.image.color = Color.green;
        }
        else if(!musicOn)
        {
            musicButton.image.color = Color.red;
        }
        if (soundOn)
        {
            soundButton.image.color = Color.green;
        }
        else if (!soundOn)
        {
            soundButton.image.color = Color.red;
        }
    }
}
