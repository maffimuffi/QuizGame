using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorThemeManager : MonoBehaviour
{
    // Lists for all objects with categories
    private List<Image> baseColorObjects = new List<Image>();
    private List<Image> secondaryColorObjects = new List<Image>();
    private List<TMP_Text> textColorObjects = new List<TMP_Text>();
    public List<Theme> themes = new List<Theme>();
    public Theme currentTheme;
    public Canvas canvas;

    public int themeIndex = 0;

    // Tapahtuu pelin alkaessa. Etsii aluksi kaikki komponentit minkä väriä täytyy vaihtaa, lisää teemat mitkä halutaan ja asettaa ensimmäisen teeman aktiiviseksi listalla.
    void Awake()
    {
        FillColorLists();
        AddThemes();
        currentTheme = themes[0];
        SetTheme();
    }

    void Update()
    {
        // Painamalla E teema vaihtuu seuraavaan listalla, minne teemat on tallennettu
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
        // Painamalla Q teema vaihtuu edelliseen listalla, minne teemat on tallennettu
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
    }
    // Etsii kaikki komponentit, minkä väriä teemat vaihtavat, ja tietyillä tageilla värit vaihdetaan oikeiksi. Tagit on liitetty jokaiseen objektiin skenessä, mitä halutaan muuttaa.
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
        }
        foreach(TMP_Text t in canvas.GetComponentsInChildren<TMP_Text>())
        {
           textColorObjects.Add(t);
        }
    }
    // Vaihtaa teeman mukaan pelin komponenttien värejä oikeiksi.
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
            if(themeIndex == 6)
            {
                if (obj3.tag == "SpecialText")
                {
                    obj3.color = currentTheme.baseColor;
                }
                else
                {
                    obj3.color = currentTheme.textColor;
                }
            }
            else
            {
                obj3.color = currentTheme.textColor;
            }
        }
    }
    // Tänne pystyy lisäämään eri väriteemoja listaan niin paljon kuin haluaa. Täytyy siis antaa 3 värikoodia RGB muodossa(1.Perusväri,2.Toinen muut objektit,3.tekstit) ja nimi.
    void AddThemes()
    {
        themes.Add(new Theme(new Color32(18, 10, 92, 255), new Color32(56, 85, 186, 255), new Color32(255, 255, 255, 255), "Ultramariini"));
        themes.Add(new Theme(new Color32(5, 41, 51, 255),new Color32(6, 75, 96, 255),new Color32(255, 255, 255, 255), "Tummansininen"));
        themes.Add(new Theme(new Color32(0, 43, 33, 255),new Color32(0, 75, 57, 255), new Color32(255, 255, 255, 255), "Vihreä"));
        themes.Add(new Theme(new Color32(97, 79, 6, 255),new Color32(173, 146, 15, 255), new Color32(255, 255, 255, 255), "Keltainen"));
        themes.Add(new Theme(new Color32(135, 15, 39, 255),new Color32(164, 61, 54, 255), new Color32(255, 255, 255, 255), "Tuli"));
        themes.Add(new Theme(new Color32(38, 8, 14, 255),new Color32(123, 35, 65, 255), new Color32(255, 255, 255, 255), "Pinkki"));
        themes.Add(new Theme(new Color32(165, 0, 0, 255), new Color32(217, 217, 217, 255), new Color32(255, 255, 255, 255), "Joulu"));
    }
}
// Luokka, millä saadaan luotua teemat.
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
