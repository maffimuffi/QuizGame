using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorThemeManager : MonoBehaviour
{
    // Containers for all objects with categories
    [SerializeField] private List<Image> baseColorObjects = new List<Image>();
    [SerializeField] private List<Image> secondaryColorObjects= new List<Image>();
    [SerializeField] private List<TMP_Text> textColorObjects = new List<TMP_Text>();

    public Canvas canvas;

    // musicButton.image.color = Color.red;
    void Awake()
    {
        FillColorLists();
        Theme1();
        Theme2();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Theme1();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Theme2();
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
            
        }
        foreach(TMP_Text t in canvas.GetComponentsInChildren<TMP_Text>())
        {
           textColorObjects.Add(t);
        }
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

    void Theme2()
    {
        foreach (Image obj1 in baseColorObjects)
        {
            obj1.color = new Color32(0, 43, 33, 255);
        }
        foreach (Image obj2 in secondaryColorObjects)
        {
            obj2.color = new Color32(0, 75, 57, 255);
        }
        foreach (TMP_Text obj3 in textColorObjects)
        {
            obj3.color = new Color32(255, 255, 255, 255);
        }
    }
}
