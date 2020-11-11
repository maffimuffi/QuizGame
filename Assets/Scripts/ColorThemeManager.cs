﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorThemeManager : MonoBehaviour
{
    // Containers for all objects with categories
    private List<Image> baseColorObjects = new List<Image>();
    private List<Image> secondaryColorObjects = new List<Image>();
    private List<Image> buttonColorObjects = new List<Image>();
    private List<TMP_Text> textColorObjects = new List<TMP_Text>();
    public List<Theme> themes = new List<Theme>();
    public Theme currentTheme;
    public Canvas canvas;

    public int themeIndex = 0;

    void Awake()
    {
        FillColorLists();
        Theme1();
        currentTheme = themes[0];
        SetTheme();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(themeIndex == (themes.Count - 1))
            {
                themeIndex = 0;
                currentTheme = themes[themeIndex];
                SetTheme();
            }
            else
            {
                themeIndex++;
                currentTheme = themes[themeIndex];
                SetTheme();
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (themeIndex == 0)
            {
                themeIndex = (themes.Count - 1);
                currentTheme = themes[themeIndex];
                SetTheme();
            }
            else
            {
                themeIndex--;
                currentTheme = themes[themeIndex];
                SetTheme();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            themeIndex = 0;
            currentTheme = themes[themeIndex];
            SetTheme();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            themeIndex = 1;
            currentTheme = themes[themeIndex];
            SetTheme();
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            themeIndex = 2;
            currentTheme = themes[themeIndex];
            SetTheme();
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            themeIndex = 3;
            currentTheme = themes[themeIndex];
            SetTheme();
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            themeIndex = 4;
            currentTheme = themes[themeIndex];
            SetTheme();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            themeIndex = 5;
            currentTheme = themes[themeIndex];
            SetTheme();

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            themeIndex = 6;
            currentTheme = themes[themeIndex];
            SetTheme();

        }
    }
    public void FillColorLists()
    {
        foreach(Image i in canvas.GetComponentsInChildren<Image>())
        {
            if(i.CompareTag("PrimaryColor"))
            {
                baseColorObjects.Add(i);
            }
            else if(i.CompareTag("SecondaryColor"))
            {
                secondaryColorObjects.Add(i);
            }
            else if(i.CompareTag("ButtonColor"))
            {
                buttonColorObjects.Add(i);
            }
        }
        foreach(TMP_Text t in canvas.GetComponentsInChildren<TMP_Text>())
        {
           textColorObjects.Add(t);
        }
    }
    public void SetTheme()
    {
        
        foreach(Image obj1 in baseColorObjects)
        {
            obj1.color = currentTheme.baseColor;
        }
        foreach (Image obj2 in secondaryColorObjects)
        {
            obj2.color = currentTheme.secondaryColor;
        }
        foreach (TMP_Text obj3 in textColorObjects)
        {
            obj3.color = currentTheme.textColor;
        }
    }
    void Theme1()
    {
        themes.Add(new Theme(new Color32(18, 10, 92, 255), new Color32(56, 90, 186, 255), new Color32(255, 255, 255, 255), "Ultramariini"));
        themes.Add(new Theme(new Color32(0, 33, 43, 255),new Color32(0, 57, 75, 255),new Color32(255, 255, 255, 255), "Tummansininen"));
        themes.Add(new Theme(new Color32(0, 43, 33, 255),new Color32(0, 75, 57, 255), new Color32(255, 255, 255, 255), "Vihreä"));
        themes.Add(new Theme(new Color32(97, 79, 6, 255),new Color32(224, 182, 13, 255), new Color32(255, 255, 255, 255), "Keltainen"));
        themes.Add(new Theme(new Color32(97, 37, 25, 255),new Color32(97, 61, 54, 255), new Color32(255, 255, 255, 255), "Punainen"));
        themes.Add(new Theme(new Color32(92, 35, 97, 255),new Color32(212, 81, 224, 255), new Color32(255, 255, 255, 255), "Pinkki"));
    }
}
[System.Serializable]
public class Theme
{

    public Theme(Color primary,Color secondary,Color text, string name)
    {
        baseColor = primary;
        secondaryColor = secondary;
        textColor = text;
        themeName = name;
    }
    public Color baseColor;
    public Color secondaryColor;
    public Color textColor;
    public string themeName;
}
