using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorThemeManager : MonoBehaviour
{
    // Containers for all objects with categories
    [SerializeField] private Image[] baseColorObjects;
    [SerializeField] private Image[] secondaryColorObjects;
    [SerializeField] private TMP_Text[] textColorObjects;

    // musicButton.image.color = Color.red;
    void Awake()
    {
        Theme1();
    }

    void Update()
    {
        
    }

    void Theme1()
    {
        foreach(Image obj1 in baseColorObjects)
        {
            obj1.color = new Color32(0, 33, 43, 255);
        }
        foreach (Image obj2 in secondaryColorObjects)
        {
            obj2.color = new Color32(0, 57, 75, 255);
        }
        foreach (TMP_Text obj3 in textColorObjects)
        {
            obj3.color = new Color32(255, 255, 255, 255);
        }
    }
}
