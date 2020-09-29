using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    public GameObject settingsScreen;
    public GameObject mainmenuScreen;

    public void BackButton()
    {
        settingsScreen.SetActive(false);
        mainmenuScreen.SetActive(true);
    }

    // Code to change music and sound volume from sliders
}
